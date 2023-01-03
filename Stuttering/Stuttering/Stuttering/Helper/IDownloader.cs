using System;
using System.Collections.Generic;
using System.Text;

namespace Stuttering.Helper
{
    public interface IDownloader
    {
        void DownloadFile(string url, string folder);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
    }
}
