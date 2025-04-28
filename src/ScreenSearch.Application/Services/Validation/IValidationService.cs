using ScreenSearch.Application.Models.Request.Search;

namespace ScreenSearch.Application.Services.Validation
{
    public interface IValidationService
    {
        public void Validate(SearchRequest request);
    }
}
