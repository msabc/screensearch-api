using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using ScreenSearch.Configuration;

namespace ScreenSearch.Application.Services.LanguageResolver
{
    public class LanguageResolverService(
        IHttpContextAccessor httpContextAccessor, 
        IOptions<ScreenSearchSettings> screenSearchOptions) : ILanguageResolverService
    {
        private readonly string _defaultCulture = screenSearchOptions.Value.LanguageSettings.DefaultCulture;

        public string ParseLanguage()
        {
            var requestCultureFeature = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            return requestCultureFeature?.RequestCulture?.Culture.Name ?? _defaultCulture;
        }
    }
}
