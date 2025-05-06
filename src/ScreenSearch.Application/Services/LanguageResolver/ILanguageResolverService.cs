namespace ScreenSearch.Application.Services.LanguageResolver
{
    public interface ILanguageResolverService
    {
        Task<string> ParseLanguageAsync();
    }
}
