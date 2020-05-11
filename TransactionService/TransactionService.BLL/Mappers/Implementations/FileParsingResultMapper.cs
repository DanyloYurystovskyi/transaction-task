using System;
using System.Collections.Generic;
using System.Text;
using TransactionService.BLL.DTOs;
using TransactionService.BLL.Models;

namespace TransactionService.BLL.Mappers.Implementations
{
    public class FileParsingResultMapper : IFileParsingResultMapper
    {
        private ITransactionRecordMapper _transactionRecordMapper;

        public FileParsingResultMapper(ITransactionRecordMapper transactionRecordMapper)
        {
            _transactionRecordMapper = transactionRecordMapper;
        }

        public FileParsingResultFailureDTO GetFileParsingResultFailureDTO(FileParsingResult input)
        {
            return new FileParsingResultFailureDTO
            {
                IsValid = input.IsValid,
                NotValidatedRecords = input.NotValidatedRecords,
                ParsingError = input.ParsingError
            };
        }
    }
}
