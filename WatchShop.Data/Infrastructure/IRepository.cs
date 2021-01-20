using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WatchShop.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T model);
        void Update(T model);
        void Delete(int id);
        T GetById(int id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    }
}