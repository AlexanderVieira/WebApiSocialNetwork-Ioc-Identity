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
    public class CountryService : BaseService<Country>, ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository) : base(countryRepository)
        {
            _countryRepository = countryRepository;
        }
    }
}
