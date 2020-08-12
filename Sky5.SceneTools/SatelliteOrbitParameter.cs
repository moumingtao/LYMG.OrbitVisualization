using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Sky5.SceneTools
{
    public class SatelliteOrbitParameter
    {
        public ObjectId ID { get; set; }
        public string SatelliteName { get; set; }
        public ObjectId SatelliteID { get; set; }
        public DateTime EpochTime { get; set; }
        public Tle Tle { get; set; }
        public InstantaneousElements InstantaneousElements { get; set; }
    }
    public class Tle
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
    }
    public class InstantaneousElements
    {
        [BsonElement("a")]
        public double 轨道半长轴 { get; set; }
        [BsonElement("e")]
        public double 轨道偏心率 { get; set; }
        [BsonElement("i")]
        public double 轨道倾角 { get; set; }
        [BsonElement("RAAN")]
        public double 升交点赤经 { get; set; }
        [BsonElement("Ω")]
        public double 近地点幅角 { get; set; }
        [BsonElement("M0")]
        public double 平近点角 { get; set; }
        [BsonElement("Cd")]
        public double 大气阻尼系数 { get; set; }
        [BsonElement("fl")]
        public double 光压反射系数 { get; set; }
    }
}
