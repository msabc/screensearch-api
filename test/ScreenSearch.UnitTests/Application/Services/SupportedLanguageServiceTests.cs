using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ScreenSearch.Application.Services.SupportedLanguage;
using ScreenSearch.Configuration;
using ScreenSearch.Domain.Interfaces.Repositories;
using ScreenSearch.Domain.Interfaces.Services.External.TMDB;
using ScreenSearch.Domain.Models.Caching;

namespace ScreenSearch.UnitTests.Application.Services
{
    public class SupportedLanguageServiceTests
    {
        private readonly Mock<ISupportedLanguageRepository> _supportedLanguageRepositoryMock;
        private readonly Mock<ITMDBService> _tmdbServiceMock;
        private readonly Mock<IOptions<ScreenSearchSettings>> _screenSearchOptionsMock;
        private readonly Mock<ILogger<SupportedLanguageService>> _loggerMock;

        private readonly SupportedLanguageService _supportedLanguageService;

        public SupportedLanguageServiceTests()
        {
            _supportedLanguageRepositoryMock = new Mock<ISupportedLanguageRepository>();
            _tmdbServiceMock = new Mock<ITMDBService>();
            _screenSearchOptionsMock = new Mock<IOptions<ScreenSearchSettings>>();
            _loggerMock = new Mock<ILogger<SupportedLanguageService>>();

            SetupMocks();

            _supportedLanguageService = new SupportedLanguageService(
                _supportedLanguageRepositoryMock.Object,
                _tmdbServiceMock.Object,
                _screenSearchOptionsMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task GetSupportedLanguagesAsync_WhenRepositoryDataIsPresent_TMDBServiceIsNotCalledAsync()
        {
            _supportedLanguageRepositoryMock.Setup(x => x.GetAsync()).ReturnsAsync(() => [
                new()
                {
                    ISO6391 = "en",
                    EnglishName = "English",
                    Name = "english"
                }
            ]);

            var response = await _supportedLanguageService.GetSupportedLanguagesAsync();

            _supportedLanguageRepositoryMock.Verify(x => x.GetAsync(), Times.Once);
            _tmdbServiceMock.Verify(x => x.GetLanguagesAsync(), Times.Never);

            Assert.NotNull(response);
            Assert.Single(response.Languages);
            Assert.Equal("en", response.Languages[0]);
        }

        [Fact]
        public async Task GetSupportedLanguagesAsync_WhenRepositoryDataIsNotPresent_TMDBServiceIsCalledAsync()
        {
            _supportedLanguageRepositoryMock.Setup(x => x.GetAsync()).ReturnsAsync(() => []);
            _tmdbServiceMock.Setup(x => x.GetLanguagesAsync()).ReturnsAsync(() => [
                new()
                {
                    ISO6391 = "tr",
                    EnglishName = "Turkish",
                    Name = "turkish"
                }
            ]);

            var response = await _supportedLanguageService.GetSupportedLanguagesAsync();

            _supportedLanguageRepositoryMock.Verify(x => x.GetAsync(), Times.Once);
            _tmdbServiceMock.Verify(x => x.GetLanguagesAsync(), Times.Once);

            Assert.NotNull(response);
            Assert.Single(response.Languages);
            Assert.Equal("tr", response.Languages[0]);
        }

        [Fact]
        public async Task GetSupportedLanguagesAsync_WhenRepositoryDataIsNotPresentAndTMDBServiceDataIsNotPresent_FallbackCultureIsUsedAsync()
        {
            _supportedLanguageRepositoryMock.Setup(x => x.GetAsync()).ReturnsAsync(() => []);
            _tmdbServiceMock.Setup(x => x.GetLanguagesAsync()).ReturnsAsync(() => []);

            var response = await _supportedLanguageService.GetSupportedLanguagesAsync();

            _supportedLanguageRepositoryMock.Verify(x => x.GetAsync(), Times.Once);
            _tmdbServiceMock.Verify(x => x.GetLanguagesAsync(), Times.Once);

            Assert.NotNull(response);
            Assert.Single(response.Languages);
            Assert.Equal("en", response.Languages[0]);
        }

        [Fact]
        public async Task IsLanguageSupportedAsync_WhenRepositoryAndServiceContainNoItems_FalseIsReturnedAsync()
        {
            _supportedLanguageRepositoryMock.Setup(x => x.GetAsync()).ReturnsAsync(() => []);
            _tmdbServiceMock.Setup(x => x.GetLanguagesAsync()).ReturnsAsync(() => []);

            bool supported = await _supportedLanguageService.IsLanguageSupportedAsync("en");

            Assert.True(supported);
        }

        [Theory]
        [InlineData("tr", true)]
        [InlineData("ar", true)]
        [InlineData("br", false)]
        [InlineData("ab", true)]
        [InlineData("de", false)]
        [InlineData("en", false)]
        public async Task IsLanguageSupportedAsync_WhenRepositoryAndServiceContainItems_ItemsAreCheckedForMatchAsync(string language, bool expected)
        {
            _supportedLanguageRepositoryMock.Setup(x => x.GetAsync()).ReturnsAsync(() => [
                new()
                {
                    ISO6391 = "tr",
                    EnglishName = "Turkish",
                    Name = "turkish"
                },
                new()
                {
                    ISO6391 = "ab",
                    EnglishName = "Abkhazian",
                    Name = "abkhazian"
                },
                new()
                {
                    ISO6391 = "ar",
                    EnglishName = "Akan",
                    Name = "akan"
                }
            ]);
            _tmdbServiceMock.Setup(x => x.GetLanguagesAsync()).ReturnsAsync(() => [
                new()
                {
                    ISO6391 = "tr",
                    EnglishName = "Turkish",
                    Name = "turkish"
                },
                new()
                {
                    ISO6391 = "de",
                    EnglishName = "German",
                    Name = "german"
                },
            ]);

            bool supported = await _supportedLanguageService.IsLanguageSupportedAsync(language);

            Assert.Equal(expected, supported);
        }

        [Fact]
        public async Task SaveSupportedLanguagesAsync_WhenServiceReturnsNullOrEmpty_RepositoryIsNotCalledAsync()
        {
            _tmdbServiceMock.Setup(x => x.GetLanguagesAsync()).ReturnsAsync(() => []);

            await _supportedLanguageService.SaveSupportedLanguagesAsync();

            _supportedLanguageRepositoryMock.Verify(x => x.SaveAsync(It.IsAny<List<SupportedLanguage>>()), Times.Never);
        }

        [Fact]
        public async Task SaveSupportedLanguagesAsync_WhenServiceReturnsData_DataIsSavedToRepository()
        {
            _tmdbServiceMock.Setup(x => x.GetLanguagesAsync()).ReturnsAsync(() => [
                new()
                {
                    ISO6391 = "de",
                    EnglishName = "German",
                    Name = "german"
                }
            ]);

            await _supportedLanguageService.SaveSupportedLanguagesAsync();

            _supportedLanguageRepositoryMock.Verify(x => x.SaveAsync(
                It.Is<List<SupportedLanguage>>(languages =>
                    languages.Count == 1 &&
                    languages[0].ISO6391 == "de" &&
                    languages[0].EnglishName == "German" &&
                    languages[0].Name == "german")
                ), Times.Once
            );
        }

        private void SetupMocks()
        {
            var settings = new ScreenSearchSettings()
            {
                LanguageSettings = new Configuration.Models.LanguageSettingsElement()
                {
                    FallbackCulture = "en"
                }
            };

            _screenSearchOptionsMock.Setup(x => x.Value).Returns(settings);
        }
    }
}
