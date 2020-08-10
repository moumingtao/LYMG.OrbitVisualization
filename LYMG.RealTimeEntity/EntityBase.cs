using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace LYMG.RealTimeEntity
{
    public class EntityBase
    {
        public ObjectId ID { get; set; }
        internal EntityBase Next;// ID从小到大排列的链表

        #region 过期
        internal int LastVisitTime;
        internal static readonly DateTime startTime = DateTime.Now;
        public void Visit() => LastVisitTime = (int)(DateTime.Now - startTime).TotalSeconds;
        internal protected virtual void OnContextChanged(EntityCache cache, bool remove)
        {
            if (remove)
            {
                LastVisitTime = -1;
                Next = null;
            }
            else
                Visit();
        }
        #endregion


    }
}
