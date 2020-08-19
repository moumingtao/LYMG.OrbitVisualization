using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sky5.SceneService.Satellite
{
    [SugarTable("卫星轨道参数", "双行根数和瞬时根数")]
    public class OrbitArgsInfo
    {
        public int ID { get; set; }
        public string SatelliteName { get; set; }
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

        public bool IsUsed { get; set; }// 是否已经在使用了，已经在使用的根数禁止删除
    }
}
