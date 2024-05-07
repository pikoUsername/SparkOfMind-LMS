using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using LMS.Domain.Files.Entities;
using LMS.Domain.Payment.Entities;
using LMS.Domain.Staff.Entities;
using LMS.Domain.User.Entities;

namespace LMS.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<UserEntity> Users { get; set; }
        DbSet<TransactionProviderEntity> TransactionProviders { get; set; }
        DbSet<PaymentSystemEntity> PaymentSystems { get; set; }
        DbSet<PermissionEntity> Permissions { get; set; }
        DbSet<GroupEntity> Groups { get; set; }
        DbSet<RoleEntity> Roles { get; set; }
        DbSet<TicketSubjectEntity> TicketSubjects { get; set; }
        DbSet<NotificationEntity> Notifications { get; set; }
        DbSet<PurchaseEntity> Purchases { get; set; }
        DbSet<TransactionEntity> Transactions { get; set; }
        DbSet<WalletEntity> Wallets { get; set; }
        DbSet<FileEntity> Files { get; set; }
        DbSet<TicketEntity> Tickets { get; set; }
        DbSet<TicketCommentEntity> TicketComments { get; set; }
        DbSet<WarningEntity> UserWarnings { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Entry(object entity);
    }
}
