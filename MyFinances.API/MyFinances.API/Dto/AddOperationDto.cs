using System;

namespace MyFinances.API.Dto
{
    public class AddOperationDto
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string CategoryId { get; set; }
        public float Price { get; set; }
        public string DateOperation { get; set; }
    }
}
