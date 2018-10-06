using System;
using System.IO;
using System.Threading.Tasks;

namespace WebAPI.BLL.Interfaces.Repositories
{
    public interface IBlobStorageRepository
    {
        Task<String> UploadImage(String container, String fileName, Stream fileStream, String contentType);
    }
}
