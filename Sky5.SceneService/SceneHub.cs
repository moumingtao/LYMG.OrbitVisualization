using Microsoft.AspNetCore.SignalR;
using Sky5.SceneService.Satellite;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sky5.SceneService
{
    public partial class SceneHub : Hub
    {
        readonly ConnectionConfig ConnectionConfig;
        readonly Service Service;

        public SceneHub(ConnectionConfig connectionConfig, Service service)
        {
            ConnectionConfig = connectionConfig;
            Service = service;
        }

    }
}
