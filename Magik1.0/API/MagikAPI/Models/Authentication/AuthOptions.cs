using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MagikAPI.Models.Authentication
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int TokenLifetime { get; set; } // seconds
        public int LongTokenLifetime { get; set; }
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
