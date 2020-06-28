using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Sky5.IO.LimitedConcurrencyLevelTaskScheduler;
using static Sky5.IO.WorkItemQueue;

namespace Sky5.IO
{
    /// <summary>
    /// 基于<see cref="LimitedConcurrencyLevelTaskScheduler"/>的同步上下文
    /// </summary>
    public class LimitedConcurrencyLevelSynchronizationContext : SynchronizationContext
    {
        private LimitedConcurrencyLevelTaskScheduler source;

        public LimitedConcurrencyLevelSynchronizationContext(LimitedConcurrencyLevelTaskScheduler source)
        {
            this.source = source;
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            if (Current == this) d(state);
            else source.Post(new WorkItem { Callback = d, State = state });
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
                source.Post(new WorkItem { Callback = d, State = state, Event = are });
                are.WaitOne();
            }
        }
    }
}
