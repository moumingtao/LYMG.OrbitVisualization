using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LYMG.GeoResources.TileDownload
{
    public abstract class TileProvider
    {
        public abstract Task<byte[]> DownloadAsync(TileItem item);
    }
}
