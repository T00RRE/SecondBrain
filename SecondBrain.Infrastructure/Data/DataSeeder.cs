using Microsoft.EntityFrameworkCore;
using SecondBrain.Domain.Entities;
using SecondBrain.Infrastructure.Data;
using Task = SecondBrain.Domain.Entities.Task; // Używamy aliasu, bo Task jest też klasą w .NET

namespace SecondBrain.Infrastructure.Data.Seed
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;

        // Konstruktor przyjmujący kontekst bazy danych
        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        // Główna metoda do uruchomienia seedingu
        public async System.Threading.Tasks.Task SeedAsync()
        {
            // Upewnij się, że istnieje systemowy użytkownik (owner danych globalnych)
            var systemUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == "system@secondbrain.local");
            if (systemUser == null)
            {
                systemUser = new User
                {
                    Email = "system@secondbrain.local",
                    Username = "SystemAdmin",
                    PasswordHash = "Placeholder",
                    FirstName = "System",
                    LastName = "Admin",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _context.Users.AddAsync(systemUser);
                await _context.SaveChangesAsync();
            }

            // --- 1. Dane początkowe (TaskPriorities i TimeCategories) ---
            await SeedTaskPriorities(systemUser.Id);
            await SeedTimeCategories(systemUser.Id);

            // Upewnij się, że zmiany są zapisane, aby pobrać ID utworzonych danych
            await _context.SaveChangesAsync();

            // --- 2. Testowy użytkownik i powiązane dane ---
            await SeedTestUserAndData();
        }

        private async System.Threading.Tasks.Task SeedTaskPriorities(int systemUserId)
        {
            if (!await _context.TaskPriorities.AnyAsync())
            {
                var priorities = new List<TaskPriority>
                {
                    new TaskPriority { Name = "Urgent", Color = "#FF0000", Level = 4, UserId = systemUserId, IsDefault = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new TaskPriority { Name = "High", Color = "#FF8C00", Level = 3, UserId = systemUserId, IsDefault = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new TaskPriority { Name = "Medium", Color = "#FFD700", Level = 2, UserId = systemUserId, IsDefault = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new TaskPriority { Name = "Low", Color = "#3CB371", Level = 1, UserId = systemUserId, IsDefault = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                };
                await _context.TaskPriorities.AddRangeAsync(priorities);
                // Nie zapisujemy od razu, robimy to po TimeCategories
            }
        }

        private async System.Threading.Tasks.Task SeedTimeCategories(int systemUserId)
        {
            if (!await _context.TimeCategories.AnyAsync())
            {
                var categories = new List<TimeCategory>
                {
                    new TimeCategory { Name = "Praca", Color = "#1E90FF", UserId = systemUserId, IsProductiveTime = true, OrderIndex = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new TimeCategory { Name = "Nauka", Color = "#DA70D6", UserId = systemUserId, IsProductiveTime = true, OrderIndex = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new TimeCategory { Name = "Sport", Color = "#3CB371", UserId = systemUserId, IsProductiveTime = true, OrderIndex = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new TimeCategory { Name = "Rozrywka", Color = "#FFA07A", UserId = systemUserId, IsProductiveTime = false, OrderIndex = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new TimeCategory { Name = "Inne", Color = "#808080", UserId = systemUserId, IsProductiveTime = false, OrderIndex = 5, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                };
                await _context.TimeCategories.AddRangeAsync(categories);
            }
        }

        private async System.Threading.Tasks.Task SeedTestUserAndData()
        {
            if (!await _context.Users.AnyAsync(u => u.Email == "test@secondbrain.com"))
            {
                // 1. Dodać testowego użytkownika
                var testUser = new User
                {
                    Email = "test@secondbrain.com",
                    Username = "testUser",
                    PasswordHash = "SimpleHashForNow", // W docelowej aplikacji, to musi być hashowane!
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _context.Users.AddAsync(testUser);
                await _context.SaveChangesAsync(); // potrzebne, aby otrzymać wygenerowane Id

                // 2. Dodać profil i ustawienia
                await _context.UserProfiles.AddAsync(new UserProfile
                {
                    UserId = testUser.Id,
                    Avatar = null,
                    Bio = "Testowy profil użytkownika",
                    DateOfBirth = null,
                    Phone = null,
                    TimeZone = "Europe/Warsaw",
                    PreferredLanguage = "pl",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });

                await _context.UserSettings.AddAsync(new UserSettings
                {
                    UserId = testUser.Id,
                    Theme = "light",
                    PomodoroWorkDuration = 25,
                    PomodoroShortBreak = 5,
                    PomodoroLongBreak = 15,
                    DailyTaskGoal = 5,
                    WeekStartsOn = 1,
                    NotificationsEnabled = true,
                    SoundEnabled = true,
                    AutoStartPomodoro = false,
                    ShowCompletedTasks = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });

                // Pobranie ID dla relacji priorytetów
                var mediumPriority = await _context.TaskPriorities.FirstOrDefaultAsync(p => p.Name == "Medium");
                var highPriority = await _context.TaskPriorities.FirstOrDefaultAsync(p => p.Name == "High");

                // 3. Dodać przykładowe zadania
                var testTasks = new List<Task>
                {
                    new Task
                    {
                        UserId = testUser.Id,
                        Title = "Skończyć Repository Pattern",
                        Description = "Zaimplementować generyczne repozytorium i UnitOfWork.",
                        DueDate = DateTime.UtcNow.AddDays(2),
                        PriorityId = highPriority?.Id
                    },
                    new Task
                    {
                        UserId = testUser.Id,
                        Title = "Wdrożyć AuthController",
                        Description = "Stworzyć endpointy Rejestracji i Logowania.",
                        DueDate = DateTime.UtcNow.AddDays(3),
                        PriorityId = mediumPriority?.Id
                    },
                    new Task
                    {
                        UserId = testUser.Id,
                        Title = "Przeczytać dokumentację React Query",
                        IsCompleted = true,
                        CompletedAt = DateTime.UtcNow.AddDays(-1),
                        PriorityId = mediumPriority?.Id
                    }
                };
                await _context.Tasks.AddRangeAsync(testTasks);

                // 4. Dodać przykładowe nawyki
                var testHabits = new List<Habit>
                {
                    new Habit { UserId = testUser.Id, Name = "Pisałeś 1000 linii kodu", Unit = "linie", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Habit { UserId = testUser.Id, Name = "Poczytane 30 minut", Unit = "minut", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                };
                await _context.Habits.AddRangeAsync(testHabits);
                await _context.SaveChangesAsync();

                // Dodanie wykonania nawyku (HabitCompletion)
                if (testHabits.Any())
                {
                    var firstHabit = testHabits.First();
                    await _context.HabitCompletions.AddAsync(new HabitCompletion
                    {
                        HabitId = firstHabit.Id,
                        CompletedDate = DateTime.UtcNow.AddDays(-1),
                        Value = 30,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    });
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}