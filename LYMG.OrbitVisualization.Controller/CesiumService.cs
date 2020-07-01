using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace LYMG.OrbitVisualization
{
    public class CesiumService
    {
        public readonly string Host;
        public HubConnection Connection { get; private set; }

        public CesiumService(string host)
        {
            this.Host = host;
        }
        public Task StartAsync()
        {
            Connection = new HubConnectionBuilder()
                .WithUrl(Host + "/CesiumHub")
                .Build();
            RegisterCallbacks();
            return Connection.StartAsync();
        }
        public Task<string[]> GetCesiumViewers(CancellationToken cancellationToken = default)
            => Connection.InvokeCoreAsync<string[]>("GetViewers", Array.Empty<object>(), cancellationToken);

        public Task WaitViewer(string name, CancellationToken cancellationToken = default)
            => Connection.InvokeAsync("WaitViewer", name, cancellationToken);

        public async Task<CesiumViewerProxy> GetViewer(string name, CancellationToken cancellationToken = default)
        {
            await WaitViewer(name, cancellationToken);
            return new CesiumViewerProxy(name, this);
        }

        #region 注册回调
        private static readonly CallbackInfo[] callbacks;
        static CesiumService()
        {
            var methods = typeof(CesiumService).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
            var temp = new List<CallbackInfo>(methods.Length);
            foreach (var method in methods)
            {
                var ps = method.GetParameters();
                var ts = new Type[ps.Length];
                for (int i = 0; i < ts.Length; i++)
                {
                    ts[i] = ps[i].ParameterType;
                }
                if (method.Name.StartsWith("On"))
                {
                    temp.Add(new CallbackInfo { Method = method, ParameterTypes = ts});
                }
            }
            callbacks = temp.ToArray();
        }
        class CallbackInfo
        {
            public MethodInfo Method;
            public Type[] ParameterTypes;

            public IDisposable Register(HubConnection connection, object state)
                => connection.On(Method.Name.Substring(2), ParameterTypes, On, state);
            Task On(object[] parameters, object state)
                => (Task)Method.Invoke(state, parameters);
        }
        void RegisterCallbacks()
        {
            foreach (var callback in callbacks)
                callback.Register(Connection, this);
        }
        #endregion
    }
}
