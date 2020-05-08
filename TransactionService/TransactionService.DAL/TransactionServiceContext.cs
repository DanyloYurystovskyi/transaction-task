using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TransactionService.DAL.Entities;

namespace TransactionService.DAL
{
    public class TransactionServiceContext : DbContext
    {
        public TransactionServiceContext(
            DbContextOptions<TransactionServiceContext> options)
            : base (options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
