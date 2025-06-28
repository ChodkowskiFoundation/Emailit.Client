using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Emailit.Client.Json
{
    internal class CaseInsensitiveEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string enumText = reader.GetString()?.ToLower();

            foreach (T enumValue in Enum.GetValues(typeof(T)))
            {
                if (enumValue.ToString().ToLower() == enumText) return enumValue;
            }

            throw new JsonException($"Unable to convert \"{enumText}\" to enum {typeof(T)}");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) 
            => writer.WriteStringValue(value.ToString().ToLower());
    }
}
