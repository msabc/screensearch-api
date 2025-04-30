using ScreenSearch.Application.Models.Enums;
using ScreenSearch.Application.Models.Response.Detail;
using ScreenSearch.Application.Services.LanguageResolver;
using ScreenSearch.Domain.Interfaces.Repositories;
using ScreenSearch.Domain.Interfaces.Services.External.Kinocheck;
using ScreenSearch.Domain.Interfaces.Services.External.TMDB;

namespace ScreenSearch.Application.Services.Detail
{
    public class DetailService(
        ICachedDetailRepository cachedDetailRepository,
        ILanguageResolverService languageResolverService,
        IKinocheckService kinocheckService,
        ITMDBService tmdbService) : IDetailService
    {
        public async Task<GetMovieDetailResponse> GetMovieDetailsByIdAsync(int tmdbId, Language language)
        {
            GetMovieDetailResponse response = new();

            string requestLanguage = languageResolverService.ParseLanguage(language);

            var cachedDetail = await cachedDetailRepository.GetMovieDetailsAsync(tmdbId, requestLanguage);

            if (cachedDetail.Metadata == null)
            {
                response.Metadata = await tmdbService.GetMovieDetailsByIdAsync(tmdbId, requestLanguage);

                await cachedDetailRepository.SaveMovieMetadataAsync(tmdbId, requestLanguage, response.Metadata);
            }
            else
            {
                response.Metadata = cachedDetail.Metadata;
            }

            if (cachedDetail.Videos == null || cachedDetail.Videos.Count == 0)
            {
                var kinocheckResponse = await kinocheckService.GetMovieVideosAsync(tmdbId, requestLanguage);

                if (kinocheckResponse.Videos.Count > 0)
                {
                    await cachedDetailRepository.SaveMovieTrailersAsync(tmdbId, requestLanguage, kinocheckResponse.Videos);
                    response.Videos = kinocheckResponse.Videos;
                }
            }
            else
            {
                response.Videos = cachedDetail.Videos;
            }

            return response;
        }

        public async Task<GetSeriesDetailResponse> GetSeriesDetailsByIdAsync(int tmdbId, Language language)
        {
            GetSeriesDetailResponse response = new();

            string requestLanguage = languageResolverService.ParseLanguage(language);

            var cachedDetail = await cachedDetailRepository.GetSeriesDetailsAsync(tmdbId, requestLanguage);

            if (cachedDetail.Metadata == null)
            {
                response.Metadata = await tmdbService.GetSeriesDetailsByIdAsync(tmdbId, requestLanguage);

                await cachedDetailRepository.SaveSeriesMetadataAsync(tmdbId, requestLanguage, response.Metadata);
            }
            else
            {
                response.Metadata = cachedDetail.Metadata;
            }

            if (cachedDetail.Videos == null || cachedDetail.Videos.Count == 0)
            {
                var kinocheckResponse = await kinocheckService.GetSeriesVideosAsync(tmdbId, requestLanguage);

                if (kinocheckResponse.Videos.Count > 0)
                {
                    await cachedDetailRepository.SaveSeriesTrailersAsync(tmdbId, requestLanguage, kinocheckResponse.Videos);
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
