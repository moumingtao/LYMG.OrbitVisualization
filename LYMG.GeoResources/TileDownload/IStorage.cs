using Sky5.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LYMG.GeoResources.TileDownload
{
    public interface IStorage
    {
        Task<HashSet<TileItem>> DeDuplication(long x, long y, int z, int count);
        Task SaveTileAsync(TileItem item);
    }
}
