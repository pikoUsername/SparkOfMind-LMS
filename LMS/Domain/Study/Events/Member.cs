using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class MemberAdded(InstitutionMemberEntity member) : DomainEvent
    {
        public InstitutionMemberEntity Member { get; set; } = member; 
    }

    public class MemberRolesChanged(InstitutionMemberEntity member, ICollection<InstitutionRolesEntity> roles, string operation) : DomainEvent {
        public InstitutionMemberEntity Member { get; set; } = member;
        public ICollection<InstitutionRolesEntity> Roles { get; set; } = roles;
        public string Operation { get; set; } = operation; 
    }
}
