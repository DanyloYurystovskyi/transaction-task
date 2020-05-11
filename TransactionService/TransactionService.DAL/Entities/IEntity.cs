using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionService.DAL.Entities
{
    public interface IEntity<T>
    {
        T Id
        {
            get;
            set;
        }
    }
}
