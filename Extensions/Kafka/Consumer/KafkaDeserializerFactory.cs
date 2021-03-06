using System.Text.Json;
using Kafka.Public;

namespace Extensions
{
    internal static class KafkaDeserializerFactory
    {
        public static IDeserializer CreateDeserializer<T>(SerializationType type, JsonSerializerOptions options)
        {
            return type switch 
            {
                SerializationType.Json => new KafkaJsonDeserializer<T>(options),
                _ => new StringDeserializer()
            };
        }
    }
}