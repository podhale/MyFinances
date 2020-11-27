using MyFinances.API.Data;
using MyFinances.API.Interfaces;
using System;
using System.Threading.Tasks;
using System.Linq;
using MyFinances.API.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyFinances.API.Repositories
{
    public class FinancesRepository : IFinancesRepository
    {
        private readonly DataContext _context;

        public FinancesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<float> GetSaldo(Guid userId)
        {
            var result = _context.Operations.GroupBy(o => o.UserId)
                   .Select(g => new { user = g.Key, total = g.Sum(i => i.Price) }).Where(op => op.user == userId).FirstOrDefault();

            if (result == null)
                return 0.0f;

            return result.total;
        }

        public async Task AddOperation(Operation operation)
        {
            if (operation == null)
                return;
            operation.Created = DateTime.Now;
            await _context.Operations.AddAsync(operation);
            await _context.SaveChangesAsync();
        }

        public async Task<MonthSaldo> GetMonthSaldo(Guid userId, int month, int year)
        {
            return await CountMonthSaldo(userId, month, year);
        }

        private async Task<MonthSaldo> CountMonthSaldo(Guid userId, int month, int year)
        {
            List<Operation> operations = await GetUserOperations(userId, month, year);
            MonthSaldo saldo = new MonthSaldo(0,0,0);
            operations.ForEach(o =>
            {
                saldo.Saldo += o.Price;
                if(o.NameOperation == "DOCHOD")
                {
                    saldo.Income += o.Price;
                } else
                {
                    saldo.Expense += o.Price;
                }
            });

            return saldo;
        }

        private async Task<List<Operation>> GetUserOperations(Guid userId, string nameOperation, int month, int year)
        {
            return await _context.Operations.Where(o => o.UserId == userId && o.NameOperation == nameOperation && o.Created.Month == month && o.Created.Year == year).ToListAsync();
        }
        private async Task<List<Operation>> GetUserOperations(Guid userId, int month, int year)
        {
            return await _context.Operations.Where(o => o.UserId == userId && o.Created.Month == month && o.Created.Year == year).ToListAsync();
        }

        private async Task<List<Operation>> GetUserOperations(Guid userId)
        {
            return await _context.Operations.Where(o => o.UserId == userId).ToListAsync();
        }


    }
}
