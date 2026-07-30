using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using WorkSphere.Application.Interfaces;

namespace WorkSphere.Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }
    public async Task<T?> GetAsync<T>(string key)
    {
        var cachedData = await _cache.GetStringAsync(key);

        if (cachedData == null)
            return default;

        return JsonSerializer.Deserialize<T>(cachedData);
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }

    public async Task SetAsync<T>(string key, T value)
    {

        var departmentsSerialize = JsonSerializer.Serialize<T>(value);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        };

        await _cache.SetStringAsync(key, departmentsSerialize, options);

    }
}
