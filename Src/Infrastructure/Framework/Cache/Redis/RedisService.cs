using System.Text;
using ONLINE_SHOP.Domain.Framework.Caching;
using ONLINE_SHOP.Domain.Framework.Exceptions;
using ONLINE_SHOP.Domain.Framework.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;

namespace ONLINE_SHOP.Infrastructure.Framework.Cache.Redis;

public class RedisService : ICacheService
{
    private IDistributedCache _distributedCache;

    public RedisService(IConfiguration configuration)
    {
        if (configuration == null)
            throw new Dexception(Situation.Make(SitKeys.InvalidObject),
                new List<KeyValuePair<string, string>> {new(":موجودیت:", "تنظیمات مورد نیاز سیستم (cache)")});

        if (string.IsNullOrEmpty(configuration["ConnectionStrings:DataRedis"]))
            throw new Dexception(Situation.Make(SitKeys.InvalidObject),
                new List<KeyValuePair<string, string>> {new(":موجودیت:", "تنظیمات مورد نیاز سیستم (cache)")});

        _distributedCache = new RedisCache(new RedisCacheOptions {Configuration = configuration["ConnectionStrings:DataRedis"]});
    }

    public void SwitchServer(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
            throw new Dexception(Situation.Make(SitKeys.InvalidObject),
                new List<KeyValuePair<string, string>> {new(":موجودیت:", "تنظیمات مورد نیاز سیستم (cache)")});

        _distributedCache = new RedisCache(new RedisCacheOptions {Configuration = connectionString});
    }

    public bool Set<TModel>(string key, TModel obj, TimeSpan slidingExpire)
    {
        var serializedValue = obj is string str ? str : obj.Serialize();
        var valueEncoded = Encoding.UTF8.GetBytes(serializedValue);
        var options = new DistributedCacheEntryOptions().SetSlidingExpiration(slidingExpire);
        _distributedCache.Set(key, valueEncoded, options);
        return true;
    }

    public void Set<TModel>(string key, TModel obj, DateTimeOffset absoluteExpire)
    {
        var serializedValue = obj is string str ? str : obj.Serialize();
        var valueEncoded = Encoding.UTF8.GetBytes(serializedValue);
        var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(absoluteExpire);
        _distributedCache.Set(key, valueEncoded, options);
    }

    public async Task SetAsync<TModel>(string key, TModel obj, TimeSpan slidingExpire, CancellationToken cancellationToken)
    {
        var serializedValue = obj is string str ? str : obj.Serialize();
        var valueEncoded = Encoding.UTF8.GetBytes(serializedValue);
        var options = new DistributedCacheEntryOptions().SetSlidingExpiration(slidingExpire);
        await _distributedCache.SetAsync(key, valueEncoded, options, cancellationToken);
    }

    public async Task SetAsync<TModel>(string key, TModel obj, DateTimeOffset absoluteExpire, CancellationToken cancellationToken)
    {
        var serializedValue = obj is string str ? str : obj.Serialize();
        var valueEncoded = Encoding.UTF8.GetBytes(serializedValue);
        var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(absoluteExpire);
        await _distributedCache.SetAsync(key, valueEncoded, options, cancellationToken);
    }

    public TOutput Get<TOutput>(string key)
    {
        var bytesValue = _distributedCache.Get(key);
        if (bytesValue is not {Length: > 0})
            return default;
        var jsonValue = Encoding.UTF8.GetString(bytesValue);
        return jsonValue.Deserialize<TOutput>();
    }

    public async Task<TOutput> GetAsync<TOutput>(string key, CancellationToken cancellationToken)
    {
        var bytesValue = await _distributedCache.GetAsync(key, cancellationToken);
        if (bytesValue is not {Length: > 0})
            return default;
        var jsonValue = Encoding.UTF8.GetString(bytesValue);
        return jsonValue.Deserialize<TOutput>();
    }

    public bool Remove(string key)
    {
        var bytesValue = _distributedCache.Get(key);
        if (bytesValue is not {Length: > 0})
            return true;
        _distributedCache.Remove(key);
        return true;
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken)
    {
        var bytesValue = await _distributedCache.GetAsync(key, cancellationToken);
        if (bytesValue is not {Length: > 0})
            return;
        await _distributedCache.RemoveAsync(key, cancellationToken);
    }
}