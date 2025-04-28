using ScreenSearch.Application.Models.Dto.Search;
using ScreenSearch.Application.Models.Request.Search;
using ScreenSearch.Application.Models.Response.Search;
using ScreenSearch.Domain.Models.Services.External.TMDB.Discover;
using ScreenSearch.Domain.Models.Services.External.TMDB.Discover.Dto;

namespace ScreenSearch.Application.Mapper
{
    internal static class SearchMapper
    {
        public static TMDBGetRequest MapToInfrastructureRequest(this SearchRequest request)
        {
            return new TMDBGetRequest()
            {
                
                IncludeAdult = request.IncludeAdult,
                IncludeVideo = request.IncludeVideo,
                Language = request.Language,
                Page = request.Page,
                Year = request.Year
            };
        }

        public static SearchResponse MapToResponse(this TMDBGetResponse response)
        {
            return new SearchResponse()
            {
                Page = response.Page,
                TotalPages = response.TotalPages,
                TotalResults = response.TotalResults,
                Results = response.Results.Select(MapToDto)
            };
        }

        private static SearchDto MapToDto(this TMDBGetResponseDto dto)
        {
            return new SearchDto()
            {
                Id = dto.Id,
                Adult = dto.Adult,
                BackdropPath = dto.BackdropPath,
                GenreIds = dto.GenreIds,
                OriginalLanguage = dto.OriginalLanguage,
                OriginalTitle = dto.OriginalTitle,
                Overview = dto.Overview,
                Popularity = dto.Popularity,
                PosterPath = dto.PosterPath,
                ReleaseDate = dto.ReleaseDate,
                Title = dto.Title,
                Video = dto.Video,
                VoteAverage = dto.VoteAverage,
                VoteCount = dto.VoteCount
            };
        }
    }
}
