using CsvHelper;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionService.BLL.Models;
using TransactionService.BLL.Models.Csv;
using TransactionService.DAL.Entities;
using TransactionService.DAL.Repositories;
using TransactionService.DAL.UnitOfWork;

namespace TransactionService.BLL.Services.Implementations
{
    public class TransactionDataService : ITransactionDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TransactionRecord> _transactionRepository;

        public TransactionDataService(IUnitOfWork unitOfWork, IGenericRepository<TransactionRecord> transactionRepository)
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
                Transactions = resultContainers.Where(c => c.Result.IsValid).Select(c => c.Result.Record),
                ValidationMessage = string.Join(string.Empty, 
                    resultContainers.Where(c => !c.Result.IsValid).Select(c => c.Result.RawRow))
            };
        }

        public Task<FileParsingResult> ParseXmlFile(Stream stream)
        {
            throw new NotImplementedException();
        }

        public Task SaveTransactions(IEnumerable<TransactionRecord> transactions)
        {
            throw new NotImplementedException();
        }
    }
}
