using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TransactionService.DAL.UnitOfWork.Implementations
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly TransactionServiceContext _context;

        public EntityFrameworkUnitOfWork(TransactionServiceContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
