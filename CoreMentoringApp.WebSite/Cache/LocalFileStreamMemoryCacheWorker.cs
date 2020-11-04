using System;
using System.IO;
using System.Threading;
using CoreMentoringApp.WebSite.Logging;
using CoreMentoringApp.WebSite.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace CoreMentoringApp.WebSite.Cache
{
    public class LocalFileStreamMemoryCacheWorker : IStreamMemoryCacheWorker
    {
        private readonly ILogger<LocalFileStreamMemoryCacheWorker> _logger;
        private readonly CacheOptions _cacheOptions;
        private readonly MemoryCache _memoryCache;

        public LocalFileStreamMemoryCacheWorker(ILogger<LocalFileStreamMemoryCacheWorker> logger,
            OptionsConfigurableMemoryCache optionsConfigurableMemoryCache,
            IOptions<CacheOptions> cacheOptions)
        {
            _logger = logger;
            _cacheOptions = cacheOptions.Value;
            _memoryCache = optionsConfigurableMemoryCache.Cache;
        }

        public Stream GetStreamMemoryCacheValue(object key)
        {
            object value;
            if (_memoryCache.TryGetValue(key, out value))
            {
                var cacheItem = value as StreamFileCacheItem;
                if (cacheItem != null)
                {
                    string cacheFilePath = cacheItem.FilePath;
                    cacheItem.CancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(_cacheOptions.Expiration));
                    _logger.LogInformation(LogEvents.GetItemFromCache, "Loading {key} stream from file {cacheFilePath}", key, cacheFilePath);
                    return File.OpenRead(cacheFilePath);
                }
            }

            throw new Exception();
        }

        public void SetStreamMemoryCacheValue(object key, Stream stream)
        {
            _memoryCache.Compact((double)1 / _cacheOptions.MaxCount);

            var cacheFilePath = WriteToFile(key, stream);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(_cacheOptions.Expiration));
            var cacheEntryOptions = new MemoryCacheEntryOptions 
                {
                    Size = 1,
                    SlidingExpiration = TimeSpan.FromSeconds(_cacheOptions.Expiration),
                    ExpirationTokens = { new CancellationChangeToken(cts.Token) }
                }
                .RegisterPostEvictionCallback(ClearCache);
            _memoryCache.Set(key, new StreamFileCacheItem { CancellationTokenSource = cts, FilePath = cacheFilePath }, cacheEntryOptions);
        }

        private string WriteToFile(object key, Stream stream)
        {
            string cacheFilePath = Path.Combine(Environment.ExpandEnvironmentVariables(_cacheOptions.Path),Path.GetRandomFileName());
            stream.Seek(0, SeekOrigin.Begin);
            using (Stream file = File.Create(cacheFilePath))
            {
                stream.CopyTo(file);
            }

            _logger.LogInformation(LogEvents.SaveItemToCache,"Stream {key} saved to into file {cacheFilePath}",key, cacheFilePath);
            return cacheFilePath;
        }

        private void ClearCache(object key, object value, EvictionReason reason, object state)
        {
            var cacheItem = value as StreamFileCacheItem;
            if (cacheItem != null)
            {
                cacheItem.CancellationTokenSource.Dispose();

                string cachePath = Convert.ToString(cacheItem.FilePath);
                if (File.Exists(cachePath))
                {
                    File.Delete(cachePath);
                    _logger.LogInformation(LogEvents.RemoveItemFromCache,
                        "Cache file of {key} stream was removed {cachePath} due to {reason}.", key, cachePath, reason);
                }
                else
                {
                    _logger.LogError("Cache file of {key} stream not found {cachePath}.", key, cachePath);
                }
            }
        }

    }
}
