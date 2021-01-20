using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchShop.Data.Models;

namespace WatchShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        WebBanDongHoContext Init();
    }
}
