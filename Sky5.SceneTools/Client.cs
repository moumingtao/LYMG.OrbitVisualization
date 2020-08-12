using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sky5.SceneTools
{
    public class Client
    {
        public IMongoCollection<Scene> Scenes;
        public IMongoCollection<SatelliteInfo> Satellites;
        public IMongoCollection<GroundStation> GroundStations;

        public Client(IMongoDatabase database)
        {
            this.Scenes = database.GetCollection<Scene>("Scenes");
            this.Satellites = database.GetCollection<SatelliteInfo>("Satellites");
            this.GroundStations = database.GetCollection<GroundStation>("GroundStations");
        }


    }
}
