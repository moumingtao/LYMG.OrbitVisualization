using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace LYMG.OrbitVisualization
{
    public class CesiumViewerProxy
    {
        public CesiumViewerProxy(string name, CesiumService service)
        {
            Name = name;
            Service = service;
        }

        public string Name { get; }
        public CesiumService Service { get; }

        public IDisposable On(string methodName, Type[] parameterTypes, Func<object[], object, Task> handler, object state)
            => Service.Connection.On(Name + "." + methodName, parameterTypes, handler, state);

        public void Remove(string methodName)
            => Service.Connection.Remove(Name + "." + methodName);

        public async Task<Process> Start(Mode mode, string baseUrl)
        {
            var command = new StringBuilder();

            #region 指定打开地址
            switch (mode)
            {
                case Mode.Nomal: command.Append("--new-window "); break;
                case Mode.Kiosk: command.Append("--kiosk "); break;
                case Mode.App: command.Append("--app="); break;
            }
            command.Append(baseUrl);
            command.Append(Name);
            #endregion
            var info = new ProcessStartInfo
            {
                FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe",
                Arguments = command.ToString(),
                WorkingDirectory = @"C:\Program Files (x86)\Google\Chrome\Application",
            };
            var process = Process.Start(info);
            await Service.WaitViewer(Name);
            return process;
        }

        public enum Mode
        {
            Nomal,
            Kiosk,
            App
        }

        public Task ViewerEvalAsync(string script, params object[] args)
            => Service.Connection.SendCoreAsync("ViewerEvalProxy", new object[] { Name, script, args });
        public async Task<T> ViewerEvalAsync<T>(string script, params object[] args)
            => (T)await Service.Connection.InvokeCoreAsync("ViewerEvalWithResultProxy", typeof(T), new object[] { Name, script, args });
    }
}
