using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sky5.SceneService.Satellite
{
    [SugarTable("卫星固有信息", "每颗卫星只有一条记录，可以通过增加列来扩展")]
    public class SatelliteInfo
    {
        public int ID { get; set; }
        public string SatelliteName { get; set; }

        public double VisualAngleDeg { get; set; }// 探测半张角
        public ushort 同步字 { get; set; }// 卫星同步字，两个字节
        public int UID { get; set; }// 星上任务ID


    }
}
