using System;
using System.IO;
using System.Threading.Tasks;

namespace WebAPI.Domain.Interfaces.Services
{
    public interface IBlobStorageService
    {
        String UploadImage(String container, String fileName, Stream fileStream, String contentType);
    }
}
