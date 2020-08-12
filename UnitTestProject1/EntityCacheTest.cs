using LYMG.RealTimeView;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnitTestProject1
{
    [TestClass]
    public class EntityCacheTest
    {
        [TestMethod]
        public void Modify()
        {
            const int COUNT = 1000000;

            var cache = new EntityCache { BucketCount = COUNT / 7 };
            Assert.AreEqual(0, cache.Count);

            for (int i = 0; i < COUNT; i++)
                cache.Add(new EntityBase { ID = ObjectId.GenerateNewId() });

            Assert.AreEqual(COUNT, cache.Count);

            var entity1 = new EntityBase { ID = new ObjectId("5f32355b2fb32d25b863350f") };
            cache.Add(entity1);
            Assert.AreEqual(COUNT + 1, cache.Count);
            Assert.AreEqual(entity1, cache.Remove(entity1.ID));
            Assert.AreEqual(COUNT, cache.Count);

            Assert.AreEqual(null, cache.Replace(entity1));// 之前不存在，不能替换
            Assert.AreEqual(null, cache.AddOrReplace(entity1));
            Assert.AreEqual(entity1, cache.AddOrReplace(entity1));
            Assert.AreEqual(entity1, cache.Replace(entity1));
            Assert.AreEqual(COUNT + 1, cache.Count);
            Assert.AreEqual(entity1, cache.Get(entity1.ID));
        }

        [TestMethod]
        public void 性能测试()
        {
            const int COUNT = 10000000;
            var stopwatch = new Stopwatch();

            RunEntityCache(stopwatch, COUNT, COUNT / 100);// 17754ms
            RunDictionary(stopwatch, COUNT, COUNT / 100);// 3087ms

            RunEntityCache(stopwatch, COUNT, COUNT / 5);// 4191ms
            RunDictionary(stopwatch, COUNT, COUNT / 5);// 2992ms

            RunEntityCache(stopwatch, COUNT, COUNT / 2);// 3899ms
            RunDictionary(stopwatch, COUNT, COUNT / 2);// 2346ms

            RunEntityCache(stopwatch, COUNT, COUNT);// 3721ms
            RunDictionary(stopwatch, COUNT, COUNT);// 2187ms

        }

        void RunEntityCache(Stopwatch stopwatch, int COUNT, int BucketCount)
        {
            stopwatch.Restart();
            var cache = new EntityCache { BucketCount = BucketCount };
            for (int i = 0; i < COUNT; i++)
                cache.Add(new EntityBase { ID = ObjectId.GenerateNewId() });
            stopwatch.Stop();
        }

        void RunDictionary(Stopwatch stopwatch, int COUNT, int capacity)
        {
            stopwatch.Restart();
            var dict = new Dictionary<ObjectId, EntityBase>(capacity);
            for (int i = 0; i < COUNT; i++)
            {
                var id = ObjectId.GenerateNewId();
                dict.Add(id, new EntityBase { ID = id });
            }
            stopwatch.Stop();
        }
    }
}
