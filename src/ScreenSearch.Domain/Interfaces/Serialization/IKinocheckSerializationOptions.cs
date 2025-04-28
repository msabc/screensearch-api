using System.Text.Json;

namespace ScreenSearch.Domain.Interfaces.Serialization
{
    public interface IKinocheckSerializationOptions
    {
        JsonSerializerOptions GetOptions();
    }
}
