using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache => MemoryCache.Default;

        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }
        public void Add(string key, object data, int cacheTimeAsMinute = 60)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTimeAsMinute) };
            Cache.Add(new CacheItem(key, data), policy);
        }

        public bool IsAdd(string key)
        {
            return Cache.Contains(key);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            //                   pattern, tek satırda gönderilecek |    derlenmiş olacak   | CaseSensitive olmayacak
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = Cache.Where(c => regex.IsMatch(c.Key)).Select(kvp => kvp.Key).ToList();

            foreach (var key in keysToRemove)
                Remove(key);
        }

        public void Clear()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }
    }
}
