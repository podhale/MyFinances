using MyFinances.API.Models;
using System.Collections.Generic;

namespace MyFinances.API.Dto
{
    public class LastTenOperations
    {
        public List<Operation> Expenses { get; set; }
        public List<Operation> Income { get; set; }
    }
}
