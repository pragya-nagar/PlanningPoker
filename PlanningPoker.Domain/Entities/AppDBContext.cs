using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PlanningPoker.Domain.Entities
{
    public partial class AppDBContext : DbContext
    {
        public AppDBContext()
        {
        }

        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Email> Email { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameSession> GameSession { get; set; }
        public virtual DbSet<HtmlTemplate> HtmlTemplate { get; set; }
        public virtual DbSet<InviteUser> InviteUser { get; set; }
        public virtual DbSet<ResetPassword> ResetPassword { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserSession> UserSession { get; set; }
        public virtual DbSet<UserStory> UserStory { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<UserStoryEstimate> UserStoryEstimate { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-BK6JOTR;Database=PlanningPoker_V1;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>(entity =>
            {
                entity.Property(e => e.Attachment).HasMaxLength(2048);

                entity.Property(e => e.Bccname).HasColumnName("BCCName");

                entity.Property(e => e.Ccemail).HasColumnName("CCEmail");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FromEmail).HasMaxLength(256);

                entity.Property(e => e.FromName).HasMaxLength(256);

                entity.Property(e => e.Subject).HasMaxLength(2048);

                entity.Property(e => e.ToEmail).HasMaxLength(256);

                entity.Property(e => e.ToName).HasMaxLength(256);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.GameName).IsRequired();

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<GameSession>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.SessionTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<HtmlTemplate>(entity =>
            {
                entity.HasKey(e => e.TamplateId)
                    .HasName("HtmlTemplate_PK");

                entity.Property(e => e.TamplateId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Heading).HasMaxLength(1024);

                entity.Property(e => e.Subject).HasMaxLength(1024);

                entity.Property(e => e.TamplateName)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<InviteUser>(entity =>
            {
                entity.HasKey(e => e.InviteUserId)
                    .HasName("InviteUser_PK");

                //entity.Property(e => e.InviteUserId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(512);

                //entity.Property(e => e.GameId).ValueGeneratedOnAdd();

                entity.Property(e => e.Reason).HasMaxLength(1024);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<ResetPassword>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ResetToken)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LastName).HasMaxLength(256);

                entity.Property(e => e.PasswordHash).HasMaxLength(1024);

                entity.Property(e => e.PasswordSalt).HasMaxLength(1024);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("UserRole_PK");

                entity.Property(e => e.RoleId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserSession>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("UserSession_PK");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.SessionId).ValueGeneratedOnAdd();

                entity.Property(e => e.SessionToken)
                    .IsRequired()
                    .HasMaxLength(1024);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserStory>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.UserStory1)
                    .IsRequired()
                    .HasColumnName("UserStory");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.Property(e => e.UserTypeId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.UserTypeName)
                    .IsRequired()
                    .HasMaxLength(256);
            });
            modelBuilder.Entity<UserStoryEstimate>(entity =>
            {
                entity.HasKey(e => e.UserStoryEstimateId)
                    .HasName("UserStoryEstimate_PK");
                entity.Property(e => e.UserStoryEstimateId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(1024);

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
