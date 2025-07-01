namespace CleanArch.StarterKit.Application.Services;

/// <summary>
/// Service for managing cache operations.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Retrieves a cached item by its key.
    /// </summary>
    /// <typeparam name="T">The type of the cached value.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <returns>The cached item, or null if not found.</returns>
    T? Get<T>(string key);

    /// <summary>
    /// Sets a value in the cache with an optional expiration.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="value">The value to store.</param>
    /// <param name="expiration">Optional expiration timespan.</param>
    void Set<T>(string key, T value, TimeSpan? expiration = null);

    /// <summary>
    /// Removes an item from the cache by its key.
    /// </summary>
    /// <param name="key">The cache key.</param>
    void Remove(string key);
}
