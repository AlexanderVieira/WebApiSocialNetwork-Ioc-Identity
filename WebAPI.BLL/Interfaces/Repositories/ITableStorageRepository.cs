using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.BLL.Interfaces.Repositories
{
    public interface ITableStorageRepository
    {
        void Save(String url);
        IEnumerable<String> GetAll();
        void Delete(String url);
    }
}
