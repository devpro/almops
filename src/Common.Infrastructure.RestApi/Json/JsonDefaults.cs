using System.Text.Json;

namespace AlmOps.Common.Infrastructure.RestApi.Json;

/// <summary>
/// JSON options of the REST API calls.
/// </summary>
public static class JsonDefaults
{
    /// <summary>
    /// Web defaults, since the APIs name their fields in camelCase and the serializer is otherwise case sensitive,
    /// and text read from a number, since the APIs send as a number some identifiers the DTOs keep as text.
    /// </summary>
    public static JsonSerializerOptions Options { get; } = new(JsonSerializerDefaults.Web)
    {
        Converters = { new LenientStringConverter() }
    };
}
