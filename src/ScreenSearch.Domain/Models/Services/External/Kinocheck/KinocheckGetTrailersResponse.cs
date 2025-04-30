using System.Text.Json.Serialization;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;

namespace ScreenSearch.Domain.Models.Services.External.Kinocheck
{
    public class KinocheckGetTrailersResponse
    {
        public Dictionary<string, KinocheckVideoDto> Trailers { get; set; }

        [JsonPropertyName("_metadata")]
        public Metadata Metadata { get; set; }
    }
}
