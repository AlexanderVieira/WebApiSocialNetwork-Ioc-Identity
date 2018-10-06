using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Repositories;

namespace WebAPI.DAL.Repositories
{
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {

    }
}
