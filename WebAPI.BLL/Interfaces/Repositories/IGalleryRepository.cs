using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;

namespace WebAPI.BLL.Interfaces.Repositories
{
    public interface IGalleryRepository : IBaseRepository<Gallery>
    {
        IEnumerable<Gallery> GetByName(Gallery gallery);
        void AddGalleryImage(Guid id, Image img);
    }
}
