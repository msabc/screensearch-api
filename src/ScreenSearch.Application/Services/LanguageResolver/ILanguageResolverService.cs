using ScreenSearch.Application.Models.Enums;

namespace ScreenSearch.Application.Services.LanguageResolver
{
    public interface ILanguageResolverService
    {
        string ParseLanguage(Language language);
    }
}
