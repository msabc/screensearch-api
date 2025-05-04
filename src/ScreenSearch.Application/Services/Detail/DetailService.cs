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
        public async Task<GetMovieDetailResponse> GetMovieDetailsByIdAsync(int tmdbId)
        {
            string language = languageResolverService.ParseLanguage();

            GetMovieDetailResponse response = new();

            var cachedDetail = await cachedDetailRepository.GetMovieDetailsAsync(tmdbId, language);

            if (cachedDetail.Metadata == null)
            {
                response.Metadata = await tmdbService.GetMovieDetailsByIdAsync(tmdbId, language);

                await cachedDetailRepository.SaveMovieMetadataAsync(tmdbId, language, response.Metadata);
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
                    await cachedDetailRepository.SaveMovieTrailersAsync(tmdbId, language, kinocheckResponse.Videos);
                    response.Videos = kinocheckResponse.Videos;
                }
            }
            else
            {
                response.Videos = cachedDetail.Videos;
            }

            return response;
        }

        public async Task<GetSeriesDetailResponse> GetSeriesDetailsByIdAsync(int tmdbId)
        {
            string language = languageResolverService.ParseLanguage();

            GetSeriesDetailResponse response = new();

            var cachedDetail = await cachedDetailRepository.GetSeriesDetailsAsync(tmdbId, language);

            if (cachedDetail.Metadata == null)
            {
                response.Metadata = await tmdbService.GetSeriesDetailsByIdAsync(tmdbId, language);

                await cachedDetailRepository.SaveSeriesMetadataAsync(tmdbId, language, response.Metadata);
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
                    await cachedDetailRepository.SaveSeriesTrailersAsync(tmdbId, language, kinocheckResponse.Videos);
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
