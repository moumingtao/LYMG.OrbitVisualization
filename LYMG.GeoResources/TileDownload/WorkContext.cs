using MongoDB.Bson;
using Sky5.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LYMG.GeoResources.TileDownload
{
    public class WorkContext
    {
        public CurrentThreadSynchronizationContext SynchronizationContext = new CurrentThreadSynchronizationContext();
        TaskCompletionSource<TileProvider> TaskCompletionSource;

        public TileProvider Provider;
        internal Queue<BsonDocument> DownloadQueue = new Queue<BsonDocument>();
        public int QueueMinCount;

        int parallelLimit;
        public int ParallelLimit
        {
            get => parallelLimit;
            set
            {
                parallelLimit = value;
                Changed();
            }
        }
        int triggerRuning;
        public void Changed()
        {
            if (System.Threading.SynchronizationContext.Current != this.SynchronizationContext)
            {
                if (Interlocked.Exchange(ref triggerRuning, 1) == 0)
                    SynchronizationContext.Post(Changed, null);
            }
            else Changed(null);
        }
        void Changed(object state)
        {
            Interlocked.Exchange(ref triggerRuning, 0);
            for (int i = WorkerCount; i < parallelLimit; i++)
            {
                RunWork();
            }
        }
        public int WorkerCount { get; private set; }
        async void RunWork()
        {
            WorkerCount++;
            try
            {
                while (WorkerCount <= parallelLimit)
                {
                    if (DownloadQueue.Count <= QueueMinCount)
                        Provider.BeginEnqueueDownloadItems(this);
                    if (DownloadQueue.Count == 0)
                    {
                        if (Provider.GenerateIsEnded && TaskCompletionSource != null)
                        {
                            TaskCompletionSource.SetResult(Provider);
                            TaskCompletionSource = null;
                        }
                        break;
                    }
                    var tile = DownloadQueue.Dequeue();
                    await Provider.DownloadAsync(tile);
                    await Provider.Storage.InsertOneAsync(tile);
                }
            }
            finally
            {
                WorkerCount--;
            }
        }
        public Task WorkTask
        {
            get
            {
                if (TaskCompletionSource == null)
                    TaskCompletionSource = new TaskCompletionSource<TileProvider>();
                return TaskCompletionSource.Task;
            }
        }
    }
}
