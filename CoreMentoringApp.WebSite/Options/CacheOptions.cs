using System.ComponentModel.DataAnnotations;

namespace CoreMentoringApp.WebSite.Options
{
    public class CacheOptions
    {
        public const string Cache = "Cache";

        public string Path { get; set; }

        [Range(0, 100)]
        public int MaxCount { get; set; }

        public long Expiration { get; set; }
    }
}

