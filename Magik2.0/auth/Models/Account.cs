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
        public string? Id { get; set; }

        [EmailAddress]
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public Role[] Roles { get; set; } = null!;
    }

    public enum Role
    {
        User,
        Admin
    }
}
