using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using ScreenSearch.Application.Services.SupportedLanguage;
using ScreenSearch.Configuration;
using ScreenSearch.Configuration.Enums;
using ScreenSearch.Configuration.Models.Jobs;

namespace ScreenSearch.Api.Jobs
{
    public class SupportedLanguagesJob(
        IServiceProvider serviceProvider,
        IOptions<ScreenSearchSettings> screenSearchOptions,
        IFeatureManager featureManager,
        ILogger<TrendingJob> logger) : BackgroundService
    {
        private readonly SupportedLanguagesJobSettingsElement _supportedLanguagesJobSettings = screenSearchOptions.Value.SupportedLanguagesJobSettings;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation($"{nameof(SupportedLanguagesJob)} is starting at {DateTime.UtcNow}.");

            using PeriodicTimer timer = new(TimeSpan.FromMinutes(_supportedLanguagesJobSettings.ExecutionIntervalInMinutes));

            try
            {
                while (await timer.WaitForNextTickAsync(stoppingToken))
                {
                    await ExecuteSupportedLanguagesJobAsync();
                }
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation($"{nameof(TrendingJob)} is stopping at {DateTime.UtcNow}.");
            }
        }

        private async Task ExecuteSupportedLanguagesJobAsync()
        {
            bool isEnabled = await featureManager.IsEnabledAsync(nameof(Features.SupportedLanguagesJobEnabled));

            if (!isEnabled)
            {
                logger.LogInformation($"{nameof(ExecuteSupportedLanguagesJobAsync)} execution is disabled.");
                return;
            }
            
            logger.LogInformation($"{nameof(ExecuteSupportedLanguagesJobAsync)} execution triggered at {DateTime.UtcNow}.");

            try
            {
                using var scope = serviceProvider.CreateScope();

                var supportedLanguageService = scope.ServiceProvider.GetRequiredService<ISupportedLanguageService>();

                await supportedLanguageService.SaveSupportedLanguagesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error executing {nameof(SupportedLanguagesJob)}.{nameof(ExecuteSupportedLanguagesJobAsync)}: {ex.Message} {ex.InnerException?.Message}");
            }
        }
    }
}
