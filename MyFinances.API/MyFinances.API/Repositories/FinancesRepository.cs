using MyFinances.API.Data;
using MyFinances.API.Interfaces;
using System;
using System.Threading.Tasks;
using System.Linq;
using MyFinances.API.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyFinances.API.Dto;
using System.Globalization;

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
            var result = await _context.Operations.GroupBy(o => o.User.Id)
                   .Select(g => new { user = g.Key, total = g.Sum(i => i.Price) }).Where(op => op.user == userId).FirstOrDefaultAsync();

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

        public async Task AddCategory(Category category)
        {
            if (category == null)
                return;
            await _context.Categories.AddAsync(category);
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
            List<Operation> operations = await _context.Operations.Where(o => o.User.Id == userId && o.DateOperation.Month == month && o.DateOperation.Year == year).ToListAsync();
            return operations;
        }

        public async Task<List<Operation>> GetOperations(Guid userId)
        {
            List<Operation> operations = await _context.Operations.Where(o => o.User.Id == userId).ToListAsync();
            return operations;
        }

        public async Task<List<Category>> GetCategories(Guid userId)
        {
            List<Category> categories = await _context.Categories.Where(o => o.User.Id == userId).ToListAsync();
            return categories;
        }        
        
        public async Task<Category> GetCategory(Guid userId)
        {
            return await _context.Categories.FirstOrDefaultAsync(o => o.User.Id == userId);
        }

        public async Task<LastTenOperations> GetLastTenOperations(Guid UserId)
        {
            LastTenOperations lastTenOperations = new LastTenOperations();

            lastTenOperations.Expenses = await _context.Operations.OrderByDescending(i => i.Created).Include(x => x.Category).Where(o => o.User.Id == UserId && o.Price < 0).Take(10).ToListAsync();
            lastTenOperations.Income = await _context.Operations.OrderByDescending(i => i.Created).Include(x => x.Category).Where(o => o.User.Id == UserId && o.Price > 0).Take(10).ToListAsync();

            return lastTenOperations;
        }

        public async Task<bool> DeleteOperation(Guid userId, Guid operationId)
        {
            Operation operation = _context.Operations.FirstOrDefault(x => x.Id == operationId && x.User.Id == userId);
            if (operation == null)
                throw new Exception("Brak operacji");

            _context.Remove(operation);
            
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Statistic> GetStatistic(Guid userId)
        {
            DateTime today = DateTime.Now;
            Statistic statistic = new Statistic();

            for (int x = 0; x < 6; x++)
            {
                MonthSaldo saldo = await GetMonthSaldo(userId, today.Month, today.Year);
                statistic.Expenses.Add(Math.Abs(saldo.Expense));
                statistic.Income.Add(saldo.Income);
                statistic.Date.Add(today.ToString("yyyy-MM"));
                today = today.AddMonths(-1);
            }

            return statistic;
        }
    }
}
