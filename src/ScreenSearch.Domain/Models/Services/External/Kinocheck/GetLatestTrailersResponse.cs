using System.Text.Json.Serialization;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;

namespace ScreenSearch.Domain.Models.Services.External.Kinocheck
{
    public class GetLatestTrailersResponse
    {
        public Dictionary<string, KinocheckTrailerDto> Movies { get; set; }

        [JsonPropertyName("_metadata")]
        public Metadata Metadata { get; set; }
    }
}
