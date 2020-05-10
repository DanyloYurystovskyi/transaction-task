using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using TransactionService.BLL.Extensions;
using TransactionService.BLL.Models.Xml;
using TransactionService.DAL.Entities;
using TransactionService.DAL.Entities.Enums;

namespace TransactionService.BLL.Extensions
{
    public static class XmlRawTransactionRecordExtensions
    {
        private const string DATE_FORMAT = "yyyy-MM-ddTHH:mm:ss";

        public static TransactionRecord ConvertToTransactionRecord(this XmlRawTransactionRecord rawRecord)
        {
            return new TransactionRecord
            {
                TransactionId = rawRecord.id,
                Amount = Convert.ToDecimal(rawRecord.PaymentDetails.Amount),
                CurrencyCode = rawRecord.PaymentDetails.CurrencyCode,
                Date = DateTime.ParseExact(rawRecord.TransactionDate, DATE_FORMAT, CultureInfo.InvariantCulture),
                Status = (TransactionRecordStatus)Enum.Parse(typeof(XmlTransactionRecordStatus), rawRecord.Status)
            };
        }

        public static bool IsValid(this XmlRawTransactionRecord rawRecord)
        {
            //TransactionId
            bool isValid = rawRecord.id != null && rawRecord.id.Length <= 50;
            //Amount
            isValid = isValid && decimal.TryParse(rawRecord.PaymentDetails.Amount, out _);
            //CurrencyCode
            isValid = isValid && rawRecord.PaymentDetails.CurrencyCode.IsISO4217();
            //Date
            isValid = isValid && TryParseTransactionDate(rawRecord.TransactionDate, out _);
            //Status
            isValid = isValid && TryParseTransactionRecordStatus(rawRecord.Status, out _);

            return isValid;
        }

        private static bool TryParseTransactionDate(string dateStr, out DateTime date)
        {
            date = new DateTime();
            return DateTime.TryParseExact(
                dateStr,
                DATE_FORMAT,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date);
        }

        private static bool TryParseTransactionRecordStatus(string value, out TransactionRecordStatus status)
        {
            object statusObj = null;
            bool succesfullyParsed = Enum.TryParse(
                typeof(XmlTransactionRecordStatus),
                value,
                out statusObj);

            status = statusObj != null ? (TransactionRecordStatus)statusObj : TransactionRecordStatus.None;
            return succesfullyParsed;
        }

    }
}
