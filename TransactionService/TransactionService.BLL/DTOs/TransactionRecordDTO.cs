using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionService.BLL.DTOs
{
    public class TransactionRecordDTO
    {
        public string Id { get; set; }

        public string Payment { get; set; }

        public char Status { get; set; }
    }
}
