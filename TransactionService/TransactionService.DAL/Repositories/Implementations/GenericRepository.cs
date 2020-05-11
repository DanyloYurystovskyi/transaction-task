using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TransactionService.DAL.Entities;

namespace TransactionService.DAL.Repositories.Implementations
{
    public class GenericRepository<T, ID> : IGenericRepository<T, ID> where T : class, IEntity<ID>
    {
        private TransactionServiceContext _context;

        public GenericRepository(TransactionServiceContext context)
        {
            _context = context;
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return  _context.Set<T>().FirstOrDefault(predicate);
        }

        public void Create(T entity)
        {
             _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public void CreateOrUpdate(T entity)
        {
            bool exists = _context.Set<T>().Any(e => e.Id.Equals(entity.Id));
            if (exists)
            {
                Update(entity);
            }
            else
            {
                Create(entity);
            }
        }
    }
}
