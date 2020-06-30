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

        public async Task<Process> Start(Mode mode = Mode.Nomal)
        {
            var command = new StringBuilder();

            #region 指定打开地址
            switch (mode)
            {
                case Mode.Nomal: command.Append("--new-window "); break;
                case Mode.Kiosk: command.Append("--kiosk "); break;
                case Mode.App: command.Append("--app="); break;
            }
            command.Append(Service.Host);
            command.Append(@"Cesium\");
            command.Append(Name);
            #endregion

            var process = Process.Start("Chrome", command.ToString());
            await Service.WaitViewer(Name);
            return process;
        }

        public enum Mode
        {
            Nomal,
            Kiosk,
            App
        }
    }
}
