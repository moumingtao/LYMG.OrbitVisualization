using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sky5.SceneTools
{
    public class Scene
    {
        public ObjectId ID { get; private set; }
        public DateTime BeginTime { get; private set; }
        public DateTime EndTime { get; private set; }
    }
}
