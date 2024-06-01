using Microsoft.EntityFrameworkCore;
using System.Reflection;
using LMS.Domain.Files.Entities;
using LMS.Domain.Payment.Entities;
using LMS.Domain.Staff.Entities;
using LMS.Domain.User.Entities;
using LMS.Application.Common.Interfaces;
using LMS.Infrastructure.EventDispatcher;
using LMS.Domain.Study.Entities;

namespace LMS.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        // User 
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<WarningEntity> UserWarnings { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }

        // payment 
        public DbSet<TransactionProviderEntity> TransactionProviders { get; set; }
        public DbSet<PaymentSystemEntity> PaymentSystems { get; set; }
        public DbSet<PurchaseEntity> Purchases { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<WalletEntity> Wallets { get; set; }

        // Staff 
        public DbSet<TicketSubjectEntity> TicketSubjects { get; set; }
        public DbSet<TicketEntity> Tickets { get; set; }
        public DbSet<TicketCommentEntity> TicketComments { get; set; }

        // Files 
        public DbSet<FileEntity> Files { get; set; }

        // Study 
        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<StudentCourseEntity> StudentCourses { get; set; }
        public DbSet<InstitutionEntity> Institutions { get; set; }
        public DbSet<AssignmentEntity> Assigments { get; set; }
        public DbSet<AttendanceEntity> Attendance { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CourseGroupEntity> CourseGroups { get; set; }
        public DbSet<DayLessonEntity> DayLessons { get; set; }
        public DbSet<DayScheduleEntity> DaySchedules { get; set; }
        public DbSet<ExaminationEntity> Examinations { get; set; }
        public DbSet<GradeTypeEntity> GradeTypes { get; set; }
        public DbSet<SubmissionEntity> Submissions { get; set; }
        public DbSet<InstitutionEventEntity> InstitutionEvents { get; set; }
        public DbSet<InstitutionMemberEntity> InstitutionMembers { get; set; }
        public DbSet<InstitutionNewsEntity> InstitutionNews { get; set; }
        public DbSet<InstitutionRolesEntity> InstitutionRoles { get; set; }
        public DbSet<TeacherEntity> Teachers { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<InvitationEntity> Invitations { get; set; }
        public DbSet<BookRentEntity> BookRents { get; set; }

        private readonly IEventDispatcher _dispatcher;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IEventDispatcher eventDispatcher) : base(options)
        {
            _dispatcher = eventDispatcher;
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;  // ??????? TODO: need to research 
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await DispatchDomainEventsAsync();
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
        }

        private async Task DispatchDomainEventsAsync()
        {
            var entities = ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities.ToList().ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                try
                {
                    await _dispatcher.Dispatch(domainEvent, this);
                }
                catch
                {
                    // Обработка ошибок при диспетчеризации событий
                    // Можно добавить логирование или другие механизмы обработки ошибок
                    throw; // Можно выбрасывать исключение или обрабатывать ошибку по желанию
                }
            }
        }
    }
}
