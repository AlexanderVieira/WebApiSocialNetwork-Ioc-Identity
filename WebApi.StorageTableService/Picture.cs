using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace WebApi.StorageTableService
{
    public class Picture : TableEntity
    {
        public String Url { get; set; }

        public Picture(Guid id, String url)
        {
            this.PartitionKey = "Lettucebrain";
            this.RowKey = id.ToString();            
            Url = url;
        }

        public Picture()
        {

        }
    }
}
