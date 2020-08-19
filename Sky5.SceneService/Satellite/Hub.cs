using Sky5.SceneService.Satellite;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sky5.SceneService
{
    public partial class SceneHub
    {
        public async Task<int> 添加卫星根数(OrbitArgsInfo args)
        {
            if (string.IsNullOrEmpty(args.TleLine1) != string.IsNullOrEmpty(args.TleLine2))
                throw new ArgumentException("双行根数不完整");

            var info = new Satellite.OrbitArgsInfo
            {
                SatelliteName = args.SatelliteName,
                EpochTime = args.EpochTime,
                TleLine1 = args.TleLine1,
                TleLine2 = args.TleLine2,
                轨道半长轴 = args.轨道半长轴,
                轨道偏心率 = args.轨道偏心率,
                轨道倾角 = args.轨道倾角,
                升交点赤经 = args.升交点赤经,
                近地点幅角 = args.近地点幅角,
                平近点角 = args.平近点角,
                大气阻尼系数 = args.大气阻尼系数,
                光压反射系数 = args.光压反射系数,
            };
            using (var db = new SqlSugarClient(ConnectionConfig))
                await db.Insertable(info).ExecuteCommandIdentityIntoEntityAsync();
            Service.Manage(info);
            return info.ID;
        }
        public class OrbitArgsInfo
        {
            [Required]
            public string SatelliteName { get; set; }
            [Required]
            public DateTime EpochTime { get; set; }

            #region 双行根数
            public string TleLine1 { get; set; }
            public string TleLine2 { get; set; }
            #endregion

            #region 瞬时根数
            public double 轨道半长轴 { get; set; }
            public double 轨道偏心率 { get; set; }
            public double 轨道倾角 { get; set; }
            public double 升交点赤经 { get; set; }
            public double 近地点幅角 { get; set; }
            public double 平近点角 { get; set; }
            #endregion

            public double 大气阻尼系数 { get; set; }
            public double 光压反射系数 { get; set; }
        }

        public Task<int> 添加卫星(SatelliteInfo satellite)
        {
            using (var db = new SqlSugarClient(ConnectionConfig))
            {
                return db.Insertable(satellite).ExecuteReturnIdentityAsync();
            }
        }

        public Task<int> 添加在管信息(ManagedInfo info)
        {
            using (var db = new SqlSugarClient(ConnectionConfig))
            {
                return db.Insertable(info).ExecuteReturnIdentityAsync();
            }
        }
    }


}
