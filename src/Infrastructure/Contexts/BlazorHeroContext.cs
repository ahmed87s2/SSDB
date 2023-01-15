using SSDB.Application.Interfaces.Services;
using SSDB.Application.Models.Chat;
using SSDB.Infrastructure.Models.Identity;
using SSDB.Domain.Contracts;
using SSDB.Domain.Entities.Catalog;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SSDB.Domain.Entities.ExtendedAttributes;
using SSDB.Domain.Entities.Misc;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

namespace SSDB.Infrastructure.Contexts
{
    public class BlazorHeroContext : AuditableContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTimeService _dateTimeService;

        public BlazorHeroContext(DbContextOptions<BlazorHeroContext> options, ICurrentUserService currentUserService, IDateTimeService dateTimeService)
            : base(options)
        {
            _currentUserService = currentUserService;
            _dateTimeService = dateTimeService;
        }

        public DbSet<ChatHistory<BlazorHeroUser>> ChatHistories { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Addmission> Addmissions { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Fuculty> Fuculties { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<UniversityConfigs> UniversityConfigs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<StudentsRegistrationInfo> StudentsRegistrationInfo { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentExtendedAttribute> DocumentExtendedAttributes { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTimeService.NowUtc;
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTimeService.NowUtc;
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        break;
                }
            }
            if (_currentUserService.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(_currentUserService.UserId, cancellationToken);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
            base.OnModelCreating(builder);
            builder.Entity<ChatHistory<BlazorHeroUser>>(entity =>
            {
                entity.ToTable("ChatHistory");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.ChatHistoryFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.ChatHistoryToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            builder.Entity<BlazorHeroUser>(entity =>
            {
                entity.ToTable(name: "Users", "Identity");
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<BlazorHeroRole>(entity =>
            {
                entity.ToTable(name: "Roles", "Identity");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", "Identity");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", "Identity");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", "Identity");
            });

            builder.Entity<BlazorHeroRoleClaim>(entity =>
            {
                entity.ToTable(name: "RoleClaims", "Identity");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", "Identity");
            });

            builder.Entity<Student>(entity =>
            entity.Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(50));
            
            //builder.Entity<University>().HasMany<Batch>()
            //        .WithOne()
            //        .HasForeignKey(d => d.UniversityId)
            //        .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<University>().HasMany<Department>()
            //        .WithOne()
            //        .HasForeignKey(d => d.UniversityId)
            //        .OnDelete(DeleteBehavior.NoAction);


            //builder.Entity<University>().HasMany<Fuculty>()
            //        .WithOne()
            //        .HasForeignKey(d => d.UniversityId)
            //        .OnDelete(DeleteBehavior.NoAction);


            //builder.Entity<University>().HasMany<Currency>()
            //        .WithOne()
            //        .HasForeignKey(d => d.UniversityId)
            //        .OnDelete(DeleteBehavior.NoAction);


            //builder.Entity<University>().HasMany<Addmission>()
            //        .WithOne()
            //        .HasForeignKey(d => d.UniversityId)
            //        .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<University>().HasMany<Payment>()
            //        .WithOne()
            //        .HasForeignKey(d => d.UniversityId)
            //        .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<University>().HasMany<Program>()
            //        .WithOne()
            //        .HasForeignKey(d => d.UniversityId)
            //        .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<University>().HasMany<Registration>()
            //        .WithOne()
            //        .HasForeignKey(d => d.UniversityId)
            //        .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<University>().HasMany<Semester>()
            //        .WithOne()
            //        .HasForeignKey(d => d.UniversityId)
            //        .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<University>().HasMany<Specialization>()
            //        .WithOne()
            //        .HasForeignKey(d => d.UniversityId)
            //        .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<University>().HasMany<Student>()
            //        .WithOne()
            //        .HasForeignKey(d => d.UniversityId)
            //        .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<University>().HasMany<StudentsRegistrationInfo>()
            //        .WithOne()
            //        .HasForeignKey(d => d.UniversityId)
            //        .OnDelete(DeleteBehavior.NoAction);

            //builder.Entity<University>().HasMany<Department>()
            //        .WithOne()
            //        .HasForeignKey(d => d.UniversityId)
            //        .OnDelete(DeleteBehavior.NoAction);
        }
    }
}