using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LYMG.OrbitVisualization.Hubs
{
    public class ViewerInfo
    {
        public string Name;
        HubCallerContext context;
        public HubCallerContext Context
        {
            get => context;
            set
            {
                if (context == value) return;
                context = value;
                if (value != null && ConnectionCompletionSource != null)
                {
                    ConnectionCompletionSource.TrySetResult(value.ConnectionId);
                    ConnectionCompletionSource = null;
                }
            }
        }
        TaskCompletionSource<string> ConnectionCompletionSource;

        public bool IsEmpty => Context == null && ConnectionCompletionSource == null;

        public Task<string> WaitConnectionAsync()
        {
            if (context != null) return Task.FromResult(context.ConnectionId);
            if (ConnectionCompletionSource == null)
                ConnectionCompletionSource = new TaskCompletionSource<string>();
            return ConnectionCompletionSource.Task;
        }
    }
}
