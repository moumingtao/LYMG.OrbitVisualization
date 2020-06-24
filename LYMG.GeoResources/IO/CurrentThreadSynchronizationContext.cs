using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Sky5.IO.WorkItemQueue;

namespace Sky5.IO
{
    public class CurrentThreadSynchronizationContext : SynchronizationContext
    {
        AutoResetEvent are = new AutoResetEvent(true);
        WorkItemQueue WorkItemQueue = new WorkItemQueue();
        internal void Post(WorkItem work)
        {
            bool isEmpty;
            lock (WorkItemQueue)
            {
                isEmpty = WorkItemQueue.FirstWork == WorkItemQueue.LastWork;
                WorkItemQueue.Enqueue(work);
            }
            if (isEmpty)
            {
                are.Set();
            }
        }
        public void ProcessWorks()
        {
            while (true)
            {
                WorkItem work;
                var queue = WorkItemQueue;
                lock (queue)
                {
                    work = queue.Dequeue();
                }
                
                if (work == null)
                {
                    if (SynchronizationContext.Current != this)
                    {
                        return;
                    }
                    are.WaitOne();
                    continue;
                }
                
                work.Callback?.Invoke(work.State);
                work.Event?.Set();
            }
        }

        public void Exit()
        {
            SynchronizationContext.SetSynchronizationContext(null);
            are.Set();
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            if (SynchronizationContext.Current == this)
                // 如果当前线程同步上下文就是this，就直接执行工作项
                d(state);
            else
                Post(new WorkItem { Callback = d, State = state });
        }
        public override void Send(SendOrPostCallback d, object state)
        {
            if (SynchronizationContext.Current == this)
            {
                // 如果当前线程同步上下文就是this，就直接执行工作项
                d(state);
            }
            else
            {
                AutoResetEvent are = new AutoResetEvent(false);
                Post(new WorkItem { Callback = d, State = state, Event = are });
                are.WaitOne();
            }
        }
    }
}
