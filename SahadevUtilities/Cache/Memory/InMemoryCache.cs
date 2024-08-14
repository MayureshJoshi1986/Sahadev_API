using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using SahadevUtilities.Cache.Core;

namespace SahadevUtilities.Cache.Memory
{
    public class InMemoryCache : CacheBase, ICache, IDisposable
    {
        private static readonly MemoryCache _cache = MemoryCache.Default;
        public static readonly InMemoryCache Instance = new InMemoryCache();


        InMemoryCache()
        { }

        public override bool Add(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            return _cache.Add(key, value, absoluteExpiration.GetValueOrDefault(DateTimeOffset.MaxValue));
        }
        public override bool Add<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            return _cache.Add(key, value, absoluteExpiration.GetValueOrDefault(DateTimeOffset.MaxValue));
        }
        public override bool Add(string key, object value, TimeSpan slidingExpiration)
        {
            return _cache.Add(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }
        public override bool Add<T>(string key, T value, TimeSpan slidingExpiration)
        {
            return _cache.Add(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }

        public override object GetValueOrAdd(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            return _cache.AddOrGetExisting(key, value, absoluteExpiration.GetValueOrDefault(DateTimeOffset.MaxValue));
        }
        public override T GetValueOrAdd<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            return (T)_cache.AddOrGetExisting(key, value, absoluteExpiration.GetValueOrDefault(DateTimeOffset.MaxValue));
        }
        public override object GetValueOrAdd(string key, object value, TimeSpan slidingExpiration)
        {
            return _cache.AddOrGetExisting(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }
        public override T GetValueOrAdd<T>(string key, T value, TimeSpan slidingExpiration)
        {
            return (T)_cache.AddOrGetExisting(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }


        public override void Set(string key, object value, DateTimeOffset? absoluteExpiration = null)
        {
            _cache.Set(key, value, absoluteExpiration.GetValueOrDefault(DateTimeOffset.MaxValue));
        }
        public override void Set<T>(string key, T value, DateTimeOffset? absoluteExpiration = null)
        {
            _cache.Set(key, value, absoluteExpiration.GetValueOrDefault(DateTimeOffset.MaxValue));
        }
        public override void Set(string key, object value, TimeSpan slidingExpiration)
        {
            _cache.Set(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }
        public override void Set<T>(string key, T value, TimeSpan slidingExpiration)
        {
            _cache.Set(key, value, new CacheItemPolicy { SlidingExpiration = slidingExpiration });
        }

        public override bool Contains(string key)
        {
            return _cache.Contains(key);
        }

        public override object Get(string key)
        {
            return _cache.Get(key);
        }
        public override T Get<T>(string key)
        {
            return (T)_cache.Get(key);
        }

        public override IEnumerable<KeyValuePair<string, object>> GetAll(IEnumerable<string> keys)
        {
            return (IEnumerable<KeyValuePair<string, object>>)_cache.GetValues(keys).Select(i => new KeyValuePair<string, object>(i.Key, i.Value));
        }
        public override IEnumerable<KeyValuePair<string, T>> GetAll<T>(IEnumerable<string> keys)
        {
            return (IEnumerable<KeyValuePair<string, T>>)_cache.GetValues(keys).Select(i => new KeyValuePair<string, T>(i.Key, (T)i.Value));
        }

        public override object Remove(string key)
        {
            return _cache.Remove(key);
        }
        public override T Remove<T>(string key)
        {
            return (T)_cache.Remove(key);
        }

        public override long GetCount()
        {
            return _cache.GetCount();
        }

        public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _cache.AsEnumerable().GetEnumerator();
        }

        public override void ClearCache()
        {
            throw new NotImplementedException();
        }

        public override void Compact()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }



        //public T GetValue<T>(string key)
        //{
        //    return (T)_cache.Get(key);
        //}

        //public bool Add(string key, object value, DateTimeOffset absExpiration)
        //{
        //    return _cache.Add(key, value, absExpiration);
        //}

        //public void Delete(string key)
        //{
        //    if (_cache.Contains(key))
        //        _cache.Remove(key);

        //}
    }
}
