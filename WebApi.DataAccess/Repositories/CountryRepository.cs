using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces.Repositories;

namespace WebAPI.DataAccess.Repositories
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {

    }
}
