using System;
using System.Collections.Generic;
using System.Text;
using TransactionService.DAL.Entities;

namespace TransactionService.BLL.Models.Csv
{
    public class CsvRowParsingResult
    {
        public bool IsValid { get; set; }

        public string RawRow { get; set; }

        public TransactionRecord Record { get; set; }
    }
}
