using System.IO;

namespace CoreMentoringApp.WebSite.Cache
{
    public interface IStreamMemoryCacheWorker
    {
        Stream GetStreamMemoryCacheValue(object key);

        void SetStreamMemoryCacheValue(object key, Stream stream);
    }
}
