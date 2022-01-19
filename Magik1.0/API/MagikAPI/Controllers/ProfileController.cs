using MagikAPI.Data;
using MagikAPI.Models.HelperModels.Profile;
using MagikAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MagikAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly MagikContext context;
        private readonly PhotoService photoService;

        public ProfileController(MagikContext context, PhotoService photoService)
        {
            this.context = context;
            this.photoService = photoService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            var currentUserId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var profile = await context.Profiles.FirstOrDefaultAsync(p => p.AccountId == currentUserId);

            if (profile == null)
            {
                return BadRequest("Сервер не может найти профиль пользователя");
            }

            return Ok(new
            {
                id = profile.Id,
                userName = profile.UserName,
                photo = profile.Photo != null ? Convert.ToBase64String(profile.Photo) : ""
            });
        }

        [HttpPut]
        [Route("changeProfilePhoto")]
        public async Task<IActionResult> ChangeProfilePhoto([FromBody] ProfilePhoto data)
        {
            var currentUserId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

            try
            {
                var profile = await context.Profiles
                    .FirstOrDefaultAsync(p => p.AccountId == currentUserId);

                profile.Photo = photoService.ProcessBase64Image(data.Base64Photo, 128, 128);

                context.Profiles.Update(profile);
                await context.SaveChangesAsync();
                return Ok(new { base64Photo = Convert.ToBase64String(profile.Photo) });
            }
            catch
            {
                return BadRequest("Неверная фотография профиля");
            }
        }

        [HttpPut]
        [Route("changeProfileName")]
        public async Task<IActionResult> ChangeProfileName([FromBody] ProfileName data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Вы ввели неверные данные");
            }
            var currentUserId = int.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var profile = await context.Profiles.FirstOrDefaultAsync(profile => profile.AccountId == currentUserId);
            profile.UserName = data.NewProfileName;
            context.Profiles.Update(profile);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
