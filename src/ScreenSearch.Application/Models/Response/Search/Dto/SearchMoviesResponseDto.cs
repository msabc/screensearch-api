namespace ScreenSearch.Application.Models.Response.Search.Dto
{
    public class SearchMoviesResponseDto
    {
        public bool Adult { get; set; } = true;

        public string BackdropPath { get; set; }

        public List<int> GenreIds { get; set; }

        public int Id { get; set; }

        public string OriginalLanguage { get; set; }

        public string OriginalTitle { get; set; }

        public string Overview { get; set; }

        public double Popularity { get; set; }

        public string PosterPath { get; set; }

        public string ReleaseDate { get; set; }

        public string Title { get; set; }

        public bool Video { get; set; } = true;

        public double VoteAverage { get; set; }

        public int VoteCount { get; set; }
    }
}
