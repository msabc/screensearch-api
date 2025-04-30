using System.Runtime.Serialization;

namespace ScreenSearch.Domain.Models.Services.External.TMDB.Search.Request
{
    public class TMDBSearchSeriesRequest : BaseTMDBSearchRequest
    {
        [DataMember(Name = "first_air_date_year")]
        public int? FirstAirDateYear { get; set; }
    }
}
