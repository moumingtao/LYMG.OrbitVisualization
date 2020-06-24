using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LYMG.GeoResources.TileDownload
{
    public class TileItem
    {
        public long X { get; set; }
        public long Y { get; set; }
        public int Z { get; set; }
        public byte[] Image { get; set; }

        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode() ^ Z ^ GetType().GetHashCode();
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType()) return false;
            var info = (TileItem)obj;
            return info.X == X && info.Y == Y && info.Z == Z;
        }
    }
}
