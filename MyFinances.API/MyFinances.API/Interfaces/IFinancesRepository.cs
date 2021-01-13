using MyFinances.API.Dto;
using MyFinances.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFinances.API.Interfaces
{
    public interface IFinancesRepository
    {
        public Task<float> GetSaldo(Guid userId);
        public Task<MonthSaldo> GetMonthSaldo(Guid userId, int month, int year);
        public Task AddOperation(Operation operation);
        public Task<List<Operation>> GetOperations(Guid userId);
        public Task<LastTenOperations> GetLastTenOperations(Guid UserId);
    }
}
