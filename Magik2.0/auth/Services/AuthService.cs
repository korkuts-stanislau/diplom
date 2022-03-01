using Auth.Data;
using Auth.Data.Interfaces;
using Auth.Models;
using Auth.UIModels;
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
        private readonly IAccountRepository rep;
        private readonly PasswordHasherService passwordHasher;

        /// <summary>
        /// User authentication service
        /// </summary>
        /// <param name="authOptions">Authentication options</param>
        /// <param name="rep">Repository with accounts data</param>
        /// <param name="passwordHasher">Password hasher</param>
        public AuthService(IOptions<AuthOptions> authOptions,
            IAccountRepository rep,
            PasswordHasherService passwordHasher)
        {
            this.authOptions = authOptions;
            this.rep = rep;
            this.passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Get user account by authentication data
        /// </summary>
        /// <param name="auth">Authentication data</param>
        /// <returns>User account</returns>
        public async Task<Account> SignIn(UIModels.AuthData auth)
        {
            var account = await rep.GetByEmailAsync(auth.Email);

            if (account == null) throw new ArgumentException("Неверный почтовый адрес");
            if (!passwordHasher.Verify(auth.Password, account.PasswordHash)) throw new ArgumentException("Неверный пароль");

            return account;
        }

        /// <summary>
        /// Create and get user account by authentication data
        /// </summary>
        /// <param name="auth">Authentication data</param>
        /// <returns>New user's account</returns>
        public async Task<Account> SignUp(UIModels.AuthData auth)
        {
            var account = await rep.GetByEmailAsync(auth.Email);

            if (account != null) throw new ArgumentException("Пользователь с такой почтой уже зарегистрирован");

            account = new Account
            {
                Id = new BsonObjectId(ObjectId.GenerateNewId()).ToString(),
                Email = auth.Email,
                PasswordHash = passwordHasher.Hash(auth.Password),
                Roles = new Role[] { Role.User }
            };
            await rep.CreateAsync(account);

            return account;
        }

        /// <summary>
        /// Generate JSON Web Token for user's account
        /// </summary>
        /// <param name="account">User's account</param>
        /// <returns>JSON Web Token</returns>
        public Token GenerateJWT(Account account)
        {
            if(string.IsNullOrWhiteSpace(account.Id)) throw new ArgumentException("Токен можно сгенерировать только для аккаунта с идентификатором.");

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

            return new Token {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
