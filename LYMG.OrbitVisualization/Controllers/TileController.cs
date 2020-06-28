using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LYMG.OrbitVisualization.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TileController : Controller
    {
        private static readonly IMongoDatabase db;
        static readonly IMongoCollection<BsonDocument> googleTiles;

        static TileController()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            db = client.GetDatabase("geo");
            googleTiles = db.GetCollection<BsonDocument>("googleTiles");
        }

        [HttpGet("{provider}")]
        public async Task<ActionResult> Get(string provider)
        {
            // https://localhost:44389/tile/googleTiles/?x=0&y=0&z=0
            var collect = db.GetCollection<BsonDocument>(provider);
            var filter = new BsonDocument();
            foreach (var item in HttpContext.Request.Query)
            {
                var val = item.Value[0];
                if (long.TryParse(val, out var num))
                    filter[item.Key] = num;
                else
                    filter[item.Key] = val;
            }
            var res = await collect.Find(filter).FirstOrDefaultAsync();
            if (res == null || !res.Contains("img")) return NotFound();
            else return File(res["img"].AsByteArray, res["contentType"].AsString);
        }
    }
}
