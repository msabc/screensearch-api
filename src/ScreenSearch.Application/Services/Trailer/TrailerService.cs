using ScreenSearch.Application.Mapper;
using ScreenSearch.Application.Models.Enums;
using ScreenSearch.Application.Models.Response;
using ScreenSearch.Application.Models.Response.Trailer.Dto;
using ScreenSearch.Application.Services.LanguageResolver;
using ScreenSearch.Domain.Interfaces.Services.External.Kinocheck;

namespace ScreenSearch.Application.Services.Trailer
{
    public class TrailerService(
        ILanguageResolverService languageResolverService,
        IKinocheckService kinocheckService) : ITrailerService
    {
        public async Task<IEnumerable<MovieTrailerDto>> GetTrailersByIdAsync(int tmdbId, Language language)
        {
            string lang = languageResolverService.ParseLanguage(language);

            var response = await kinocheckService.GetTrailersAsync(tmdbId, lang);

            return response.Select(x => x.MapToDto());
        }

        public async Task<PagedResponse<MovieTrailerDto>> GetLatestAsync(Language language, int? page)
        {
            string lang = languageResolverService.ParseLanguage(language);

            var response = await kinocheckService.GetLatestTrailersAsync(page, lang);

            return response.MapToPagedResponse();
        }

        public async Task<PagedResponse<MovieTrailerDto>> GetTrendingAsync(Language language, int? page)
        {
            string lang = languageResolverService.ParseLanguage(language);

            var response = await kinocheckService.GetTrendingTrailersAsync(page, lang);

            return response.MapToPagedResponse();
        }
    }
}
