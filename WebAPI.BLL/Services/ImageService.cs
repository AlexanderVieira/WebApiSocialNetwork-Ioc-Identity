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
    public class ImageService : BaseService<Image>, IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository) : base(imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public IEnumerable<Image> GetImagesByUserId(Guid userId)
        {
            return _imageRepository.GetImagesByUserId(userId);
        }
    }
}
