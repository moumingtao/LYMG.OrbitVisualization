using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sky5.SceneService.Satellite
{
    [SugarTable("卫星在管信息", "同一卫星对不同程序的在管信息不同")]
    public class ManagedInfo
    {
        public int ID { get; set; }
        public string SatelliteName { get; set; }
        public int Priority { get; set; }// 优先级，数字越大优先级越高
        public string AppName { get; set; }// 指定对谁是在管的
        public uint Color { get; set; }// 颜色的ARGB值
        public int OrbitArgs { get; set; }// 当前采用的轨道参数，不一定是最新的，0表示不指定
    }
}
