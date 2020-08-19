using LYMG.RealTimeView;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sky5.SceneService.Satellite
{
    public class ViewAllSatellite: View
    {
        Service Service;

        public ViewAllSatellite(Service service)
        {
            Service = service;
        }

        protected override JToken Render()
        {
            var rows = new JArray();
            foreach (var item in Service.AllSatellite)
            {
                var row = new JObject();
                row.Add("name", item.Key);
                if (item.Value.NewstOrbitArgs != null)
                    row.Add("orbitArgs", item.Value.NewstOrbitArgs.EpochTime);
                if (item.Value.ManagedInfos != null)
                    row.Add("managed", item.Value.ManagedInfos.Count);
                if (item.Value.SatelliteInfo != null)
                {
                    row.Add("同步字", item.Value.SatelliteInfo.同步字.ToString("X"));
                    row.Add("UID", item.Value.SatelliteInfo.UID);
                    row.Add("探测角度", item.Value.SatelliteInfo.VisualAngleDeg);
                }
                rows.Add(row);
            }
            return rows;
        }
    }
}
