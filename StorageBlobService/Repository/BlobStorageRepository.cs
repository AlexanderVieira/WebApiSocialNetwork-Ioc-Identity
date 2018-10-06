using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;
using WebAPI.BLL.Interfaces.Repositories;

namespace WebAPI.StorageBlobService.Repository
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private CloudStorageAccount _cloudStorageAccount;        
        public BlobStorageRepository()
        {
            _cloudStorageAccount = CloudStorageAccount
                .Parse(StorageBlobService.Properties.Settings.Default.StorageConnectionString);
        }

        /// <summary>
        /// Retorna o endereço da imagem
        /// </summary>
        /// <param name="container"></param>
        /// <param name="fileName"></param>
        /// <param name="inputStream"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<String> UploadImage(String container, String fileName, Stream fileStream, String contentType)
        {
            //Classe que faz acesso ao Azure Storage Blob
            var blobClient = _cloudStorageAccount.CreateCloudBlobClient();

            //Classe que faz referência a um Container
            var blobContainer = blobClient.GetContainerReference(container);

            //Cria um container novo se não existe
            await blobContainer.CreateIfNotExistsAsync();

            //Altera a configuração do container para permitir o acesso anônimo
            await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            //Referência a uma imagem
            var cloudBlockBlob = blobContainer.GetBlockBlobReference(fileName);
            cloudBlockBlob.Properties.ContentType = contentType;

            //Upload não assíncrono
            await cloudBlockBlob.UploadFromStreamAsync(fileStream);

            //Blob URL
            return cloudBlockBlob.Uri.AbsoluteUri;
        }        
    }
}
