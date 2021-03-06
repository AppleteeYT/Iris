using System.Text.Json;
using Kafka.Public;
using Microsoft.Extensions.Logging;

namespace Extensions
{
    public static class KafkaConsumerFactory
    {
        public static IKafkaConsumer<TKey, TValue> Create<TKey, TValue>(
            ConsumerConfig config,
            ILoggerFactory loggerFactory,
            JsonSerializerOptions options) 
            where TKey : class 
            where TValue : class
        {
            SerializationConfig serializationConfig = CreateSerializationConfig<TKey, TValue>(config, options);
            
            IClusterClient clusterClient = ClusterClientFactory.Create(
                config,
                serializationConfig,
                loggerFactory);

            var consumer = new KafkaConsumer<TKey, TValue>(
                config.DefaultTopic,
                clusterClient);

            var consumerGroupConfig = new ConsumerGroupConfiguration
            {
                DefaultOffsetToReadFrom = Offset.Earliest
            };
            consumer.Subscribe(config.GroupId, config.SubscriptionTopics, consumerGroupConfig);

            return consumer;
        }
        
        private static SerializationConfig CreateSerializationConfig<TKey, TValue>(BaseKafkaConfig config, JsonSerializerOptions options)
        {
            var serializationConfig = new SerializationConfig();

            serializationConfig.SetDefaultDeserializers(
                KafkaDeserializerFactory.CreateDeserializer<TKey>(config.KeySerializationType, options),
                KafkaDeserializerFactory.CreateDeserializer<TValue>(config.ValueSerializationType, options));
            
            return serializationConfig;
        }
    }
}