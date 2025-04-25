using ScreenSearch.Application.Models.Request.Discover;

namespace ScreenSearch.Application.Services.Validation
{
    public interface IValidationService
    {
        public void Validate(DiscoverRequest request);
    }
}
