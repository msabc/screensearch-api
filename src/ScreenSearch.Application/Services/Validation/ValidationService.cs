using ScreenSearch.Application.Models.Request.Discover;
using ScreenSearch.Domain.Exceptions;

namespace ScreenSearch.Application.Services.Validation
{
    internal class ValidationService : IValidationService
    {
        public void Validate(DiscoverRequest request)
        {
            if (request.Year.HasValue && 
                (request.Year.Value > DateTime.UtcNow.Year || request.Year.Value < 1900))
                throw new CustomHttpException("Invalid year provided.");
        }
    }
}
