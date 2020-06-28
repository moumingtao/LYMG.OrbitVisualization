using LYMG.GeoResources.TileDownload;
using LYMG.GeoResources.TileDownload.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var str = "{age:23}";
            var hs = new HashSet<BsonDocument>();
            hs.Add(BsonDocument.Parse(str));
            Assert.AreEqual(hs.Count, 1);
            hs.Add(BsonDocument.Parse(str));
            Assert.AreEqual(hs.Count, 1);
        }

        [TestMethod]
        public void Download()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("geo");
            var context = new WorkContext();
            var provider = new GoogleMap();
            provider.Storage = db.GetCollection<BsonDocument>("googleTiles");
            context.ParallelLimit = 5;
            context.Provider = provider;
            context.Changed();
            context.SynchronizationContext.ProcessWorks();
        }
    }
}
