using MagikAPI.Data;
using MagikAPI.Models;
using MagikAPI.Models.Authentication;
using MagikAPI.Models.HelperModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MagikAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> authOptions;
        private readonly MagikContext context;

        public AuthController(IOptions<AuthOptions> authOptions, MagikContext context)
        {
            this.authOptions = authOptions;
            this.context = context;
        }

        [Route("signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignIn request)
        {
            var result = await AuthenticateUser(request.Email, request.Password);

            if (result.Entity != null)
            {
                return Ok(new
                {
                    access_token = GenerateJWT(result.Entity, request.Remember)
                });
            }

            return Unauthorized(result.Error);
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUp request)
        {
            var result = await SignUpUser(request.Email, request.Password);

            if (result.Entity)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Error);
            }
        }

        private async Task<MessageWrapper<Account>> AuthenticateUser(string email, string password)
        {
            var user = await context.Accounts.FirstOrDefaultAsync(user => user.Email == email);
            if (user == null)
            {
                return new MessageWrapper<Account>(null, "Нет пользователя с такой почтой");
            }
            if (user.PasswordHash != password)
            {
                return new MessageWrapper<Account>(null, "Вы ввели неверный пароль пользователя");
            }

            return new MessageWrapper<Account>(user, null);
        }

        private async Task<MessageWrapper<bool>> SignUpUser(string email, string password)
        {
            if (!ModelState.IsValid)
            {
                StringBuilder errors = new StringBuilder();

                if (ModelState.ContainsKey("Email") && ModelState["Email"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    errors.Append("Неверный почтовый адрес\n");
                }

                if (ModelState.ContainsKey("Password") && ModelState["Password"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    errors.Append("Неверный пароль\n");
                }

                return new MessageWrapper<bool>(false, errors.ToString());
            }

            if (await context.Accounts.FirstOrDefaultAsync(a => a.Email == email) != null)
            {
                return new MessageWrapper<bool>(false, "Уже есть пользователь с такой почтой");
            }

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var account = new Account
                {
                    Email = email,
                    PasswordHash = password
                };

                await context.Accounts.AddAsync(account);
                await context.SaveChangesAsync();

                //add user profile
                await context.Profiles.AddAsync(new Profile
                {
                    UserName = email,
                    AccountId = account.Id
                });
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
            }

            return new MessageWrapper<bool>(true, null);
        }

        private string GenerateJWT(Account user, bool remember)
        {
            var authParams = authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(remember ? authParams.LongTokenLifetime : authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
