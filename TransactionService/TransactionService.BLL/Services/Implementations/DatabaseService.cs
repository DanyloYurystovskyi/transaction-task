using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransactionService.DAL.Entities;
using TransactionService.DAL.Entities.Enums;
using TransactionService.DAL.Repositories;
using TransactionService.DAL.UnitOfWork;

namespace TransactionService.BLL.Services.Implementations
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TransactionRecord, string> _transactionRecordRepository;

        public DatabaseService(IUnitOfWork unitOfWork, IGenericRepository<TransactionRecord, string> transactionRecordRepository)
        {
            _unitOfWork = unitOfWork;
            _transactionRecordRepository = transactionRecordRepository;
        }

        public async Task SaveTransactionRecordsAsync(IEnumerable<TransactionRecord> records)
        {
            foreach (var record in records)
            {
                _transactionRecordRepository.CreateOrUpdate(record);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionRecord>> GetTransactionRecordsByCurrency(string currency)
        {
            return _transactionRecordRepository.GetAll(r => r.CurrencyCode == currency);
        }

        public async Task<IEnumerable<TransactionRecord>> GetTransactionRecordsByStatus(TransactionRecordStatus status)
        {
            return _transactionRecordRepository.GetAll(r => r.Status == status);
        }

        public async Task<IEnumerable<TransactionRecord>> GetTransactionRecordsByDateRange(DateTime start, DateTime end)
        {
            return _transactionRecordRepository.GetAll(r => r.Date >= start && r.Date <= end);
        }

        public async Task<IEnumerable<TransactionRecord>> GetTransactionRecords()
        {
            return _transactionRecordRepository.GetAll(r => true);
        }
    }
}
