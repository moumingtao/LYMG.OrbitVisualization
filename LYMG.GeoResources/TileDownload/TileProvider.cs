using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LYMG.GeoResources.TileDownload
{
    public abstract class TileProvider
    {
        internal protected abstract Task DownloadAsync(BsonDocument item);
        public IMongoCollection<BsonDocument> Storage;

        bool BeginEnqueueDownloadItemsIsRuning;
        internal async void BeginEnqueueDownloadItems(WorkContext context)
        {
            if (BeginEnqueueDownloadItemsIsRuning) return;
            BeginEnqueueDownloadItemsIsRuning = true;
            try
            {
                await GenerateTask(context.DownloadQueue);
                context.Changed();
            }
            finally
            {
                BeginEnqueueDownloadItemsIsRuning = false;
            }
        }

        protected abstract Task GenerateTask(Queue<BsonDocument> downloadQueue);
        public abstract bool GenerateIsEnded { get; }
    }
}
