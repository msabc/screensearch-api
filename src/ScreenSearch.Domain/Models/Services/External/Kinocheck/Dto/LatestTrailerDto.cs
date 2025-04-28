using System.Text.Json.Serialization;

namespace ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto
{
    public class LatestTrailerDto
    {
        public required string Id { get; set; }

        [JsonPropertyName("youtube_video_id")]
        public required string YoutubeVideoId { get; set; }

        [JsonPropertyName("youtube_channel_id")]
        public required string YoutubeChannelId { get; set; }

        [JsonPropertyName("youtube_thumbnail")]
        public required string YoutubeThumbnail { get; set; }

        public required string Title { get; set; }

        public required string Url { get; set; }

        public required string Thumbnail { get; set; }

        public required string Language { get; set; }

        public required List<string> Categories { get; set; }

        public required List<string> Genres { get; set; }

        public required DateTime Published { get; set; }

        public required long Views { get; set; }
    }
}
