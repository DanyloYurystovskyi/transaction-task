using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Transactions;
using TransactionService.BLL.Extensions;
using TransactionService.DAL.Entities;
using TransactionService.DAL.Entities.Enums;

namespace TransactionService.BLL.Models.Csv
{
    public sealed class CsvMap : ClassMap<RecordParsingResultContainer>
    {
        public CsvMap()
        {
            Map(m => m.Result).TypeConverter<CsvRowParser>();
        }
    }
}
