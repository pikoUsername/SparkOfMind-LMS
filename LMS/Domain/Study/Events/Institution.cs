using LMS.Domain.Study.Entities;
using LMS.Domain.User.Entities;

namespace LMS.Domain.Study.Events
{
    public class InstitutionCreated(InstitutionEntity institution) : DomainEvent
    {
        public InstitutionEntity Institution { get; set; } = institution; 
    }

    public class InstitutionFeeChanged(InstitutionEntity institution, decimal newFee, decimal oldFee) : DomainEvent
    {
        public InstitutionEntity Institution { get; set; } = institution;
        public decimal NewFee { get; set; } = newFee; 
        public decimal OldFee { get; set; } = oldFee;
    }

    public class InstitutionEmailChanged(InstitutionEntity institution) : DomainEvent
    {
        public InstitutionEntity Institution { get; set; } = institution;
    }

    public class InstitutionEventCreated(InstitutionEventEntity @event) : DomainEvent
    {
        public InstitutionEventEntity Event { get; set; } = @event;
    }

    public class InstitutionRoleCreated(InstitutionRolesEntity entity) : DomainEvent {
        public InstitutionRolesEntity Role { get; set; } = entity; 
    }

    public class InstitutionBlocked(InstitutionEntity institution, string reason, UserEntity blockedBy) : DomainEvent {
        public InstitutionEntity Institution { get; set; } = institution;
        public string Reason { get; set; } = reason;
        public UserEntity BlockedBy { get; } = blockedBy; 
    }

    public class InstitutionRolePermissionAdded(InstitutionEntity institution, InstitutionRolesEntity role) : DomainEvent
    {
        public InstitutionRolesEntity Role { get; } = role;
        public InstitutionEntity Institution { get; } = institution; 
    }

    public class InstitutionMarkedDeleted(InstitutionEntity institution, string reason, UserEntity blockedBy) : DomainEvent
    {
        public InstitutionEntity Institution { get; set; } = institution;
        public string Reason { get; set; } = reason;
        public UserEntity BlockedBy { get; } = blockedBy;
    }
}
