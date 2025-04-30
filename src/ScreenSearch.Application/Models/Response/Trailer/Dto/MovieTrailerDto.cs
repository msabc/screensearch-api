namespace ScreenSearch.Application.Models.Response.Trailer.Dto
{
    public class MovieTrailerDto
    {
        public string Id { get; set; }

        public string YoutubeVideoId { get; set; }

        public string YoutubeChannelId { get; set; }

        public string YoutubeThumbnail { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Thumbnail { get; set; }

        public string Language { get; set; }

        public List<string> Categories { get; set; }

        public List<string> Genres { get; set; }

        public DateTime Published { get; set; }

        public long Views { get; set; }
    }
}
