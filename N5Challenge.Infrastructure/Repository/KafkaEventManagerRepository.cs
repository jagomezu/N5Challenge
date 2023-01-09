using Confluent.Kafka;
using N5Challenge.Infrastructure.Interfaces;
using N5Challenge.Transverse.Dto;
using N5Challenge.Transverse.Logger;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace N5Challenge.Infrastructure.Repository
{
    public class KafkaEventManagerRepository : IEventManagerRepository
    {
        #region Properties
        private readonly KafkaOptionsDto _options;
        #endregion

        #region Constructor
        public KafkaEventManagerRepository(KafkaOptionsDto options)
        {
            _options = options;
        }
        #endregion

        #region Public methods
        public bool PublishMessage(EventDto eventInfo)
        {
            try
            {
                Log.Information("[Kafka Publish Message] --> Topic: {@Topic} on server {@server} with message {@Message}", _options.TopicName, _options.KafkaServer, eventInfo);

                ProducerConfig config = new()
                {
                    BootstrapServers = _options.KafkaServer,
                    ClientId = Dns.GetHostName(),
                    CancellationDelayMaxMs = _options.CancellationDelayMaxMs
                };

                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var producerResponse = producer.ProduceAsync(_options.TopicName, new Message<Null, string>
                    {
                        Value = JsonConvert.SerializeObject(eventInfo)
                    });

                    Log.Information("[Kafka Publish Message] --> Topic: {@Topic} on server {@server} with message {@Message} -- {Result}", _options.TopicName, _options.KafkaServer, eventInfo, producerResponse);

                    return producerResponse != null && producerResponse.Id > 0;
                }
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("[Kafka Publish Message Error] --> Topic: {@Topic} on server {@server} with message {@Message}", ex, _options.TopicName, _options.KafkaServer, eventInfo);

                return false;
            }
        }
        #endregion
    }
}
