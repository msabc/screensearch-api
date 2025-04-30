using ScreenSearch.Application.Models.Request.Search;
using ScreenSearch.Application.Models.Response;
using ScreenSearch.Application.Models.Response.Search.Dto;
using ScreenSearch.Domain.Models.Services.External.TMDB.Search.Request;
using ScreenSearch.Domain.Models.Services.External.TMDB.Search.Response;
using ScreenSearch.Domain.Models.Services.External.TMDB.Search.Response.Dto;

namespace ScreenSearch.Application.Mapper
{
    internal static class SearchMapper
    {
        public static TMDBSearchMoviesRequest MapToInfrastructureRequest(this SearchMoviesRequest request)
        {
            return new TMDBSearchMoviesRequest()
            {
                Query = request.Query,
                Language = request.Language,
                Page = request.Page,
                PrimaryReleaseYear = request.PrimaryReleaseYear,
                IncludeAdult = request.IncludeAdult,
                Region = request.Region,
                Year = request.Year
            };
        }

        public static TMDBSearchSeriesRequest MapToInfrastructureRequest(this SearchSeriesRequest request)
        {
            return new TMDBSearchSeriesRequest()
            {
                Query = request.Query,
                Language = request.Language,
                Page = request.Page,
                FirstAirDateYear = request.FirstAirDateYear,
                IncludeAdult = request.IncludeAdult,
                Year = request.Year
            };
        }

        public static PagedResponse<SearchMoviesResponseDto> MapToResponse(this TMDBPagedResponse<TMDBSearchMoviesResponseDto> response)
        {
            return new PagedResponse<SearchMoviesResponseDto>()
            {
                Page = response.Page,
                TotalPages = response.TotalPages,
                TotalResults = response.TotalResults,
                Results = response.Results.Select(x => x.MapToDto())
            };
        }

        public static PagedResponse<SearchSeriesResponseDto> MapToResponse(this TMDBPagedResponse<TMDBSearchSeriesResponseDto> response)
        {
            return new PagedResponse<SearchSeriesResponseDto>()
            {
                Page = response.Page,
                TotalPages = response.TotalPages,
                TotalResults = response.TotalResults,
                Results = response.Results.Select(x => x.MapToDto())
            };
        }

        private static SearchMoviesResponseDto MapToDto(this TMDBSearchMoviesResponseDto dto)
        {
            return new SearchMoviesResponseDto()
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

        private static SearchSeriesResponseDto MapToDto(this TMDBSearchSeriesResponseDto dto)
        {
            return new SearchSeriesResponseDto()
            {
                Id = dto.Id,
                Adult = dto.Adult,
                BackdropPath = dto.BackdropPath,
                GenreIds = dto.GenreIds,
                OriginalLanguage = dto.OriginalLanguage,
                FirstAirDate = dto.FirstAirDate,
                Name = dto.Name,
                OriginalName = dto.OriginalName,
                OriginCountry = dto.OriginCountry,
                Overview = dto.Overview,
                Popularity = dto.Popularity,
                PosterPath = dto.PosterPath,
                VoteAverage = dto.VoteAverage,
                VoteCount = dto.VoteCount
            };
        }
    }
}
