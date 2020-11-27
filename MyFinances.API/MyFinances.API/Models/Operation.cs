using System;

namespace MyFinances.API.Models
{
    public class Operation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public float Price { get; set; }
        public string NameOperation { get; set; }
        public DateTime Created { get; set; }
    }
}
