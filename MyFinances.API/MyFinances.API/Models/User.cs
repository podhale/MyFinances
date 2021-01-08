using System;
using System.Collections.Generic;

namespace MyFinances.API.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        public User(string email)
        {
            Email = email;
        }
    }
}
