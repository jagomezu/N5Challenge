using N5Challenge.Transverse.Dto;

namespace N5Challenge.Infrastructure.Interfaces
{
    public interface IEventManagerRepository
    {
        public bool PublishMessage(EventDto eventInfo);
    }
}
