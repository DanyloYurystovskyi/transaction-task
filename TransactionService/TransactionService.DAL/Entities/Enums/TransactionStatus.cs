using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionService.DAL.Entities.Enums
{
    public enum TransactionStatus
    {
        Approved = 'A',
        Failed = 'R', //Rejected
        Finshed = 'D' //Done
    }
}
