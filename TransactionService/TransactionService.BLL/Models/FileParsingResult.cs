using System;
using System.Collections.Generic;
using System.Text;
using TransactionService.DAL.Entities;

namespace TransactionService.BLL.Models
{
    public class FileParsingResult
    {
        public bool IsValid { get; set; }

        public IEnumerable<TransactionRecord> Transactions { get; set; }

        public string ValidationMessage { get; set; }
    }
}
