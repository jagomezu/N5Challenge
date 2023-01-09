using N5Challenge.Transverse.Dto;

namespace N5Challenge.Domain.Interfaces
{
    public interface IEventManagerDomain
    {
        public bool PublishMessage(EventDto eventInfo);
    }
}
