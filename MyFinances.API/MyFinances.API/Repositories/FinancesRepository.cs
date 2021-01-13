using MyFinances.API.Data;
using MyFinances.API.Interfaces;
using System;
using System.Threading.Tasks;
using System.Linq;
using MyFinances.API.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyFinances.API.Dto;

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
            var result = _context.Operations.GroupBy(o => o.User.Id)
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
                if(o.Price > 0)
                {
                    saldo.Income += o.Price;
                } else
                {
                    saldo.Expense += o.Price;
                }
            });

            return saldo;
        }

        private async Task<List<Operation>> GetUserOperations(Guid userId, int month, int year)
        {
            List<Operation> operations = await _context.Operations.Where(o => o.User.Id == userId && o.Created.Month == month && o.Created.Year == year).ToListAsync();
            return operations;
        }

        public async Task<List<Operation>> GetOperations(Guid userId)
        {
            List<Operation> operations = await _context.Operations.Where(o => o.User.Id == userId).ToListAsync();
            return operations;
        }

        public async Task<LastTenOperations> GetLastTenOperations(Guid UserId)
        {
            LastTenOperations lastTenOperations = new LastTenOperations();
            try
            {
                lastTenOperations.Expenses = await _context.Operations.OrderBy(i => i.Created).Where(o => o.User.Id == UserId && o.Price < 0).Take(10).ToListAsync();
                lastTenOperations.Income = await _context.Operations.OrderBy(i => i.Created).Where(o => o.User.Id == UserId && o.Price > 0).Take(10).ToListAsync();
            }
            catch(Exception e)
            {
                var d = e;
            }

            return lastTenOperations;
        }
    }
}
