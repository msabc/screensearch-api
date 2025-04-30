using ScreenSearch.Application.Models.Request.Search;
using ScreenSearch.Application.Models.Request.Search.Base;

namespace ScreenSearch.Application.Services.Validation
{
    public interface IValidationService
    {
        public void Validate(BaseSearchRequest request);
    }
}
