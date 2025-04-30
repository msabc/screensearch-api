namespace ScreenSearch.Domain.Interfaces.Caching
{
    public interface ICacheStore
    {
        Task<string?> GetValueAsync(string key, TimeSpan? expiry);

        Task SetValueAsync(string key, string value, TimeSpan? expiry);

        Task RemoveValueAsync(string key);
    }
}
