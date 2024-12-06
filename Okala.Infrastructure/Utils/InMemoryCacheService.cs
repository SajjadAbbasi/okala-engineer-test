using Microsoft.Extensions.Caching.Memory;
using Okala.Application.Interfaces.Cache;

namespace Okala.Infrastructure.Utils;

public class InMemoryCacheService(IMemoryCache cache) : ICacheService
{
    public bool TryGetValue<T>(string key,out T? value)
    {
        return cache.TryGetValue(key,out value);
    }

    public void Set<T>(string key, T content,TimeSpan? absoluteExpiration = null)
    {
        cache.Set(key, content, absoluteExpiration ?? TimeSpan.FromMinutes(1));
    }
}