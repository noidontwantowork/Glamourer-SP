using System.Text.Json;
#if HAS_NEWTONSOFT
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
#endif

namespace ImSharp;

#if HAS_NEWTONSOFT
/// <summary> Conversion from and to string. </summary>
public class StringU8ConverterNewtonSoft : JsonConverter<StringU8>
{
    /// <inheritdoc/>
    public override void WriteJson(JsonWriter writer, StringU8 value, JsonSerializer serializer)
        => writer.WriteValue(value.ToString());

    /// <inheritdoc/>
    public override StringU8 ReadJson(JsonReader reader, Type objectType, StringU8 existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var text = serializer.Deserialize<string>(reader);
        if (text is null)
            return hasExistingValue ? existingValue : StringU8.Empty;

        return new StringU8(text);
    }
}
#endif

/// <summary> Conversion from and to string. </summary>
public class StringU8Converter : System.Text.Json.Serialization.JsonConverter<StringU8>
{
    /// <inheritdoc/>
    public override StringU8 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var lengthEstimate = reader.ValueSpan.Length;
        using var lease =
            ImSharpConfiguration.ArrayPool.RentLease(lengthEstimate is 0 ? ImSharpConfiguration.ArrayPoolRequestSizeLarge : lengthEstimate);

        var length = reader.CopyString(lease.Array);
        return new StringU8(lease.Array.AsSpan(0, length), false);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, StringU8 value, JsonSerializerOptions options)
        => writer.WriteStringValue(value);
}
