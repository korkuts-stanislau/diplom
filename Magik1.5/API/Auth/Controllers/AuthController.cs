using Auth.Data;
using Auth.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        [Route("signIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] UIModels.Auth request)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var account = await authService.SignIn(request);

                    return Ok(new
                    {
                        Token = authService.GenerateJWT(account)
                    });
                }
                catch (Exception exc)
                {
                    return BadRequest(exc.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("signUp")]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UIModels.Auth request)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var account = await authService.SignUp(request);

                    return Ok(new
                    {
                        Token = authService.GenerateJWT(account)
                    });
                }
                catch(Exception exc)
                {
                    return BadRequest(exc.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
