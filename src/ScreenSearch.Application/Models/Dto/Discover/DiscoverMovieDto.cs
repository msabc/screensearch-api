using System.Text.Json.Serialization;

namespace ScreenSearch.Application.Models.Dto.Discover
{
    public class DiscoverMovieDto
    {
        public bool Adult { get; set; } = true;

        public string BackdropPath { get; set; }

        public List<int> GenreIds { get; set; } = [];

        public int Id { get; set; } = 0;

        public string OriginalLanguage { get; set; }

        public string OriginalTitle { get; set; }

        public string Overview { get; set; }

        public double Popularity { get; set; } = 0;

        public string PosterPath { get; set; }

        public string ReleaseDate { get; set; }

        public string Title { get; set; }

        public bool Video { get; set; } = true;

        public double VoteAverage { get; set; } = 0;

        public int VoteCount { get; set; } = 0;
    }
}
