using System;
using System.Collections.Generic;

namespace MyFinances.API.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Operation> Collections { get; set; }
    }
}
