using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFinances.API.Dto;
using MyFinances.API.Interfaces;
using MyFinances.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFinances.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FinancesController : ControllerBase
    {
        private readonly IFinancesRepository _financesRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public FinancesController(IFinancesRepository financesRepository, IMapper mapper, IAuthRepository authRepository)
        {
            _financesRepository = financesRepository;
            _authRepository = authRepository;
            _mapper = mapper;
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

        [HttpGet("getStatistic")]
        public async Task<Statistic> GetStatistic(Guid userId)
        {
            return await _financesRepository.GetStatistic(userId);
        }

        [HttpPost("addOperation")]
        public async Task<IActionResult> AddOperation([FromBody] AddOperationDto operationDto)
        {
            Operation operation = _mapper.Map<Operation>(operationDto);
            operation.User = await _authRepository.GetUser(new Guid(operationDto.UserId));
            operation.Category = await _financesRepository.GetCategory(new Guid(operationDto.UserId));

            await _financesRepository.AddOperation(operation);

            return Ok();
        }

        [HttpPost("addCategory")]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryDto categoryDto)
        {
            Category category = _mapper.Map<Category>(categoryDto);
            category.User = await _authRepository.GetUser(categoryDto.UserId);

            await _financesRepository.AddCategory(category);

            return Ok();
        }

        [HttpGet("getOperations")]
        public async Task<List<Operation>> GetOperations(Guid userId)
        {
            return await _financesRepository.GetOperations(userId);
        }

        [HttpGet("getLastTenOperations")]
        public async Task<LastTenOperations> GetLastTenOperations(Guid userId)
        {
            return await _financesRepository.GetLastTenOperations(userId);
        }

        [HttpGet("getCategories")]
        public async Task<List<Category>> GetCategories(Guid userId)
        {
            return await _financesRepository.GetCategories(userId);
        }

        [HttpDelete("deleteOperation")]
        public async Task<IActionResult> DeleteOperation(Guid userId, Guid operationId)
        {
            bool isDelete =  await _financesRepository.DeleteOperation(userId, operationId);
            if (isDelete) return NoContent(); 
            
            return BadRequest("Błąd! nie udało się usunąć operacji!");
        }

    }
}
