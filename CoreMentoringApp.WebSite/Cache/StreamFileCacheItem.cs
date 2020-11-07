using System.Threading;

namespace CoreMentoringApp.WebSite.Cache
{
    public class StreamFileCacheItem
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public string FilePath { get; set; }
    }
}
