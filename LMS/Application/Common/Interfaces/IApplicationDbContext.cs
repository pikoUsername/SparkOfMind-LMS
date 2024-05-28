using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using LMS.Domain.Files.Entities;
using LMS.Domain.Payment.Entities;
using LMS.Domain.Staff.Entities;
using LMS.Domain.User.Entities;
using LMS.Domain.Study.Entities;

namespace LMS.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        // User 
        DbSet<UserEntity> Users { get; set; }
        DbSet<PermissionEntity> Permissions { get; set; }
        DbSet<GroupEntity> Groups { get; set; }
        DbSet<RoleEntity> Roles { get; set; }
        DbSet<WarningEntity> UserWarnings { get; set; }
        DbSet<NotificationEntity> Notifications { get; set; }

        // payment 
        DbSet<TransactionProviderEntity> TransactionProviders { get; set; }
        DbSet<PaymentSystemEntity> PaymentSystems { get; set; }
        DbSet<PurchaseEntity> Purchases { get; set; }
        DbSet<TransactionEntity> Transactions { get; set; }
        DbSet<WalletEntity> Wallets { get; set; }

        // Staff 
        DbSet<TicketSubjectEntity> TicketSubjects { get; set; }
        DbSet<TicketEntity> Tickets { get; set; }
        DbSet<TicketCommentEntity> TicketComments { get; set; }

        // Files 
        DbSet<FileEntity> Files { get; set; }

        // Study 
        DbSet<StudentEntity> Students { get; set; }
        DbSet<StudentCourseEntity> StudentCourses { get; set; }
        DbSet<InstitutionEntity> Institutions { get; set; }
        DbSet<AssignmentEntity> Assigments { get; set; }
        DbSet<AttendanceEntity> Attendance { get; set; }
        DbSet<BookEntity> Books { get; set; }
        DbSet<CategoryEntity> Categories { get; set; }
        DbSet<CourseGroupEntity> CourseGroups { get; set; }
        DbSet<DayLessonEntity> DayLessons { get; set; }
        DbSet<DayScheduleEntity> DaySchedules { get; set; }
        DbSet<ExaminationEntity> Examinations { get; set; }
        DbSet<GradeTypeEntity> GradeTypes { get; set; }
        DbSet<SubmissionEntity> Submissions { get; set; }
        DbSet<InstitutionEventEntity> InstitutionEvents { get; set; }
        DbSet<InstitutionMemberEntity> InstitutionMembers { get; set; }
        DbSet<InstitutionNewsEntity> InstitutionNews { get; set; }
        DbSet<InstitutionRolesEntity> InstitutionRoles { get; set; }
        DbSet<TeacherEntity> Teachers { get; set; }
        DbSet<CourseEntity> Courses { get; set; }
        DbSet<BookRentEntity> BookRents { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Entry(object entity);
    }
}
