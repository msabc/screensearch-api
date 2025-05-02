namespace ScreenSearch.Application.Services.Trending
{
    public interface ITrendingService
    {
        Task SaveTrendingDataAsync(string language);
    }
}
