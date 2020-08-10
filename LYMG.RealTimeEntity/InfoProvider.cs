using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LYMG.RealTimeEntity
{
    public class InfoProvider
    {
        int version;
        readonly InfoContext Context;

        public InfoProvider(InfoContext context)
        {
            Context = context;
        }
        public ValueTask Update()
        {
            if (Context.version == version)
                return default;
            version = Context.version;
            Context.NeedClearItems.Add(this);
            return Provid();
        }
        protected virtual ValueTask Provid() => default;
        public virtual void Clear() { }
    }
}
