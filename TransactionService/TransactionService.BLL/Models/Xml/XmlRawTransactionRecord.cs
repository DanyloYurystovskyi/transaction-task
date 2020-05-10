using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TransactionService.BLL.Models.Xml
{
    [XmlType("Transaction")]
    public class XmlRawTransactionRecord
    {
        [XmlAttribute("id")]
        public string id { get; set; }

        public string TransactionDate { get; set; }

        public PaymentDetails PaymentDetails { get; set; }

        public string Status { get; set; }
    }

    [XmlType("PaymentDetails")]
    public struct PaymentDetails
    {
        public string Amount { get; set; }

        public string CurrencyCode { get; set; }
    }
}
