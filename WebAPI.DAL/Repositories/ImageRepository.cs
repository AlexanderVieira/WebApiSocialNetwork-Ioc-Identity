using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Repositories;

namespace WebAPI.DAL.Repositories
{
    public class ImageRepository : BaseRepository<Image> ,IImageRepository
    {
        public IEnumerable<Image> GetImagesByUserId(Guid userId)
        {
            return _ctx.Images.Where(i => i.Profile.Id.Equals(userId)).ToList();            
        }
    }
}
