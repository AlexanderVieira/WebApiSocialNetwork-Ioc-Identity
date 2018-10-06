using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces.Repositories;
using WebAPI.Domain.Interfaces.Services;

namespace WebApi.Domain.Services
{
    public class CountryProcedureService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryProcedureService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public void Delete(Guid id)
        {
            _countryRepository.Delete(id);
        }

        public void Dispose()
        {
            _countryRepository.Dispose();
        }

        public IEnumerable<Country> GetAll()
        {
            return _countryRepository.GetAll();
        }

        public Country GetById(Guid id)
        {
            return _countryRepository.GetById(id);
        }

        public void Save(Country obj)
        {
            _countryRepository.Save(obj);
        }

        public void Update(Country obj)
        {
            _countryRepository.Update(obj);
        }
    }
}
