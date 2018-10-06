using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Repositories;
using WebAPI.BLL.Interfaces.Services;

namespace WebAPI.BLL.Services
{
    public class MarketplaceService : BaseService<Marketplace>, IMarketplaceService
    {
        private readonly IMarketplaceRepository _marketplaceRepository;

        public MarketplaceService(IMarketplaceRepository marketplaceRepository) : base(marketplaceRepository)
        {
            _marketplaceRepository = marketplaceRepository;
        }

        public IEnumerable<Marketplace> GetByName(Marketplace marketplace)
        {
            return _marketplaceRepository.GetByName(marketplace);
        }
    }
}
