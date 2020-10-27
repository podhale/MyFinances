using System;
using System.Security.Claims;

namespace MyFinances.API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public ClaimsIdentity Username { get; internal set; }

        public User()
        {
                
        }
        public User(string email)
        {
            Email = email;
        }
    }
}
