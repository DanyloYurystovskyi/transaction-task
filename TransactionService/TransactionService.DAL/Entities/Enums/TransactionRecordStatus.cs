using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionService.DAL.Entities.Enums
{
    public enum TransactionRecordStatus
    {
        None = 'N',
        Approved = 'A',
        Failed = 'R', //Rejected
        Finished = 'D' //Done
    }
}
