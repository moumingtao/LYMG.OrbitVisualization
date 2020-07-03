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
        public bool ViewerEnter(string name)
        {
            if(Viewers.TryGetValue(name, out var viewer))
                lock (viewer)
                {
                    if (viewer.Context != null && viewer.Context.ConnectionId != Context.ConnectionId)
                        return false;
                }
            else
                viewer = GetOrAddViewer(name);
            lock (viewer)
                viewer.Context = Context;
            Context.Items["Viewer"] = viewer;
            return true;
        }
        public ICollection<string> GetViewers() => Viewers.Keys;
        public override Task OnConnectedAsync()
        {
            if (Context.Items.TryGetValue("Viewer", out var value) && value is ViewerInfo viewer)
                lock (viewer)
                    viewer.Context = Context;
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.Items.TryGetValue("Viewer", out var value) && value is ViewerInfo viewer)
                lock (viewer)
                {
                    viewer.Context = null;
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
            return Clients.Client(Viewers[name].Context.ConnectionId).SendCoreAsync("eval", new object[] { null, script, args });
        }
        public async Task<object> ViewerEvalWithResultProxy(string name, string script, object[] args)
        {
            var cid = "cmd:" + Guid.NewGuid().ToString("N");
            try
            {
                var viewer = Viewers[name];
                var source = new TaskCompletionSource<object>();
                viewer.Context.Items[cid] = source;
                await Clients.Client(viewer.Context.ConnectionId).SendCoreAsync("eval", new object[] { cid, script, args });
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
