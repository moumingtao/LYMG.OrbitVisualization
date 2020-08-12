using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace LYMG.RealTimeView
{
    public class EntityProvider<T> where T : EntityBase
    {
        public IMongoCollection<T> Collection;
        readonly EntityCache Cache = new EntityCache();

        public T Get(ObjectId id) => (T)Cache.GetOrAdd(id, ()=> Collection.Find(i => i.ID == id).First());
        public async void ListenAsync()
        {
            var option = new ChangeStreamOptions {
                StartAtOperationTime = new BsonTimestamp((int)(DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds, 0)
            };

            using (var cursor = await Collection.WatchAsync(option))
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var csd in cursor.Current)
                    {
                        Cache.Replace(csd.FullDocument);
                    }
                }
            }
        }
    }
}
