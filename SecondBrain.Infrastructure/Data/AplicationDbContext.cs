using Microsoft.EntityFrameworkCore;
using SecondBrain.Domain.Entities;
using SecondBrain.Domain.Common;

namespace SecondBrain.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ===================================================================
        // 1. DbSet dla wszystkich Encji
        // ===================================================================

        // MODUŁ 1: USER & PROFILES
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserProfile> UserProfiles { get; set; } = null!;
        public DbSet<UserSettings> UserSettings { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<ActivityLog> ActivityLogs { get; set; } = null!;
        public DbSet<UserStat> UserStats { get; set; } = null!;

        // MODUŁ 2: TASKS
        public DbSet<Domain.Entities.Task> Tasks { get; set; } = null!;
        public DbSet<TaskCategory> TaskCategories { get; set; } = null!;
        public DbSet<TaskPriority> TaskPriorities { get; set; } = null!;
        public DbSet<RecurringTaskTemplate> RecurringTaskTemplates { get; set; } = null!;
        public DbSet<RecurringTaskInstance> RecurringTaskInstances { get; set; } = null!;
        public DbSet<TaskComment> TaskComments { get; set; } = null!;
        public DbSet<TaskHistory> TaskHistory { get; set; } = null!;

        // MODUŁ 3: HABITS
        public DbSet<Habit> Habits { get; set; } = null!;
        public DbSet<HabitCategory> HabitCategories { get; set; } = null!;
        public DbSet<HabitSchedule> HabitSchedules { get; set; } = null!;
        public DbSet<HabitCompletion> HabitCompletions { get; set; } = null!;
        public DbSet<HabitStreak> HabitStreaks { get; set; } = null!;
        public DbSet<HabitReminder> HabitReminders { get; set; } = null!;

        // MODUŁ 4: DAILY REVIEW
        public DbSet<TimeCategory> TimeCategories { get; set; } = null!;
        public DbSet<TimeBlock> TimeBlocks { get; set; } = null!;
        public DbSet<TimeBlockTemplate> TimeBlockTemplates { get; set; } = null!;
        public DbSet<TimeBlockTemplateItem> TimeBlockTemplateItems { get; set; } = null!;
        public DbSet<DailyReviewStatus> DailyReviewStatuses { get; set; } = null!;

        // MODUŁ 5: POMODORO
        public DbSet<PomodoroSetting> PomodoroSettings { get; set; } = null!;
        public DbSet<PomodoroSession> PomodoroSessions { get; set; } = null!;
        public DbSet<PomodoroTemplate> PomodoroTemplates { get; set; } = null!;
        public DbSet<PomodoroStat> PomodoroStats { get; set; } = null!;

        // MODUŁ 6: REPORTS
        public DbSet<ReportTemplate> ReportTemplates { get; set; } = null!;
        public DbSet<GeneratedReport> GeneratedReports { get; set; } = null!;
        public DbSet<ExportHistory> ExportHistory { get; set; } = null!;

        // MODUŁ 7: SYSTEM
        public DbSet<SystemSetting> SystemSettings { get; set; } = null!;
        public DbSet<ErrorLog> ErrorLogs { get; set; } = null!;
        public DbSet<AuditTrail> AuditTrails { get; set; } = null!;

        // ===================================================================
        // 2. Konfiguracja Modelu (Relacje, Unikalność, Długości)
        // ===================================================================

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- KONFIGURACJA USERS (0) ---
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            });
            // --- MODUŁ 1: USER & PROFILES ---

            // 1. UserProfile (One-to-One)
            modelBuilder.Entity<UserProfile>(entity =>
            {
                // Wymuszanie relacji 1:1 przez unikalny klucz obcy
                entity.HasIndex(e => e.UserId).IsUnique();
                entity.HasOne(e => e.User)
                    .WithOne() // W encji User nie mamy jawnej kolekcji dla UserProfile
                    .HasForeignKey<UserProfile>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // 2. UserSettings (One-to-One)
            modelBuilder.Entity<UserSettings>(entity =>
            {
                entity.HasIndex(e => e.UserId).IsUnique();
                entity.HasOne(e => e.User)
                    .WithOne()
                    .HasForeignKey<UserSettings>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // 5. UserStat (One-to-One)
            modelBuilder.Entity<UserStat>(entity =>
            {
                entity.HasIndex(e => e.UserId).IsUnique();
                entity.HasOne(e => e.User)
                    .WithOne()
                    .HasForeignKey<UserStat>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // 3. Notifications, 4. ActivityLogs - Standard Many-to-One (User has many)
            modelBuilder.Entity<Notification>(e => e.Property(n => n.Title).HasMaxLength(255));
            modelBuilder.Entity<ActivityLog>(e => e.Property(n => n.Action).HasMaxLength(100));


            // --- MODUŁ 2: TASKS ---

            // 7. Tasks (Self-Referencing i relacje)
            modelBuilder.Entity<Domain.Entities.Task>(entity =>
            {
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);

                // Relacja Self-Referencing dla subtasków
                entity.HasOne(d => d.ParentTask)
                    .WithMany(p => p.Subtasks)
                    .HasForeignKey(d => d.ParentTaskId)
                    .OnDelete(DeleteBehavior.Restrict); // Zmieniono z SetNull na Restrict, aby przerwać cykl kaskadowy

                // Relacje do TaskCategory i TaskPriority
                entity.HasOne(d => d.Category).WithMany(c => c.Tasks).HasForeignKey(d => d.CategoryId).OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(d => d.Priority).WithMany(p => p.Tasks).HasForeignKey(d => d.PriorityId).OnDelete(DeleteBehavior.SetNull);

                // Relacja do User (pomijamy dla Task, bo już jest w User.cs, ale dodajemy OnDelete)
                entity.HasOne(d => d.User).WithMany(u => u.Tasks).HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.Cascade);
            });

            // 6. TaskCategories, 8. TaskPriorities - Standardowe tabele słownikowe
            modelBuilder.Entity<TaskCategory>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                // Przerwanie kaskady względem User
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // <--- KLUCZOWA ZMIANA
            });

            modelBuilder.Entity<TaskPriority>(e => e.Property(p => p.Name).IsRequired().HasMaxLength(50));

            // 9. RecurringTaskTemplates
            modelBuilder.Entity<RecurringTaskTemplate>(e => e.Property(t => t.Title).IsRequired().HasMaxLength(200));

            // 10. RecurringTaskInstances (Klucz złożony na FK)
            modelBuilder.Entity<RecurringTaskInstance>(entity =>
            {
                entity.HasIndex(e => new { e.TemplateId, e.ScheduledDate }).IsUnique();

                // Relacja: Instance -> Task. Przerwanie kaskady względem Task
                entity.HasOne(e => e.Task)
                    .WithMany()
                    .HasForeignKey(e => e.TaskId)
                    .OnDelete(DeleteBehavior.Restrict); // Zmieniono z Cascade na Restrict

                // Relacja do TemplateId pozostaje Cascade
                entity.HasOne(e => e.Template)
                    .WithMany(t => t.Instances)
                    .HasForeignKey(e => e.TemplateId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // 12. TaskHistory - FK do ChangedBy (User)
            modelBuilder.Entity<TaskHistory>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany() // Nie musi mieć kolekcji w User
                    .HasForeignKey(d => d.ChangedBy)
                    .OnDelete(DeleteBehavior.Restrict); // Nie kasujemy historii przy kasowaniu usera
            });


            // --- MODUŁ 3: HABITS ---

            // 14. Habits (Relacje)
            modelBuilder.Entity<Habit>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasOne(d => d.Category).WithMany(c => c.Habits).HasForeignKey(d => d.CategoryId).OnDelete(DeleteBehavior.SetNull);
                // Relacja Many-to-One do User jest w User.cs
            });

            // 17. HabitStreaks (One-to-One)
            modelBuilder.Entity<HabitStreak>(entity =>
            {
                entity.HasIndex(e => e.HabitId).IsUnique();
                entity.HasOne(e => e.Habit)
                    .WithOne(h => h.Streak)
                    .HasForeignKey<HabitStreak>(e => e.HabitId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // 15. HabitSchedules (Klucz złożony na FK)
            modelBuilder.Entity<HabitSchedule>(entity =>
            {
                entity.HasIndex(e => new { e.HabitId, e.DayOfWeek }).IsUnique();
            });
            modelBuilder.Entity<HabitCompletion>(entity =>
            {
                // Przyjmujemy standardową precyzję 18 cyfr, 2 po przecinku
                entity.Property(e => e.Value).HasPrecision(18, 2);
            });

            // HabitCategory - przerwanie kaskady względem User
            modelBuilder.Entity<HabitCategory>(entity =>
            {
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // <--- KLUCZOWA ZMIANA
            });
            modelBuilder.Entity<HabitCompletion>(entity =>
            {
                // Przyjmujemy standardową precyzję 18 cyfr, 2 po przecinku
                entity.Property(e => e.Value).HasPrecision(18, 2);
                // ...
            });

            // --- MODUŁ 4: DAILY REVIEW ---

            // 19. TimeCategories
            modelBuilder.Entity<TimeCategory>(entity =>
            {
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                // Przerwanie kaskady względem User
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // KLUCZOWA ZMIANA
            });

            // 20. TimeBlockTemplateItem - przerwanie kaskady względem TimeCategory
            modelBuilder.Entity<TimeBlockTemplateItem>(entity =>
            {
                entity.HasOne(e => e.Category)
                    .WithMany(c => c.TemplateItems)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict); // KLUCZOWA ZMIANA
            });

            // 20. TimeBlocks (Relacje)
            modelBuilder.Entity<TimeBlock>(entity =>
            {
                // Przerwanie kaskady względem User
                entity.HasOne(e => e.User)
                    .WithMany(u => u.TimeBlocks)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // <--- KLUCZOWA I OSTATECZNA ZMIANA

                // Relacja do TimeCategories pozostaje RESTRICT
                entity.HasOne(d => d.Category).WithMany(c => c.TimeBlocks).HasForeignKey(d => d.CategoryId).OnDelete(DeleteBehavior.Restrict);

                // Relacja do Task pozostaje SET NULL
                entity.HasOne(d => d.Task).WithMany().HasForeignKey(d => d.TaskId).OnDelete(DeleteBehavior.SetNull);

                // W TimeBlock usunięto pola string Category i Color na rzecz FK
            });

            // 23. DailyReviewStatus (Klucz złożony/Unikalny Index)
            modelBuilder.Entity<DailyReviewStatus>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.ReviewDate }).IsUnique();
                entity.Property(e => e.ProductiveHours).HasPrecision(18, 2);
            });

            // --- MODUŁ 5: POMODORO ---

            // 24. PomodoroSettings (One-to-One)
            modelBuilder.Entity<PomodoroSetting>(entity =>
            {
                entity.HasIndex(e => e.UserId).IsUnique();
                entity.HasOne(e => e.User)
                    .WithOne()
                    .HasForeignKey<PomodoroSetting>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // 27. PomodoroStats (Klucz złożony/Unikalny Index)
            modelBuilder.Entity<PomodoroStat>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.Date }).IsUnique();
            });

            // 25. PomodoroSessions - Relacja do Task jest już zdefiniowana w Twoim kodzie, więc zostawiamy.
            // Upewnij się, że usunąłeś stary 'public DbSet<Domain.Entities.Task> Tasks { get; set; }'
            // z Twojej implementacji, jeśli nie używasz using SecondBrain.Domain.Entities.

            // --- MODUŁ 7: SYSTEM ---

            // 31. SystemSettings (Unikalny klucz)
            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.HasIndex(e => e.SettingKey).IsUnique();
                entity.Property(e => e.SettingKey).IsRequired().HasMaxLength(100);

                entity.HasOne(d => d.UpdatedByUser) // Zgodnie z encją
                    .WithMany()
                    .HasForeignKey(d => d.UpdatedBy)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // 33. AuditTrail
            modelBuilder.Entity<AuditTrail>(e => e.Property(a => a.Action).HasMaxLength(50));
        }

        // Automatyczne ustawianie CreatedAt i UpdatedAt
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedAt = DateTime.UtcNow; // Dodane, aby UpdatedAt nie był null przy tworzeniu
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