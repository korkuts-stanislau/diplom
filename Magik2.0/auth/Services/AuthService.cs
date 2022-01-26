using Auth.Data;
using Auth.Models;
using Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
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
        private readonly AuthRepository rep;
        private readonly PasswordHasherService passwordHasher;

        public AuthService(IOptions<AuthOptions> authOptions,
            AuthRepository rep,
            PasswordHasherService passwordHasher)
        {
            this.authOptions = authOptions;
            this.rep = rep;
            this.passwordHasher = passwordHasher;
        }

        public async Task<Account> SignIn(UIModels.Auth auth)
        {
            var account = await rep.GetAccountByEmail(auth.Email);

            if (account == null)
            {
                throw new Exception("Неверный почтовый адрес");
            }

            if (!passwordHasher.Verify(auth.Password, account.PasswordHash))
            {
                throw new Exception("Неверный пароль");
            }

            return account;
        }

        public async Task<Account> SignUp(UIModels.Auth auth)
        {
            var account = await rep.GetAccountByEmail(auth.Email);

            if (account != null)
            {
                throw new Exception("Пользователь с такой почтой уже зарегистрирован");
            }

            account = new Account
            {
                Id = new BsonObjectId(ObjectId.GenerateNewId()).ToString(),
                Email = auth.Email,
                PasswordHash = passwordHasher.Hash(auth.Password),
                Roles = new Role[] { Role.User }
            };
            await rep.Create(account);

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
