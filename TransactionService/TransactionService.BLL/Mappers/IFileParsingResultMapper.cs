using System;
using System.Collections.Generic;
using System.Text;
using TransactionService.BLL.DTOs;
using TransactionService.BLL.Models;

namespace TransactionService.BLL.Mappers
{
    public interface IFileParsingResultMapper
    {
        FileParsingResultFailureDTO GetFileParsingResultFailureDTO(FileParsingResult input);
    }
}
