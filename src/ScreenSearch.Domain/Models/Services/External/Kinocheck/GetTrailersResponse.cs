using System.Text.Json.Serialization;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;

namespace ScreenSearch.Domain.Models.Services.External.Kinocheck
{
    public class GetTrailersResponse
    {
        public Dictionary<string, KinocheckTrailerDto> Trailers { get; set; }

        [JsonPropertyName("_metadata")]
        public Metadata Metadata { get; set; }
    }
}
