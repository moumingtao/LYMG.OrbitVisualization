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
        string connectionId;
        public string ConnectionId
        {
            get => connectionId;
            set
            {
                if (connectionId == value) return;
                connectionId = value;
                if (value != null && ConnectionCompletionSource != null)
                {
                    ConnectionCompletionSource.TrySetResult(value);
                    ConnectionCompletionSource = null;
                }
            }
        }
        TaskCompletionSource<string> ConnectionCompletionSource;

        public bool IsEmpty => ConnectionId == null && ConnectionCompletionSource == null;

        public Task<string> WaitConnectionAsync()
        {
            if (connectionId != null) return Task.FromResult(connectionId);
            if (ConnectionCompletionSource == null)
                ConnectionCompletionSource = new TaskCompletionSource<string>();
            return ConnectionCompletionSource.Task;
        }

        internal Task SendCoreAsync(IHubCallerClients clients, string method, object[] args)
        {
            var cid = connectionId;
            if (cid != null)
                return clients.Client(cid).SendCoreAsync(method, args);
            return null;
        }
    }
}
