using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TransactionService.BLL.Extensions;
using TransactionService.BLL.Models.Csv;
using TransactionService.DAL.Entities;
using TransactionService.DAL.Entities.Enums;

namespace TransactionService.BLL.Models.Csv
{
    public class CsvRowParser : ITypeConverter
    {
        private const string DATE_FORMAT = "dd/MM/yyyy HH:mm:ss";

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            bool isValid = true;
            //TransactionId
            string transactionId = null;
            isValid = isValid && row.TryGetField(0, out transactionId) && transactionId.Length <= 50;
            //Amount
            decimal amount = 0m;
            isValid = isValid && row.TryGetField(1, out amount);
            //CurrencyCode
            string currency = null;
            isValid = isValid && row.TryGetField(2, out currency) && currency.IsISO4217();
            //Date
            DateTime date = new DateTime();
            isValid = isValid && TryParseTransactionDate(row, out date);
            //Status
            TransactionRecordStatus status = TransactionRecordStatus.None;
            isValid = isValid && TryParseTransactionRecordStatus(row, out status);

            return new CsvRowParsingResult
            {
                IsValid = isValid,
                RawRow = row.Context.RawRecord,
                Record = new TransactionRecord
                {
                    TransactionId = transactionId,
                    Amount = amount,
                    CurrencyCode = currency,
                    Date = date,
                    Status = status
                }
            };
        }

        private bool TryParseTransactionDate(IReaderRow row, out DateTime date)
        {
            date = new DateTime();
            return row.TryGetField(3, out string dateStr) 
                && DateTime.TryParseExact(
                    dateStr, 
                    DATE_FORMAT, 
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None, 
                    out date);
        }

        private bool TryParseTransactionRecordStatus(IReaderRow row, out TransactionRecordStatus status)
        {
            object statusObj = null;
            bool succesfullyParsed = row.TryGetField(4, out string statusStr)
                && Enum.TryParse(
                    typeof(CsvTransactionRecordStatus), 
                    statusStr, 
                    out statusObj);

            status = statusObj != null ? (TransactionRecordStatus)statusObj : TransactionRecordStatus.None;
            return succesfullyParsed;
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            throw new NotImplementedException();
        }
    }
}
