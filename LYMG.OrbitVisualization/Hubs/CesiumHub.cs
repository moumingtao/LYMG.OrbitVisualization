using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver.Core.Connections;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LYMG.OrbitVisualization.Hubs
{
    public class CesiumHub : Hub
    {
        public CesiumHub()
        { }
        #region Viewers管理
        static ConcurrentDictionary<string, ViewerInfo> Viewers = new ConcurrentDictionary<string, ViewerInfo>();
        ViewerInfo GetOrAddViewer(string name) => Viewers.GetOrAdd(name, CreateViewer);
        ViewerInfo CreateViewer(string name) => new ViewerInfo { Name = name };
        public void ViewerEnter(string name)
        {
            var viewer = GetOrAddViewer(name);
            lock (viewer)
                viewer.ConnectionId = Context.ConnectionId;
            Context.Items["Viewer"] = viewer;
        }
        public ICollection<string> GetViewers() => Viewers.Keys;
        public override Task OnConnectedAsync()
        {
            if (Context.Items.TryGetValue("Viewer", out var value) && value is ViewerInfo viewer)
                lock (viewer)
                    viewer.ConnectionId = Context.ConnectionId;
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.Items.TryGetValue("Viewer", out var value) && value is ViewerInfo viewer)
                lock (viewer)
                {
                    if (viewer.IsEmpty)
                        Viewers.TryRemove(viewer.Name, out _);
                    else viewer.ConnectionId = null;
                }
            return base.OnDisconnectedAsync(exception);
        }
        public Task WaitViewer(string name)
        {
            var viewer = GetOrAddViewer(name);
            lock (viewer)
                return viewer.WaitConnectionAsync();
        }
        #endregion

        public void ViewerInvoke(string name, string method, object[] args)
        {
            if (Viewers.TryGetValue(name, out var viewer))
            {
                var cid = viewer.ConnectionId;
                if (cid != null)
                    Clients.Client(cid).SendCoreAsync(method, args);
            }
        }
    }
}
