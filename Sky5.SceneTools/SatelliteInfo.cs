using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sky5.SceneTools
{
    public class SatelliteInfo
    {
        public ObjectId ID { get; set; }
        public string Name { get; set; }
        public float VisualAngleDeg { get; set; }
    }
}
