using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;

namespace WebAPI.BLL.Interfaces.Repositories
{
    public interface IImageRepository : IBaseRepository<Image> 
    {
        IEnumerable<Image> GetImagesByUserId(Guid userId);
    }
}
