﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionService.BLL.DTOs
{
    public class TransactionDTO
    {
        public string Id { get; set; }

        public string Payment { get; set; }

        public char TransactionStatus { get; set; }
    }
}
