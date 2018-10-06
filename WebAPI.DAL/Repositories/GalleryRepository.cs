using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Repositories;

namespace WebAPI.DAL.Repositories
{
    public class GalleryRepository : BaseRepository<Gallery>, IGalleryRepository
    {
        public IEnumerable<Gallery> GetByName(Gallery gallery)
        {
            return _ctx.Galleries.ToList().Where(ga => ga.Title == gallery.Title).OrderBy(ga => ga.Title);
        }

        public void AddGalleryImage(Guid id, Image img)
        {
            _ctx.Galleries.SingleOrDefault(g => g.Id.Equals(id)).Images.Add(img);            
        }
    }
}
