namespace ONLINE_SHOP.Domain.Framework.Caching;

public interface ICacheService
{
    void SwitchServer(string connectionString);
    bool Set<TModel>(string key, TModel obj, TimeSpan slidingExpire);
    void Set<TModel>(string key, TModel obj, DateTimeOffset absoluteExpire);
    Task SetAsync<TModel>(string key, TModel obj, TimeSpan slidingExpire, CancellationToken cancellationToken);
    Task SetAsync<TModel>(string key, TModel obj, DateTimeOffset absoluteExpire, CancellationToken cancellationToken);
    TOutput Get<TOutput>(string key);
    Task<TOutput> GetAsync<TOutput>(string key, CancellationToken cancellationToken);
    bool Remove(string key);
    Task RemoveAsync(string key, CancellationToken cancellationToken);
}