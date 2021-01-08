namespace MyFinances.API.Models
{
    public class MonthSaldo
    {
        public float Saldo { get; set; }
        public float Expense { get; set; }
        public float Income { get; set; }

        public MonthSaldo() { }
        public MonthSaldo(float saldo, float expanse, float income)
        {
            Saldo = saldo;
            Expense = expanse;
            Income = income;
        }
    }
}
