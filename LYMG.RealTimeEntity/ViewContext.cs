using JsonDiffPatchDotNet;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LYMG.RealTimeEntity
{
    public class ViewContext
    {
        IHubClients Clients;
        // 参数作为Key从容器中获取View实例
        public Task NotifyDiffAsync(string groupName, int version, JToken diff)
        {
            return Clients.Group(groupName).SendCoreAsync("Path", new object[] { version, diff });
        }


    }
}
