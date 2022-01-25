using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Auth.Models
{
    public class Account
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [StringLength(20, MinimumLength = 20)]
        public string PasswordHash { get; set; }

        public Role[] Roles { get; set; }
    }

    public enum Role
    {
        User,
        Admin
    }
}
