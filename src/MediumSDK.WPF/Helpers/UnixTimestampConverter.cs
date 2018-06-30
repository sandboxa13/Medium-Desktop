using System;
using Newtonsoft.Json;

namespace MediumSDK.WPF.Helpers
{
    internal class UnixTimestampConverter : JsonConverter
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(((DateTime)value - _epoch).TotalMilliseconds.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == new DateTime().GetType();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return !(reader.Value is long timestamp) ? null : FromUnixTimestampMs(timestamp);
        }

        public static DateTime? FromUnixTimestampMs(long? timestamp)
        {
            if (!timestamp.HasValue) return null;
            return new DateTime(1970, 1, 1).AddMilliseconds(timestamp.Value);
        }
    }
}
