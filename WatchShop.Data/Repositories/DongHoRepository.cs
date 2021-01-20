using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchShop.Data.Infrastructure;
using WatchShop.Data.Models;

namespace WatchShop.Data.Repositories
{
    public interface IDongHoRepository : IRepository<DongHo>
    {
    }
    public class DongHoRepository : RepositoryBase<DongHo>, IDongHoRepository
    {
        public DongHoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
