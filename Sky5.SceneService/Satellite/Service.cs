using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sky5.SceneService.Satellite
{
    public class Service
    {
        readonly Dictionary<string, SatelliteCache> SatelliteCaches = new Dictionary<string, SatelliteCache>();

        public async Task Load(SqlSugarClient db)
        {
            // 加载所有卫星
            var sats = await db.Queryable<SatelliteInfo>().ToListAsync();
            foreach (var satellite in sats) Manage(satellite);

            // 加载所有卫星最新的轨道根数
            var oais = await db.Queryable<OrbitArgsInfo>()
                .GroupBy(i => i.SatelliteName)
                .OrderBy(i => i.EpochTime)
                .ToListAsync();
            foreach (var item in oais) Manage(item);

            // 加载所有卫星的在管信息
            var mis = await db.Queryable<ManagedInfo>().ToListAsync();
            foreach (var mi in mis) Manage(mi);
        }

        public void Manage(SatelliteInfo satellite)
        {
            if (SatelliteCaches.TryGetValue(satellite.SatelliteName, out var sc))
                sc.SatelliteInfo = satellite;
            else
                SatelliteCaches.Add(satellite.SatelliteName, new SatelliteCache { SatelliteInfo = satellite });
        }

        public void Manage(OrbitArgsInfo orbitArgs)
        {
            if (SatelliteCaches.TryGetValue(orbitArgs.SatelliteName, out var sc))
            {
                if(sc.NewstOrbitArgs == null || sc.NewstOrbitArgs.EpochTime < orbitArgs.EpochTime)
                    sc.NewstOrbitArgs = orbitArgs;
            }
            else
                SatelliteCaches.Add(orbitArgs.SatelliteName, new SatelliteCache { NewstOrbitArgs = orbitArgs });
        }

        public void Manage(ManagedInfo mi)
        {
            if (SatelliteCaches.TryGetValue(mi.SatelliteName, out var sc))
            {
                if (sc.ManagedInfos == null)
                    sc.ManagedInfos = new List<ManagedInfo>(1);
                sc.ManagedInfos.Add(mi);
            }
            else
            {
                sc = new SatelliteCache { ManagedInfos = new List<ManagedInfo>(1) };
                sc.ManagedInfos.Add(mi);
                SatelliteCaches.Add(mi.SatelliteName, sc);
            }
        }

        internal SatelliteInfo GetSatelliteCacheBySatelliteName(string sateName)
        {
            foreach (var item in SatelliteCaches.Values)
            {
                if (item.SatelliteInfo != null && item.SatelliteInfo.SatelliteName == sateName)
                    return item.SatelliteInfo;
            }
            return null;
        }

        internal IEnumerable<KeyValuePair<string, SatelliteCache>> AllSatellite => SatelliteCaches;
    }
    struct SatelliteCache
    {
        public SatelliteInfo SatelliteInfo;
        public OrbitArgsInfo NewstOrbitArgs;
        public List<ManagedInfo> ManagedInfos;
    }
}
