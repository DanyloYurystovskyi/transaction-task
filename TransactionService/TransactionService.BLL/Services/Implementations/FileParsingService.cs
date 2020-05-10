using CsvHelper;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TransactionService.BLL.Extensions;
using TransactionService.BLL.Models;
using TransactionService.BLL.Models.Csv;
using TransactionService.BLL.Models.Xml;
using TransactionService.DAL.Entities;
using TransactionService.DAL.Repositories;
using TransactionService.DAL.UnitOfWork;

namespace TransactionService.BLL.Services.Implementations
{
    public class FileParsingService : IFileParsingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TransactionRecord> _transactionRepository;

        public FileParsingService(IUnitOfWork unitOfWork, IGenericRepository<TransactionRecord> transactionRepository)
        {
            _unitOfWork = unitOfWork;
            _transactionRepository = transactionRepository;
        }

        public async Task<FileParsingResult> ParseCsvFile(Stream stream)
        {
            using var streamReader = new StreamReader(stream);
            using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            csv.Configuration.HasHeaderRecord = false;
            csv.Configuration.RegisterClassMap<CsvMap>();
            //due to how CsvHelper works, we must encapsulate parsing result inside container        
            var resultContainers = await csv.GetRecordsAsync<CsvRowParsingResultContainer>().ToListAsync();

            return new FileParsingResult
            {
                IsValid = !resultContainers.Any(c => !c.Result.IsValid),
                ValidTransactions = resultContainers.Where(c => c.Result.IsValid).Select(c => c.Result.Record),
                NotValidatedRecords = resultContainers.Where(c => !c.Result.IsValid).Select(c => c.Result.RawRow)
            };
        }

        public async Task<FileParsingResult> ParseXmlFile(Stream stream)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<XmlRawTransactionRecord>), new XmlRootAttribute("Transactions"));
            var xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.Async = true;
            var xmlReader = XmlReader.Create(stream, xmlReaderSettings);

            try
            {
                var rawRecords = (List<XmlRawTransactionRecord>)xmlSerializer.Deserialize(xmlReader);
                var validTransactions = rawRecords
                    .Where(r => r.IsValid())
                    .Select(r => r.ConvertToTransactionRecord())
                    .ToList();
                return new FileParsingResult
                {
                    IsValid = rawRecords.Count == validTransactions.Count,
                    ValidTransactions = validTransactions
                };
            }
            catch (InvalidOperationException ex)
            {
                return new FileParsingResult
                {
                    IsValid = false,
                    ErrorMessage = ex.Message,
                    ValidTransactions = { },
                    NotValidatedRecords = new string[] { await xmlReader.ReadElementContentAsStringAsync() },
                };
            }
        }

        public Task SaveTransactions(IEnumerable<TransactionRecord> transactions)
        {
            throw new NotImplementedException();
        }
    }
}
