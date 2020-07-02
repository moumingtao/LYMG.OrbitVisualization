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
                    viewer.ConnectionId = null;
                    if (viewer.IsEmpty)
                        Viewers.TryRemove(viewer.Name, out _);
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

        #region 调用代理
        public Task ViewerEvalProxy(string name, string script, object[] args)
        {
            return Viewers[name].SendCoreAsync(Clients, "eval", new object[] { null, script, args });
        }
        public async Task<object> ViewerEvalWithResultProxy(string name, string script, object[] args)
        {
            var cid = "cmd:" + Guid.NewGuid();
            try
            {
                var source = new TaskCompletionSource<object>();
                Context.Items[cid] = source;
                await Viewers[name].SendCoreAsync(Clients, "eval", new object[] { cid, script, args });
                return await source.Task;
            }
            finally
            {
                Context.Items.Remove(cid);
            }
        }
        public void ViewerResponse(string cid, object res)
        {
            var source = (TaskCompletionSource<object>)Context.Items[cid];
            source.SetResult(res);
        }
        #endregion
    }
}
