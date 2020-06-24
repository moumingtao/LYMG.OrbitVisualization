using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Sky5.IO.WorkItemQueue;

namespace Sky5.IO
{
    /// <summary>
    /// 限制了并行数的任务调度器，由于重置了同步上下文，awit/async代码块也会被它调度
    /// </summary>
    public class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
    {
        /// <summary>
        /// 最大并发数
        /// </summary>
        public int MaxDegreeOfParallelism;
        public int ParallelismCount { get; private set; }
        public readonly LimitedConcurrencyLevelSynchronizationContext SyncContext;
        WorkItemQueue WorkItemQueue = new WorkItemQueue();

        public LimitedConcurrencyLevelTaskScheduler(int maxDegreeOfParallelism)
        {
            this.MaxDegreeOfParallelism = maxDegreeOfParallelism;
            SyncContext = new LimitedConcurrencyLevelSynchronizationContext(this);
        }
        
        internal void Post(WorkItem work)
        {
            lock (WorkItemQueue)
            {
                WorkItemQueue.Enqueue(work);
                if (ParallelismCount < MaxDegreeOfParallelism)
                {
                    ++ParallelismCount;
                    ThreadPool.UnsafeQueueUserWorkItem(ProcessWorks, null);
                }
            }
        }
        private void ProcessWorks(object state)
        {
            var oldContext = SynchronizationContext.Current;
            SynchronizationContext.SetSynchronizationContext(SyncContext);
            try
            {
                while (true)
                {
                    WorkItem work;
                    lock (WorkItemQueue)
                    {
                        work = WorkItemQueue.Dequeue();
                        if (work == null)
                        {
                            --ParallelismCount;
                            break;
                        }
                    }
                    if (work.Task != null)
                    {
                        TryExecuteTask(work.Task);
                    }
                    else
                    {
                        work.Callback?.Invoke(work.State);
                        work.Event?.Set();
                    }
                }
            }
            finally
            {
                SynchronizationContext.SetSynchronizationContext(oldContext);
            }
        }

        protected override void QueueTask(Task task)
        {
            Post(new WorkItem { Task = task});
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (SynchronizationContext.Current == SyncContext) return false;
            if (taskWasPreviouslyQueued) TryDequeue(task);
            return base.TryExecuteTask(task);
        }

        protected override bool TryDequeue(Task task)
        {
            lock (WorkItemQueue)
            {
                var last = WorkItemQueue.LastWork;
                for (WorkItem i = WorkItemQueue.FirstWork; i != last; i = i.Next)
                {
                    if (i.Task == task)
                    {
                        WorkItemQueue.Remove(i);
                        return true;
                    }
                }
            }
            return false;
        }
        public override int MaximumConcurrencyLevel => ParallelismCount;
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            lock (WorkItemQueue)
            {
                var last = WorkItemQueue.LastWork;
                for (WorkItem i = WorkItemQueue.FirstWork; i != last; i = i.Next)
                {
                    var task = i.Task;
                    if (task != null)
                    {
                        yield return task;
                    }
                }
            }
        }
    }
}
