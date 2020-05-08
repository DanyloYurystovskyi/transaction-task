using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TransactionService.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
