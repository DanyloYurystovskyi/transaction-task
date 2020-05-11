using System;
using System.Collections.Generic;
using System.Text;
using TransactionService.BLL.DTOs;
using TransactionService.DAL.Entities;

namespace TransactionService.BLL.Mappers
{
    public interface ITransactionRecordMapper
    {
        TransactionRecordDTO GetTransactionRecordDTO(TransactionRecord record);
    }
}
