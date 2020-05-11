using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TransactionService.DAL.Repositories
{
    public interface IGenericRepository<T, ID>
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        T Get(Expression<Func<T, bool>> predicate);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void CreateOrUpdate(T entity);
    }
}
