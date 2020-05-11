using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionService.BLL.DTOs
{
    public class FileParsingResultFailureDTO
    {
        public bool IsValid { get; set; }

        public string ParsingError { get; set; }

        public IEnumerable<string> NotValidatedRecords { get; set; }
    }
}
