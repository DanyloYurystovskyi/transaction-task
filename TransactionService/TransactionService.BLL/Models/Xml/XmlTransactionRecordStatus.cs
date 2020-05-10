using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionService.BLL.Models.Xml
{
    public enum XmlTransactionRecordStatus
    {
        None = 'N',
        Approved = 'A',
        Rejected = 'R',
        Done = 'D'
    }
}
