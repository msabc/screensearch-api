using System.Reflection;
using System.Runtime.Serialization;

namespace ScreenSearch.Infrastructure.Extensions
{
    internal static class RequestObjectExtensions
    {
        internal static IDictionary<string, string> ToQueryDictionary(this object source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var properties = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var dict = new Dictionary<string, string>();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);

                if (value != null)
                {
                    // Attempt to get a DataMember attribute for custom naming
                    var dataMemberAttribute = prop.GetCustomAttribute<DataMemberAttribute>();
                    var key = dataMemberAttribute != null ? dataMemberAttribute.Name : prop.Name;

                    dict[key] = value.ToString();
                }
            }

            return dict;
        }
    }
}
