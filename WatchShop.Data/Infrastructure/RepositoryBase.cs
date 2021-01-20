using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WatchShop.Data.Models;

namespace WatchShop.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        private WebBanDongHoContext dbContext;
        private readonly IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected WebBanDongHoContext DbContext
        {
            get { return dbContext ?? (dbContext = DbFactory.Init()); }
        }

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        public virtual void Add(T model)
        {
            dbSet.Add(model);
        }

        public virtual void Update(T model)
        {
            dbSet.Attach(model);
            dbContext.Entry(model).State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
        }

        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }
    }
}
