using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionService.DAL.Entities.Enums
{
    public enum TransactionRecordStatus
    {
        Approved = 'A',
        Rejected = 'R',
        Done = 'D'
    }
}
