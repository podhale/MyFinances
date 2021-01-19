using System.Collections.Generic;

namespace MyFinances.API.Dto
{
    public class Statistic
    {
        public List<string> Date { get; set; }
        public List<float> Income { get; set; }
        public List<float> Expenses { get; set; }
    }
}
