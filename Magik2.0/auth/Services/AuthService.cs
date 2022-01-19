﻿using Auth.Data;
using Auth.Models;
using Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Services
{
    public class AuthService
    {
        private readonly IOptions<AuthOptions> authOptions;
        private readonly InMemoryRepository rep;
        private readonly PasswordHasherService passwordHasher;

        public AuthService(IOptions<AuthOptions> authOptions,
            InMemoryRepository rep,
            PasswordHasherService passwordHasher)
        {
            this.authOptions = authOptions;
            this.rep = rep;
            this.passwordHasher = passwordHasher;
        }

        public async Task<Account> SignIn(UIModels.Auth auth)
        {
            var account = rep.Accounts
                .FirstOrDefault(acc => acc.Email == auth.Email);

            if (account == null)
            {
                throw new Exception("There is no account with this email");
            }

            if (!passwordHasher.Verify(auth.Password, account.PasswordHash))
            {
                throw new Exception("Incorrect password");
            }

            return account;
        }

        public async Task<Account> SignUp(UIModels.Auth auth)
        {
            var account = rep.Accounts
                .FirstOrDefault(acc => acc.Email == auth.Email);

            if (account != null)
            {
                throw new Exception("Account with this email exists");
            }

            account = new Account
            {
                Id = Guid.NewGuid(),
                Email = auth.Email,
                PasswordHash = passwordHasher.Hash(auth.Password),
                Roles = new Role[] { Role.User }
            };
            rep.Accounts.Add(account);

            return account;
        }

        public string GenerateJWT(Account account)
        {
            var authParams = authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, account.Email),
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString())
            };

            foreach (var role in account.Roles)
            {
                claims.Add(new Claim("role", role.ToString()));
            }

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
