using System.ComponentModel.DataAnnotations;

namespace ScreenSearch.Application.Models.Request.Search.Base
{
    public class BaseSearchRequest
    {
        [Required]
        [StringLength(300)]
        public required string Query { get; set; }

        public string? Language { get; set; }

        public bool? IncludeAdult { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page should be a positive integer.")]
        public int? Page { get; set; }

        [Range(1000,9999, ErrorMessage = "Acceptable range is between 1000 and 9999.")]
        public int? Year { get; set; }
    }
}
