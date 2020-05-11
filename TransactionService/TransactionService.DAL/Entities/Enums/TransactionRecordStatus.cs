using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionService.DAL.Entities.Enums
{
    public enum TransactionRecordStatus
    {
        None = 'N',
        Approved = 'A',
        Rejected = 'R',
        Done = 'D'
    }
}
