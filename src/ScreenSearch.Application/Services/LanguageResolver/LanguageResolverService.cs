using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using ScreenSearch.Application.Services.SupportedLanguage;
using ScreenSearch.Configuration;

namespace ScreenSearch.Application.Services.LanguageResolver
{
    public class LanguageResolverService(
        IHttpContextAccessor httpContextAccessor, 
        ISupportedLanguageService supportedLanguageService,
        IOptions<ScreenSearchSettings> screenSearchOptions) : ILanguageResolverService
    {
        private readonly string _defaultCulture = screenSearchOptions.Value.LanguageSettings.DefaultCulture;

        public async Task<string> ParseLanguageAsync(string? languageQueryParameter)
        {
            var httpContext = httpContextAccessor.HttpContext;
            
            if (httpContext == null)
            {
                return _defaultCulture;
            }

            // check the query
            if (!string.IsNullOrWhiteSpace(languageQueryParameter) && httpContext.Request.Query.ContainsKey(languageQueryParameter))
            {
                var queryLanguage = httpContext.Request.Query[languageQueryParameter].ToString();

                if (!string.IsNullOrWhiteSpace(queryLanguage))
                {
                    if (await supportedLanguageService.IsLanguageSupportedAsync(queryLanguage))
                    {
                        return queryLanguage;
                    }
                }
            }

            // check the request
            var cultureFeature = httpContext.Features.Get<IRequestCultureFeature>();

            if (cultureFeature != null)
            {
                var requestLanguage = cultureFeature.RequestCulture.Culture?.Name;
                if (!string.IsNullOrWhiteSpace(requestLanguage))
                {
                    if (await supportedLanguageService.IsLanguageSupportedAsync(requestLanguage))
                    {
                        return requestLanguage;
                    }
                }
            }

            return _defaultCulture;
        }
    }
}
