using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchShop.Data.Models;

namespace WatchShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private WebBanDongHoContext dbContext;
        public WebBanDongHoContext Init()
        {
            return dbContext ?? (dbContext = new WebBanDongHoContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
