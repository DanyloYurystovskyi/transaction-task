using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransactionService.DAL.Entities;
using TransactionService.DAL.Entities.Enums;

namespace TransactionService.BLL.Services
{
    public interface IDatabaseService
    {
        Task SaveTransactionRecordsAsync(IEnumerable<TransactionRecord> records);
        Task<IEnumerable<TransactionRecord>> GetTransactionRecordsByCurrency(string currency);
        Task<IEnumerable<TransactionRecord>> GetTransactionRecordsByDateRange(DateTime? start, DateTime? end);
        Task<IEnumerable<TransactionRecord>> GetTransactionRecordsByCurrency(TransactionRecordStatus status);

    }
}
