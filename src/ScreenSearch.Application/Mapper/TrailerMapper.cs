using ScreenSearch.Application.Models.Response;
using ScreenSearch.Application.Models.Response.Trailer;
using ScreenSearch.Domain.Models.Services.External.Kinocheck;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;

namespace ScreenSearch.Application.Mapper
{
    internal static class TrailerMapper
    {
        public static MovieTrailerDto MapToDto(this KinocheckTrailerDto response)
        {
            return new MovieTrailerDto()
            {
                Id = response.Id,
                Categories = response.Categories,
                Genres = response.Genres,
                Language = response.Language,
                Published = response.Published,
                Thumbnail = response.Thumbnail,
                Title = response.Title,
                Url = response.Url,
                Views = response.Views,
                YoutubeChannelId = response.YoutubeChannelId,
                YoutubeThumbnail = response.YoutubeThumbnail,
                YoutubeVideoId = response.YoutubeVideoId
            };
        }

        public static PagedResponse<MovieTrailerDto> MapToPagedResponse(this GetTrailersResponse response)
        {
            return new PagedResponse<MovieTrailerDto>()
            {
                Results = response.Trailers.Values.Select(MapToDto),
                Page = response.Metadata.Page,
                TotalPages = response.Metadata.TotalPages,
                TotalResults = response.Metadata.TotalCount
            };
        }
    }
}
