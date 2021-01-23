using System;

namespace MyFinances.API.Dto
{
    public class AddCategoryDto
    {
        public Guid UserId { get; set; }
        public string NameCategory { get; set; }
    }
}
