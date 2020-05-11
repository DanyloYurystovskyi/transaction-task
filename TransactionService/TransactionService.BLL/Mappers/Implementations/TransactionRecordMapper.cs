using System;
using System.Collections.Generic;
using System.Text;
using TransactionService.BLL.DTOs;
using TransactionService.DAL.Entities;

namespace TransactionService.BLL.Mappers.Implementations
{
    public class TransactionRecordMapper : ITransactionRecordMapper
    {
        public TransactionRecordDTO GetTransactionRecordDTO(TransactionRecord record)
        {
            return new TransactionRecordDTO
            {
                Id = record.Id,
                Payment = $"{record.Amount} {record.CurrencyCode}",
                TransactionStatus = (char)record.Status
            };
        }
    }
}
