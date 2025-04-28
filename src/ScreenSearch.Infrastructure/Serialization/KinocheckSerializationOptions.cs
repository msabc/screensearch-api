using System.Text.Json;
using ScreenSearch.Domain.Interfaces.Serialization;
using ScreenSearch.Infrastructure.Serialization.Kinocheck.Converters;

namespace ScreenSearch.Infrastructure.Serialization
{
    public class KinocheckSerializationOptions : IKinocheckSerializationOptions
    {
        private readonly JsonSerializerOptions _options;

        public KinocheckSerializationOptions()
        {
            _options =  new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new KinocheckResponseConverter() }
            };
        }

        public JsonSerializerOptions GetOptions() => _options;
    }
}
