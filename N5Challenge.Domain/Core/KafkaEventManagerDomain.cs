using N5Challenge.Domain.Interfaces;
using N5Challenge.Infrastructure.Interfaces;
using N5Challenge.Transverse.Dto;
using N5Challenge.Transverse.Logger;
using Serilog;

namespace N5Challenge.Domain.Core
{
    public class KafkaEventManagerDomain : IEventManagerDomain
    {
        #region Properties
        private readonly IEventManagerRepository _repository;
        #endregion

        #region Constructor
        public KafkaEventManagerDomain(IEventManagerRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public Methods
        public bool PublishMessage(EventDto eventInfo)
        {
            try
            {
                Log.Information("[DOMAIN Kafka Publish Message] --> Message {@Message}", eventInfo);

                return _repository.PublishMessage(eventInfo);
            }
            catch (Exception ex)
            {
                LoggerUtils.WriteLogError("[DOMAIN Kafka Publish Message Error] --> Message {@Message}", ex, eventInfo);

                return false;
            }
        }
        #endregion
    }
}
