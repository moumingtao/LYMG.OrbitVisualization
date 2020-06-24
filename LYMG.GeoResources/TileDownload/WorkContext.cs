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
        public TaskScheduler TaskScheduler = new LimitedConcurrencyLevelTaskScheduler(1);
        public readonly Queue<DownloadTask> Tasks = new Queue<DownloadTask>();

        public TileProvider Provider;
        protected Queue<TileItem> DownloadQueue = new Queue<TileItem>();
        public int QueueMaxCount, QueueMinCount;

        public IStorage Storage;
        int parallelLimit;
        public int ParallelLimit
        {
            get => parallelLimit;
            set
            {
                parallelLimit = value;
                Trigger();
            }
        }
        int triggerRuning;
        public void Trigger()
        {
            if (TaskScheduler.Current != this.TaskScheduler)
            {
                if (Interlocked.Exchange(ref triggerRuning, 1) == 0)
                    new Task(Trigger).Start(TaskScheduler);
                return;
            }
            Interlocked.Exchange(ref triggerRuning, 0);
            while (parallelLimit > WorkerCount)
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
                        BeginEnqueueDownloadItems();
                    if (DownloadQueue.Count == 0) break;
                    var tile = DownloadQueue.Peek();
                    await Provider.DownloadAsync(tile);
                    await Storage.SaveTileAsync(tile);
                }
            }
            finally
            {
                WorkerCount--;
            }
        }

        long downloaded;
        bool BeginEnqueueDownloadItemsIsRuning;
        void BeginEnqueueDownloadItems()
        {
            if (BeginEnqueueDownloadItemsIsRuning) return;
            if (Tasks.Count == 0) return;
            BeginEnqueueDownloadItemsIsRuning = true;
            try
            {
                var task = Tasks.Peek();
                var count = QueueMaxCount - QueueMinCount;
                var x = downloaded % task.XCount;
                if (x + count > task.XCount)
                    count = (int)(task.XCount - x);
                Storage.DeDuplication(x, downloaded / task.XCount, task.Z, count);
                downloaded += count;
                if (downloaded >= task.XCount * task.YCount)
                    Tasks.Dequeue();
                Trigger();
            }
            finally
            {
                BeginEnqueueDownloadItemsIsRuning = false;
            }
        }
    }
}
