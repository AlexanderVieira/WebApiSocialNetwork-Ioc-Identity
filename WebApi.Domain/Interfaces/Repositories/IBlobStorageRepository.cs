using System;
using System.IO;
using System.Threading.Tasks;

namespace WebAPI.Domain.Interfaces.Repositories
{
    public interface IBlobStorageRepository
    {
        String UploadImage(String container, String fileName, Stream fileStream, String contentType);
    }
}
