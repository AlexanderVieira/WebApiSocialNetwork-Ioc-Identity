using System.Collections.Generic;
using System.Linq;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Repositories;

namespace WebAPI.DAL.Repositories
{
    public class MarketplaceRepository : BaseRepository<Marketplace>, IMarketplaceRepository
    {
        public IEnumerable<Marketplace> GetByName(Marketplace marketplace)
        {
            return _ctx.Marketplaces.ToList().Where(m => m.Title == marketplace.Title).OrderBy(m => m.Title);
        }
    }
}
