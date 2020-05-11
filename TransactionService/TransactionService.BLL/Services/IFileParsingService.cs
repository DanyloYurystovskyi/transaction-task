using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TransactionService.BLL.Models;
using TransactionService.DAL.Entities;

namespace TransactionService.BLL.Services
{
    public interface IFileParsingService
    {
        Task<FileParsingResult> ParseCsvFile(Stream stream);

        Task<FileParsingResult> ParseXmlFile(Stream stream);
    }
}
