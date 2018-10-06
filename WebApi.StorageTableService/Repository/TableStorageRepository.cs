using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.BLL.Interfaces.Repositories;

namespace WebApi.StorageTableService.Repository
{
    public class TableStorageRepository : ITableStorageRepository
    {
        public void Delete(string url)
        {
            var storageAccount = CloudStorageAccount
                .Parse(WebApi.StorageTableService.Properties.Settings.Default.StorageConnectionString);

            var tableClient = storageAccount.CreateCloudTableClient();

            var table = tableClient.GetTableReference("Picture");

            TableQuery<Picture> query = new TableQuery<Picture>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Lettucebrain"));

            var picuresToDelete = table.ExecuteQuery(query);

            TableBatchOperation operationToDelete = new TableBatchOperation();
            foreach (var currentPicture in picuresToDelete)
            {
                operationToDelete.Delete(currentPicture);
            }
            table.ExecuteBatch(operationToDelete);
        }

        public IEnumerable<string> GetAll()
        {
            try
            {
                var storageAccount = CloudStorageAccount
                .Parse(WebApi.StorageTableService.Properties.Settings.Default.StorageConnectionString);

                var tableClient = storageAccount.CreateCloudTableClient();

                var table = tableClient.GetTableReference("Picture");

                TableQuery<Picture> query = new TableQuery<Picture>()
                    .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Lettucebrain"));

                IEnumerable<Picture> pictures = table.ExecuteQuery(query);

                IEnumerable<string> urls = null;

                if (pictures.ToList().Count() > 0)
                {
                    urls = pictures.Select(p => p.Url);
                }
                else
                {
                    urls = new List<string>();
                }

                return urls;
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return null;
        }

        public void Save(string url)
        {
            try
            {
                var storageAccount = CloudStorageAccount
                .Parse(WebApi.StorageTableService.Properties.Settings.Default.StorageConnectionString);

                var tableClient = storageAccount.CreateCloudTableClient();

                var table = tableClient.GetTableReference("Picture");

                table.CreateIfNotExists();

                var batchOperation = new TableBatchOperation();

                var picture = new Picture(Guid.NewGuid(), url);

                batchOperation.Insert(picture);

                table.ExecuteBatch(batchOperation);
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            
        }
    }
}
