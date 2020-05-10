﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TransactionService.BLL.Models;
using TransactionService.BLL.Services;
using TransactionService.DAL.Entities;

namespace TransactionService.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IFileParsingService _fileParsingService;

        private const string CONTENT_TYPE_CSV = "application/vnd.ms-excel";
        private const string CONTENT_TYPE_XML = "text/xml";

        public TransactionController(ILogger<TransactionController> logger, IFileParsingService fileParsingService)
        {
            _logger = logger;
            _fileParsingService = fileParsingService;
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
                return Ok();
            }
            else
            {
                //mapper!
                return BadRequest(fileContents.NotValidatedRecords);
            }
        }
    }
}