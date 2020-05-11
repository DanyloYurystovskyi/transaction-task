using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionService.BLL.Mappers;
using TransactionService.BLL.Models;
using TransactionService.BLL.Services;
using TransactionService.DAL.Entities;

namespace TransactionService.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IDatabaseService _databaseService;
        private readonly IFileParsingService _fileParsingService;
        private readonly IFileParsingResultMapper _fileParsingResultMapper;

        private const string CONTENT_TYPE_CSV = "application/vnd.ms-excel";
        private const string CONTENT_TYPE_XML = "text/xml";

        public TransactionController(
            ILogger<TransactionController> logger, 
            IDatabaseService databaseService,
            IFileParsingService fileParsingService,
            IFileParsingResultMapper fileParsingResultMapper)
        {
            _logger = logger;
            _databaseService = databaseService;
            _fileParsingService = fileParsingService;
            _fileParsingResultMapper = fileParsingResultMapper;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            FileParsingResult fileContents = null;
            switch(file.ContentType)
            {
                case CONTENT_TYPE_CSV:
                    fileContents = await _fileParsingService.ParseCsvFile(file.OpenReadStream());
                    break;
                case CONTENT_TYPE_XML:
                    fileContents = await _fileParsingService.ParseXmlFile(file.OpenReadStream());
                    break;
                default:
                    return BadRequest("Unknown format");
            }

            if (fileContents.IsValid)
            {
                await _databaseService.SaveTransactionRecordsAsync(fileContents.ValidTransactions);
                return Ok();
            }
            else
            {
                var fileContentsDto = _fileParsingResultMapper.GetFileParsingResultFailureDTO(fileContents);
                return BadRequest(fileContentsDto);
            }
        }
    }
}