using Sky5.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LYMG.GeoResources.TileDownload
{
    public class DownloadTask
    {
        public long BeginX { get; set; }
        public long BeginY { get; set; }
        public long XCount { get; set; }
        public long YCount { get; set; }
        public int Z { get; set; }
    }
}
