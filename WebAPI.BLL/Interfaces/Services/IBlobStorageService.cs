using System;
using System.IO;
using System.Threading.Tasks;

namespace WebAPI.BLL.Interfaces.Services
{
    public interface IBlobStorageService
    {
        Task<String> UploadImage(String container, String fileName, Stream fileStream, String contentType);
    }
}
