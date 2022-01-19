using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.UIModels
{
    public class Auth
    {
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(5)]
        public string Password { get; set; }
    }
}
