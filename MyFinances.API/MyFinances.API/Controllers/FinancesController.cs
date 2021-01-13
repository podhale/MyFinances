using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinances.API.Interfaces;
using MyFinances.API.Models;

namespace MyFinances.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinancesController : ControllerBase
    {
        private readonly IFinancesRepository _financesRepository;

        public FinancesController(IFinancesRepository financesRepository)
        {
            _financesRepository = financesRepository;
        }

        [HttpGet("saldo")]
        public async Task<float> GetSaldoForUser(Guid userId)
        {
            return await _financesRepository.GetSaldo(userId);
        }

        [HttpGet("monthSaldo")]
        public async Task<MonthSaldo> GetSaldoMonthForUser(Guid userId, int month, int year)
        {
            return await _financesRepository.GetMonthSaldo(userId, month, year);
        }

        [HttpPost("addOperation")]
        public async Task<IActionResult> AddOperation(Operation operation)
        {
            await _financesRepository.AddOperation(operation);
            return Ok();
        }

        [HttpGet("getOperations")]
        public async Task<List<Operation>> GetOperations(Guid userId)
        {
            return await _financesRepository.GetOperations(userId);
        }

    }
}
