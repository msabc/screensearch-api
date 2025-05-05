using ScreenSearch.Application.Models.Response.Detail;
using ScreenSearch.Application.Services.LanguageResolver;
using ScreenSearch.Domain.Interfaces.Repositories;
using ScreenSearch.Domain.Interfaces.Services.External.Kinocheck;
using ScreenSearch.Domain.Interfaces.Services.External.TMDB;

namespace ScreenSearch.Application.Services.Detail
{
    public class DetailService(
        ILanguageResolverService languageResolverService,
        IDetailRepository cachedDetailRepository,
        IKinocheckService kinocheckService,
        ITMDBService tmdbService) : IDetailService
    {
        public async Task<GetMovieDetailResponse> GetMovieDetailsByIdAsync(int tmdbId, string? language)
        {
            string parsedLanguage = await languageResolverService.ParseLanguageAsync(nameof(language));

            GetMovieDetailResponse response = new();

            var cachedDetail = await cachedDetailRepository.GetMovieDetailsAsync(tmdbId, parsedLanguage);

            if (cachedDetail.Metadata == null)
            {
                response.Metadata = await tmdbService.GetMovieDetailsByIdAsync(tmdbId, parsedLanguage);

                await cachedDetailRepository.SaveMovieMetadataAsync(tmdbId, parsedLanguage, response.Metadata);
            }
            else
            {
                response.Metadata = cachedDetail.Metadata;
            }

            if (cachedDetail.Videos == null || cachedDetail.Videos.Count == 0)
            {
                var kinocheckResponse = await kinocheckService.GetMovieVideosAsync(tmdbId);

                if (kinocheckResponse.Videos.Count > 0)
                {
                    await cachedDetailRepository.SaveMovieTrailersAsync(tmdbId, parsedLanguage, kinocheckResponse.Videos);
                    response.Videos = kinocheckResponse.Videos;
                }
            }
            else
            {
                response.Videos = cachedDetail.Videos;
            }

            return response;
        }

        public async Task<GetSeriesDetailResponse> GetSeriesDetailsByIdAsync(int tmdbId, string? language)
        {
            string parsedLanguage = await languageResolverService.ParseLanguageAsync(nameof(language));

            GetSeriesDetailResponse response = new();

            var cachedDetail = await cachedDetailRepository.GetSeriesDetailsAsync(tmdbId, parsedLanguage);

            if (cachedDetail.Metadata == null)
            {
                response.Metadata = await tmdbService.GetSeriesDetailsByIdAsync(tmdbId, parsedLanguage);

                await cachedDetailRepository.SaveSeriesMetadataAsync(tmdbId, parsedLanguage, response.Metadata);
            }
            else
            {
                response.Metadata = cachedDetail.Metadata;
            }

            if (cachedDetail.Videos == null || cachedDetail.Videos.Count == 0)
            {
                var kinocheckResponse = await kinocheckService.GetSeriesVideosAsync(tmdbId);

                if (kinocheckResponse.Videos.Count > 0)
                {
                    await cachedDetailRepository.SaveSeriesTrailersAsync(tmdbId, parsedLanguage, kinocheckResponse.Videos);
                    response.Videos = kinocheckResponse.Videos;
                }
            }
            else
            {
                response.Videos = cachedDetail.Videos;
            }

            return response;
        }
    }
}
