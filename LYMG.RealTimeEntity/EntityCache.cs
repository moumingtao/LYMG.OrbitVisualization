using Microsoft.VisualBasic;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYMG.RealTimeEntity
{
    public class EntityCache
    {
        EntityBase[] Buckets;

        public int Count { get; private set; }

        public EntityBase Replace(EntityBase entity)
        {
            var index = entity.ID.GetHashCode() % Buckets.Length;

            ref EntityBase e = ref Buckets[index];

            while (e != null)
            {
                if (e.ID > entity.ID) return null;

                if (e.ID == entity.ID)
                {
                    var old = e;
                    entity.Next = e.Next;
                    e = entity;
                    old.OnContextChanged(this, true);
                    entity.OnContextChanged(this, false);
                    return old;
                }

                e = ref e.Next;
            }
            return null;
        }

        public EntityBase AddOrReplace(EntityBase entity)
        {
            var index = entity.ID.GetHashCode() % Buckets.Length;

            ref EntityBase e = ref Buckets[index];

            while (e != null)
            {
                if (e.ID > entity.ID)
                {
                    entity.Next = e;
                    e = entity;
                    Count++;
                    entity.OnContextChanged(this, false);
                    return null;
                }

                if (e.ID == entity.ID)
                {
                    var old = e;
                    entity.Next = e.Next;
                    e = entity;
                    old.OnContextChanged(this, true);
                    entity.OnContextChanged(this, false);
                    return old;
                }

                e = ref e.Next;
            }
            e = entity;
            Count++;
            entity.OnContextChanged(this, false);
            return null;
        }

        public void Add(EntityBase entity)
        {
            var index = entity.ID.GetHashCode() % Buckets.Length;

            ref EntityBase e = ref Buckets[index];

            while (e != null)
            {
                if (e.ID > entity.ID)
                {
                    entity.Next = e;
                    e = entity;
                    Count++;
                    entity.OnContextChanged(this, false);
                    return;
                }

                if (e.ID == entity.ID)
                    throw new Exception();

                e = ref e.Next;
            }
            e = entity;
            Count++;
            entity.OnContextChanged(this, false);
        }

        public EntityBase Get(ObjectId id)
        {
            if (Buckets == null) return null;
            var index = id.GetHashCode() % Buckets.Length;
            for (EntityBase e = Buckets[index]; e != null; e = e.Next)
            {
                if (e.ID < id) continue;
                if (e.ID == id)
                {
                    e.Visit();
                    return e;
                }
                if (e.ID > id) return null;
            }
            return null;
        }
        public EntityBase GetOrAdd(ObjectId id, Func<EntityBase> add)
        {
            if (Buckets == null) return null;
            var index = id.GetHashCode() % Buckets.Length;
            ref EntityBase e = ref Buckets[index];
            while (e != null)
            {
                if (e.ID < id) continue;
                if (e.ID == id)
                {
                    e.Visit();
                    return e;
                }
                if (e.ID > id)
                {
                    var item = add();
                    item.Next = e;
                    e = item;
                    item.OnContextChanged(this, false);
                    return item;
                }
                e = ref e.Next;
            }
            {
                var item = add();
                e = item;
                item.OnContextChanged(this, false);
                return item;
            }
        }
        public EntityBase Remove(ObjectId id)
        {
            if (Buckets == null) return null;
            var index = id.GetHashCode() % Buckets.Length;
            for (ref EntityBase e = ref Buckets[index]; e != null; e = ref e.Next)
            {
                if (e.ID < id) continue;
                if (e.ID == id)
                {
                    var old = e;
                    e = e.Next;
                    Count--;
                    old.OnContextChanged(this, true);
                    return old;
                }
                if (e.ID > id) return null;
            }
            return null;
        }

        public IEnumerable<EntityBase> Items()
        {
            for (int i = 0; i < Buckets.Length; i++)
                for (EntityBase e = Buckets[i]; e != null; e = e.Next)
                    yield return e;
        }

        public void Clear()
        {
            for (int i = 0; i < Buckets.Length; i++)
            {
                Buckets[i] = null;
            }
        }

        public void ClearExpired(TimeSpan timeout)
        {
            if (Buckets == null) return;
            var time = DateTime.Now - timeout;
            for (int i = 0; i < Buckets.Length; i++)
            {
                for (ref var e = ref Buckets[i]; e != null; e = e.Next)
                {
                    if (e.IsExpire(time))
                    {
                        e = e.Next;
                        Count--;
                    }
                }
            }
        }

        public int BucketCount
        {
            get => Buckets == null ? 0 : Buckets.Length;
            set
            {
                if (value == BucketCount) return;
                if (value == 0)
                {
                    if (Items().Any())
                        throw new Exception();
                    Buckets = null;
                }
                else
                {
                    var old = Buckets;
                    Buckets = new EntityBase[value];
                    for (int i = 0; i < old.Length; i++)
                        for (EntityBase e = old[i]; e != null; e = e.Next)
                            Add(e);
                }
            }
        }

    }
}
