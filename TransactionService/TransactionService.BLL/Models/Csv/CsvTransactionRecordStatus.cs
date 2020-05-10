using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionService.BLL.Models.Csv
{
    public enum CsvTransactionRecordStatus
    {
        Approved = 'A',
        Failed = 'R',
        Finished = 'D'
    }
}
