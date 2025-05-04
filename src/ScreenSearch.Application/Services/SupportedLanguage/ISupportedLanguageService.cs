using ScreenSearch.Application.Models.Response.SupportedLanguage;

namespace ScreenSearch.Application.Services.SupportedLanguage
{
    public interface ISupportedLanguageService
    {
        Task<GetSupportedLanguagesResponse> GetSupportedLanguagesAsync();

        Task SaveSupportedLanguagesAsync();
    }
}
