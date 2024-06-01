using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class NewEntityCreated(InstitutionNewsEntity news) : DomainEvent
    {
        public InstitutionNewsEntity News { get; set; } = news; 
    }
}
