using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LYMG.GeoResources.TileDownload.Providers
{
    // https://segmentfault.com/a/1190000016644921
    public class GoogleMap : TileProvider
    {
        public string lyrs = "s";
        public string gl = "CN";
        public string hl = "zh-CN";
        private readonly IEnumerator<TileRectangularArea> AreaEnumerator;

        public override bool GenerateIsEnded => AreaEnumerator.Current == null;

        public GoogleMap()
        {
            AreaEnumerator = Areas().GetEnumerator();
        }

        internal protected async override Task DownloadAsync(BsonDocument tile)
        {
            var request = (HttpWebRequest)WebRequest.Create($"https://mts1.google.com/vt/lyrs={tile["lyrs"]}&gl={tile["gl"]}&hl={tile["hl"]}&x={tile["x"]}&y={tile["y"]}&z={tile["z"]}");
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36";
            //request.Proxy = new WebProxy("127.0.0.1", 10808);
            using (var response = await request.GetResponseAsync())
            {
                var buffer = new byte[response.ContentLength];
                using (var stream = response.GetResponseStream())
                    await stream.ReadAsync(buffer, 0, buffer.Length);
                tile["img"] = buffer;
                tile["contentType"] = response.ContentType;
            }
        }


        protected async override Task GenerateTask(Queue<BsonDocument> downloadQueue)
        {
            while (AreaEnumerator.MoveNext())
            {
                var area = AreaEnumerator.Current;
                var tiles = new HashSet<BsonDocument>();
                for (long x = 0; x < area.CountX; x++)
                    for (int y = 0; y < area.CountY; y++)
                    {
                        var t = new BsonDocument();
                        t["x"] = area.BeginX + x;
                        t["y"] = area.BeginY + y;
                        t["z"] = area.Level;
                        tiles.Add(t);
                    }
                await Storage.Find(tile =>
                    tile["x"] >= area.BeginX && tile["x"] < area.BeginX + area.CountX &&
                    tile["y"] >= area.BeginY && tile["y"] < area.BeginY + area.CountY &&
                    tile["z"] == area.Level && tile["lyrs"] == lyrs && tile["gl"] == gl && tile["hl"] == hl)
                    .Project(Builders<BsonDocument>.Projection.Include("x").Include("y").Include("z").Exclude("_id"))
                    .ForEachAsync(tile => tiles.Remove(tile));
                foreach (var t in tiles)
                {
                    t["lyrs"] = lyrs;
                    t["gl"] = gl;
                    t["hl"] = hl;
                }
                foreach (var item in tiles) downloadQueue.Enqueue(item);
                if (tiles.Count > 0) break;
            }
        }

        IEnumerable<TileRectangularArea> Areas()
        {
            int z = 0;
            while (z < 5)
            {
                var w = 1 << z;
                yield return new TileRectangularArea { BeginX = 0, BeginY = 0, CountX = w, CountY = w, Level = z };
                z++;
            }
            while (z < 15)
            {
                var count = 1 << (z - 5);
                for (int x = 0; x < count; x++)
                    for (int y = 0; y < count; y++)
                    {
                        yield return new TileRectangularArea { BeginX = x * count, BeginY = y * count, CountX = 1 << 5, CountY = 1 << 5, Level = z };
                    }
                z++;
            }
        }
    }
}
