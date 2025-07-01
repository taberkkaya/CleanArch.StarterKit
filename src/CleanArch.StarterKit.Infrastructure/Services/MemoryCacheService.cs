using CleanArch.StarterKit.Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArch.StarterKit.Infrastructure.Services;

/// <summary>
/// Provides in-memory caching implementation using <see cref="IMemoryCache"/>.
/// </summary>
public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public MemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    /// <summary>
    /// Retrieves a cached item by its key.
    /// </summary>
    public T? Get<T>(string key)
    {
        _cache.TryGetValue(key, out T? value);
        return value;
    }

    /// <summary>
    /// Stores a value in the cache with optional expiration.
    /// </summary>
    public void Set<T>(string key, T value, TimeSpan? expiration = null)
    {
        var options = new MemoryCacheEntryOptions();
        if (expiration.HasValue)
            options.SetAbsoluteExpiration(expiration.Value);

        _cache.Set(key, value, options);
    }

    /// <summary>
    /// Removes an item from the cache by its key.
    /// </summary>
    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}
