using System;
using System.Collections.Generic;
using WebAPI.BLL.Interfaces.Repositories;
using WebAPI.BLL.Interfaces.Services;

namespace WebAPI.BLL.Services
{
    public class TableStorageService : ITableStorageService
    {
        private readonly ITableStorageRepository _tableStorageRepository;

        public TableStorageService(ITableStorageRepository tableStorageRepository)
        {
            _tableStorageRepository = tableStorageRepository;
        }

        public void Delete(string url)
        {
            _tableStorageRepository.Delete(url);
        }

        public IEnumerable<string> GetAll()
        {
            return _tableStorageRepository.GetAll();
        }

        public void Save(string url)
        {
            _tableStorageRepository.Save(url);
        }
    }
}
