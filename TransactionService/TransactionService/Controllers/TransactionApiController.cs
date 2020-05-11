using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionService.BLL.Extensions;
using TransactionService.BLL.Mappers;
using TransactionService.BLL.Services;
using TransactionService.DAL.Entities.Enums;

namespace TransactionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionApiController : ControllerBase
    {
        private readonly ILogger<TransactionApiController> _logger;
        private readonly IDatabaseService _databaseService;
        private readonly ITransactionRecordMapper _transactionRecordMapper;

        public TransactionApiController(
            ILogger<TransactionApiController> logger,
            IDatabaseService databaseService,
            ITransactionRecordMapper transactionRecordMapper)
        {
            _logger = logger;
            _databaseService = databaseService;
            _transactionRecordMapper = transactionRecordMapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _databaseService.GetTransactionRecords();
            var dtos = transactions.Select(t => _transactionRecordMapper.GetTransactionRecordDTO(t));
            return Ok(dtos);
        }


        [HttpGet]
        [Route("currency")]
        public async Task<IActionResult> GetAllByCurrency(string currency)
        {
            if (!currency.IsISO4217())
            {
                return BadRequest("This is not ISO4217 currency code");
            }

            var transactions = await _databaseService.GetTransactionRecordsByCurrency(currency);
            var dtos = transactions.Select(t => _transactionRecordMapper.GetTransactionRecordDTO(t));
            return Ok(dtos);
        }

        [HttpGet]
        [Route("status")]
        public async Task<IActionResult> GetAllByStatus(string status)
        {
            if (!Enum.TryParse(typeof(TransactionRecordStatus), status, false, out var statusObj))
            {
                return BadRequest("This is not correct status: Approved, Failed or Finished");
            }

            var transactions = await _databaseService.GetTransactionRecordsByStatus((TransactionRecordStatus)statusObj);
            var dtos = transactions.Select(t => _transactionRecordMapper.GetTransactionRecordDTO(t));
            return Ok(dtos);
        }

        [HttpGet]
        [Route("daterange")]
        public async Task<IActionResult> GetAllByDateRange(DateTime start, DateTime end)
        {
            var transactions = await _databaseService.GetTransactionRecordsByDateRange(start, end);
            var dtos = transactions.Select(t => _transactionRecordMapper.GetTransactionRecordDTO(t));
            return Ok(dtos);
        }
    }
}