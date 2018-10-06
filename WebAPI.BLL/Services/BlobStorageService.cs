using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces.Repositories;
using WebAPI.BLL.Interfaces.Services;

namespace WebAPI.BLL.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly IBlobStorageRepository _blobStorageRepository;

        public BlobStorageService(IBlobStorageRepository blobStorageRepository)
        {
            _blobStorageRepository = blobStorageRepository;
        }

        public Task<String> UploadImage(String container, String fileName, Stream fileStream, String contentType)
        {
            return _blobStorageRepository.UploadImage(container, fileName, fileStream, contentType);
        }
    }
}
