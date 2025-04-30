using System.Text.Json;
using System.Text.Json.Serialization;
using ScreenSearch.Domain.Models.Services.External.Kinocheck;
using ScreenSearch.Domain.Models.Services.External.Kinocheck.Dto;

namespace ScreenSearch.Infrastructure.Serialization.Kinocheck.Converters
{
    internal class KinocheckResponseConverter : JsonConverter<KinocheckGetTrailersResponse>
    {
        public override KinocheckGetTrailersResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token");
            }

            var response = new KinocheckGetTrailersResponse
            {
                Trailers = []
            };

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();

                    if (propertyName == "_metadata")
                    {
                        reader.Read();
                        response.Metadata = JsonSerializer.Deserialize<Metadata>(ref reader, options);
                    }
                    else
                    {
                        reader.Read();
                        var movie = JsonSerializer.Deserialize<KinocheckVideoDto>(ref reader, options);
                        response.Trailers[propertyName] = movie;
                    }
                }
            }

            return response;
        }

        public override void Write(Utf8JsonWriter writer, KinocheckGetTrailersResponse value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
