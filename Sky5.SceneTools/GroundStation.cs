using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sky5.SceneTools
{
    public class GroundStation
    {
        public ObjectId ID { get; set; }
        public string Name { get; set; }
        public float MinElevation { get; set; }
        public double LatitudeDeg { get; set; }
        public double LongitudeDeg { get; set; }
    }
}
