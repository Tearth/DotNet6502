using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Monitor.Instructions.Converters
{
    public class HexJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(byte) == objectType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue($"0x{value:X}");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            var token = JToken.Load(reader);
            if (!(token is JValue))
            {
                throw new JsonSerializationException("Token was not a primitive");
            }

            return byte.Parse((string)token, NumberStyles.HexNumber);
        }
    }
}