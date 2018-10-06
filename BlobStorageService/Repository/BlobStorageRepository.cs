using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using WebAPI.Domain.Interfaces.Repositories;

namespace BlobStorageService.Repository
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private CloudStorageAccount _cloudStorageAccount;
        
        public BlobStorageRepository()
        {
            _cloudStorageAccount = CloudStorageAccount
                .Parse(BlobStorageService.Properties.Settings.Default.StorageConnectionStr);
        }

        /// <summary>
        /// Retorna o endereço da imagem
        /// </summary>
        /// <param name="container"></param>
        /// <param name="fileName"></param>
        /// <param name="inputStream"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string UploadImage(string container, string fileName, Stream fileStream, string contentType)
        {
            //Classe que faz acesso ao Azure Storage Blob
            var blobClient = _cloudStorageAccount.CreateCloudBlobClient();

            //Classe que faz referência a um Container
            var blobContainer = blobClient.GetContainerReference(container);

            //Cria um container novo se não existe
            blobContainer.CreateIfNotExistsAsync();

            //Altera a configuração do container para permitir o acesso anônimo
            blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            //Referência a uma imagem
            var cloudBlockBlob = blobContainer.GetBlockBlobReference(fileName);
            cloudBlockBlob.Properties.ContentType = contentType;

            //Upload não assíncrono
            cloudBlockBlob.UploadFromStreamAsync(fileStream);

            //Blob URL
            return cloudBlockBlob.Uri.AbsoluteUri;
        }

    }
}
