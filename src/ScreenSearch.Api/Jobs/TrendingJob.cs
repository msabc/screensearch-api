using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using ScreenSearch.Application.Services.Trending;
using ScreenSearch.Configuration;
using ScreenSearch.Configuration.Enums;
using ScreenSearch.Configuration.Models.Jobs;

namespace ScreenSearch.Api.Jobs
{
    public class TrendingJob(
        IServiceProvider serviceProvider,
        IOptions<ScreenSearchSettings> screenSearchOptions,
        IFeatureManager featureManager,
        ILogger<TrendingJob> logger) : BackgroundService
    {
        private readonly TrendingJobSettingsElement _trendingJobSettings = screenSearchOptions.Value.TrendingJobSettings;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation($"{nameof(TrendingJob)} is starting at {DateTime.UtcNow}.");

            using PeriodicTimer timer = new(TimeSpan.FromMinutes(_trendingJobSettings.ExecutionIntervalInMinutes));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await ExecuteTrendingJobAsync(_trendingJobSettings.Language);
                }
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation($"{nameof(TrendingJob)} is stopping at {DateTime.UtcNow}.");
            }
        }

        private async Task ExecuteTrendingJobAsync(string language)
        {
            bool isEnabled = await featureManager.IsEnabledAsync(nameof(Features.TrendingJobEnabled));

            if (!isEnabled)
            {
                logger.LogInformation($"{nameof(ExecuteTrendingJobAsync)} execution is disabled.");
                return;
            }
            
            logger.LogInformation($"{nameof(ExecuteTrendingJobAsync)} execution triggered at {DateTime.UtcNow}.");

            try
            {
                using var scope = serviceProvider.CreateScope();

                var trendingService = scope.ServiceProvider.GetRequiredService<ITrendingService>();

                await trendingService.SaveTrendingDataAsync(language);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error executing {nameof(TrendingJob)}.{nameof(ExecuteTrendingJobAsync)}: {ex.Message} {ex.InnerException?.Message}");
            }
        }
    }
}
