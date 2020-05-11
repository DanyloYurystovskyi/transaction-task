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

        public static bool TryConvertToTransactionRecord(this XmlRawTransactionRecord rawRecord, out TransactionRecord record)
        {
            //TransactionId
            bool isValid = rawRecord.id != null && rawRecord.id.Length <= 50;
            //Amount
            decimal amount = default;
            isValid = isValid && decimal.TryParse(rawRecord.PaymentDetails.Amount, out amount);
            //CurrencyCode
            isValid = isValid && rawRecord.PaymentDetails.CurrencyCode.IsISO4217();
            //Date
            DateTime date = default;
            isValid = isValid && TryParseTransactionDate(rawRecord.TransactionDate, out date);
            //Status
            var status = TransactionRecordStatus.None;
            isValid = isValid && TryParseTransactionRecordStatus(rawRecord.Status, out status);

            record = isValid ? new TransactionRecord
            {
                TransactionId = rawRecord.id,
                Amount = amount,
                CurrencyCode = rawRecord.PaymentDetails.CurrencyCode,
                Date = date,
                Status = status
            } : null;

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
