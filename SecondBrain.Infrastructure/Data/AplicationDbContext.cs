using Microsoft.EntityFrameworkCore;
using SecondBrain.Domain.Entities;

namespace SecondBrain.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet dla każdej tabeli
        public DbSet<User> Users { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitCompletion> HabitCompletions { get; set; }
        public DbSet<PomodoroSession> PomodoroSessions { get; set; }
        public DbSet<TimeBlock> TimeBlocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            // Konfiguracja Task
            modelBuilder.Entity<Domain.Entities.Task>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);

                // Relacja: User -> Tasks
                entity.HasOne(e => e.User)
                    .WithMany(u => u.Tasks)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Konfiguracja Habit
            modelBuilder.Entity<Habit>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Icon).HasMaxLength(10);
                entity.Property(e => e.Color).HasMaxLength(7);

                // Relacja: User -> Habits
                entity.HasOne(e => e.User)
                    .WithMany(u => u.Habits)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Konfiguracja HabitCompletion
            modelBuilder.Entity<HabitCompletion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Note).HasMaxLength(500);

                // Relacja: Habit -> Completions
                entity.HasOne(e => e.Habit)
                    .WithMany(h => h.Completions)
                    .HasForeignKey(e => e.HabitId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Konfiguracja PomodoroSession
            modelBuilder.Entity<PomodoroSession>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Note).HasMaxLength(500);

                // Relacja: User -> PomodoroSessions
                entity.HasOne(e => e.User)
                    .WithMany(u => u.PomodoroSessions)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Opcjonalna relacja do Task
                entity.HasOne(e => e.Task)
                    .WithMany()
                    .HasForeignKey(e => e.TaskId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Konfiguracja TimeBlock
            modelBuilder.Entity<TimeBlock>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Activity).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Category).HasMaxLength(50);
                entity.Property(e => e.Color).HasMaxLength(7);

                // Relacja: User -> TimeBlocks
                entity.HasOne(e => e.User)
                    .WithMany(u => u.TimeBlocks)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        // Automatyczne ustawianie CreatedAt i UpdatedAt
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Domain.Common.BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}