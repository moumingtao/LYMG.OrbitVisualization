using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sky5.IO
{
    class WorkItemQueue
    {
        public WorkItemQueue()
        {
            WorkItem work = new WorkItem();
            FirstWork = work;
            LastWork = work;
            work.Next = work;
            work.Prev = work;
        }
        public class WorkItem
        {
            public WorkItem Prev;
            public WorkItem Next;

            public SendOrPostCallback Callback;
            public object State;
            public AutoResetEvent Event;
            public Task Task;
        }

        public readonly WorkItem FirstWork;
        public readonly WorkItem LastWork;

        public void Enqueue(WorkItem item)
        {
            var first = FirstWork;
            var next = first.Next;
            first.Next = item;
            item.Prev = first;
            item.Next = next;
            next.Prev = item;
        }
        public WorkItem Dequeue()
        {
            var last = LastWork;
            var prev = last.Prev;
            if (prev == FirstWork) return null;
            var pprev = prev.Prev;

            pprev.Next = last;
            last.Prev = pprev;

            return prev;
        }
        public void Remove(WorkItem item)
        {
            var prev = item.Prev;
            var next = item.Next;
            prev.Next = next;
            next.Prev = prev;
        }
    }
}
