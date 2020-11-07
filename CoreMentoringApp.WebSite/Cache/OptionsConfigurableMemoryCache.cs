using CoreMentoringApp.WebSite.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CoreMentoringApp.WebSite.Cache
{
    public class OptionsConfigurableMemoryCache
    {
        public MemoryCache Cache { get; }

        public OptionsConfigurableMemoryCache(IOptions<CacheOptions> cacheOptions)
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = cacheOptions.Value.MaxCount
            });
        }

    }
}
