using CsvHelper;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
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
        public FileParsingService()
        {
        }

        public async Task<FileParsingResult> ParseCsvFile(Stream stream)
        {
            using var streamReader = new StreamReader(stream);
            using var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            csv.Configuration.HasHeaderRecord = false;
            csv.Configuration.RegisterClassMap<CsvMap>();
            //due to how CsvHelper works, we must encapsulate parsing result inside container        
            var resultContainers = await csv.GetRecordsAsync<RecordParsingResultContainer>().ToListAsync();

            return new FileParsingResult
            {
                IsValid = !resultContainers.Any(c => !c.Result.IsValid),
                ValidTransactions = resultContainers.Where(c => c.Result.IsValid).Select(c => c.Result.Record),
                NotValidatedRecords = resultContainers.Where(c => !c.Result.IsValid).Select(c => c.Result.RawString)
            };
        }

        public async Task<FileParsingResult> ParseXmlFile(Stream stream)
        {
            XDocument xDocument;
            try
            {
                xDocument = await XDocument.LoadAsync(stream, LoadOptions.PreserveWhitespace, CancellationToken.None);
            }
            catch(XmlException ex)
            {
                return new FileParsingResult
                {
                    IsValid = false,
                    ParsingError = ex.Message
                };
            }
            var transactionXmlElements = xDocument.Root.Elements("Transaction");
            var xmlSerializer = new XmlSerializer(typeof(XmlRawTransactionRecord));

            var parsingResults = transactionXmlElements.Select(el =>
            {
                var rawRecord = (XmlRawTransactionRecord)xmlSerializer.Deserialize(el.CreateReader());
                bool isValid = rawRecord.TryConvertToTransactionRecord(out var record);
                return new RecordParsingResult
                {
                    IsValid = isValid,
                    Record = record,
                    RawString = el.ToString()
                };
            }).ToList();

            return new FileParsingResult
            {
                IsValid = !parsingResults.Any(r => !r.IsValid),
                ValidTransactions = parsingResults.Where(r => r.IsValid).Select(r => r.Record),
                NotValidatedRecords = parsingResults.Where(r => !r.IsValid).Select(r => r.RawString)
            };
        }

        public Task SaveTransactions(IEnumerable<TransactionRecord> transactions)
        {
            throw new NotImplementedException();
        }
    }
}
