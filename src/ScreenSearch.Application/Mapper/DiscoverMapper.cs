using ScreenSearch.Application.Models.Dto.Discover;
using ScreenSearch.Application.Models.Request.Discover;
using ScreenSearch.Application.Models.Response.Discover;
using ScreenSearch.Domain.Models.Services.TMDB.Discover;
using ScreenSearch.Domain.Models.Services.TMDB.Discover.Dto;

namespace ScreenSearch.Application.Mapper
{
    internal static class DiscoverMapper
    {
        public static GetTMDBDiscoverRequest MapToInfrastructureRequest(this DiscoverRequest request)
        {
            return new GetTMDBDiscoverRequest()
            {
                
                IncludeAdult = request.IncludeAdult,
                IncludeVideo = request.IncludeVideo,
                Language = request.Language,
                Page = request.Page,
                Year = request.Year
            };
        }

        public static DiscoverResponse MapToResponse(this GetTMDBDiscoverResponse response)
        {
            return new DiscoverResponse()
            {
                Page = response.Page,
                TotalPages = response.TotalPages,
                TotalResults = response.TotalResults,
                Results = response.Results.Select(MapToDto)
            };
        }

        private static DiscoverMovieDto MapToDto(this TMDBDiscoverMovieResponseDto dto)
        {
            return new DiscoverMovieDto()
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
