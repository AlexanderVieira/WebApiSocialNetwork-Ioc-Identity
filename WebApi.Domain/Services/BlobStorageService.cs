using System.IO;
using WebAPI.Domain.Interfaces.Repositories;
using WebAPI.Domain.Interfaces.Services;

namespace WebAPI.Domain.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly IBlobStorageRepository _blobStorageRepository;

        public BlobStorageService(IBlobStorageRepository blobStorageRepository)
        {
            _blobStorageRepository = blobStorageRepository;
        }

        public string UploadImage(string container, string fileName, Stream fileStream, string contentType)
        {
            return _blobStorageRepository.UploadImage(container, fileName, fileStream, contentType);
        }
    }
}
