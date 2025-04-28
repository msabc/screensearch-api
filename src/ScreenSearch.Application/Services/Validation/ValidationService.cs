using ScreenSearch.Application.Models.Request.Search;
using ScreenSearch.Domain.Exceptions;

namespace ScreenSearch.Application.Services.Validation
{
    internal class ValidationService : IValidationService
    {
        public void Validate(SearchRequest request)
        {
            if (request.Year.HasValue && 
                (request.Year.Value > DateTime.UtcNow.Year || request.Year.Value < 1900))
                throw new CustomHttpException("Invalid year provided.");
        }
    }
}
