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
    public class GalleryService : BaseService<Gallery>, IGalleryService
    {
        private readonly IGalleryRepository _galleryRepository;

        public GalleryService(IGalleryRepository galleryRepository) : base(galleryRepository)
        {
            _galleryRepository = galleryRepository;
        }

        public void AddGalleryImage(Guid id, Image img)
        {
            _galleryRepository.AddGalleryImage(id, img);
        }

        public IEnumerable<Gallery> GetByName(Gallery gallery)
        {
            return _galleryRepository.GetByName(gallery);
        }
    }
}
