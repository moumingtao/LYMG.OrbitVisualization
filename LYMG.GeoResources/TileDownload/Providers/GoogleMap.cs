using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LYMG.GeoResources.TileDownload.Providers
{
    // https://segmentfault.com/a/1190000016644921
    public class GoogleMap : TileProvider
    {
        private readonly IEnumerator<TileRectangularArea> AreaEnumerator;

        public override bool GenerateIsEnded => AreaEnumerator.Current == null;
        Random random = new Random();

        public GoogleMap()
        {
            AreaEnumerator = Areas().GetEnumerator();
        }

        internal protected async override Task DownloadAsync(BsonDocument tile, object worker)
        {
            const int hostcount = 4;
            int offset = random.Next(hostcount);
            for (int i = 0; i < hostcount; i++)
            {
                var host = "khms" + (offset + i) % hostcount;
                var request = (HttpWebRequest)WebRequest.Create($"https://{host}.google.com/kh/v=871?x={tile["x"]}&y={tile["y"]}&z={tile["z"]}");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36";
                try
                {
                    using (var response = await request.GetResponseAsync())
                    {
                        var buffer = (MemoryStream)worker;
                        buffer.Position = 0;
                        using (var stream = response.GetResponseStream())
                            await stream.CopyToAsync(buffer);
                        tile["img"] = buffer.ToArray();
                        tile["contentType"] = response.ContentType;
                        tile["host"] = host;
                    }
                    break;
                }
                catch (WebException ex)
                {
                    tile["err"]= ex.Message;
                    if (ex.Response is HttpWebResponse res && res.StatusCode == HttpStatusCode.NotFound)
                        await Task.Yield();
                    else
                        await Task.Delay(1000 + i * 3000);
                }
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
                    tile["z"] == area.Level)
                    .Project(Builders<BsonDocument>.Projection.Include("x").Include("y").Include("z").Exclude("_id"))
                    .ForEachAsync(tile => tiles.Remove(tile));
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

        protected internal override object GetWorkerTag()
        {
            return new MemoryStream();
        }
    }
}
