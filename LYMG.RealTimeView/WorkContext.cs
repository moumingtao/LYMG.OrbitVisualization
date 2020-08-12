using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LYMG.RealTimeView
{
    public class WorkContext
    {
        public SynchronizationContext SynchronizationContext;
        public Task PostAsync(Func<Task> func)
        {
            if (SynchronizationContext.Current == SynchronizationContext)
                return func();

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            SynchronizationContext.Post(state=> {
                func().ContinueWith(task => {
                    if (task.Exception != null)
                        tcs.SetException(task.Exception);
                    else
                        tcs.SetResult(null);
                });
            }, null);
            return tcs.Task;
        }
        public Task PostAsync(Action action)
        {
            if (SynchronizationContext.Current == SynchronizationContext)
            {
                action();
                return Task.CompletedTask;
            }
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            SynchronizationContext.Post(state => {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }, null);
            return tcs.Task;
        }
        public void Post(SendOrPostCallback callback, object state)
        {
            if (SynchronizationContext.Current == SynchronizationContext)
                callback(state);
            SynchronizationContext.Post(callback, state);
        }
    }
}
