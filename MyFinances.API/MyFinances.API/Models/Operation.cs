using System;

namespace MyFinances.API.Models
{
    public class Operation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime Created { get; set; }
        public virtual User User { get; set; }
        public virtual Category Category { get; set; }

    }
}
