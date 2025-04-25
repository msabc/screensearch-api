using System.Runtime.Serialization;

namespace ScreenSearch.Domain.Models.Services.TMDB.Discover
{
    public class GetTMDBDiscoverRequest
    {
        [DataMember(Name = "include_adult")]
        public bool? IncludeAdult { get; set; }

        [DataMember(Name = "include_video")]
        public bool? IncludeVideo { get; set; }

        [DataMember(Name = "language")]
        public string? Language { get; set; }

        [DataMember(Name = "page")]
        public int? Page { get; set; }

        [DataMember(Name = "year")]
        public int? Year { get; set; }

        [DataMember(Name = "sort_by")]
        public string? SortBy { get; set; }
    }
}
