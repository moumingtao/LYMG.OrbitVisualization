using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LYMG.RealTimeView
{
    public class InfoContext
    {
        internal readonly HashSet<InfoProvider> NeedClearItems = new HashSet<InfoProvider>();
        internal int version;

        public int IncVersion()
        {
            foreach (var item in NeedClearItems)
                item.Clear();
            return ++version;
        }
    }
}
