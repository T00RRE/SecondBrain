using SecondBrain.Domain.Entities;

namespace SecondBrain.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Tutaj udostępniamy repozytoria dla konkretnych encji
        IRepository<User> Users { get; }
        IRepository<Domain.Entities.Task> Tasks { get; }
        IRepository<Habit> Habits { get; }

        // Zapisuje zmiany w bazie (commit transakcji)
        Task<int> CompleteAsync();
    }
}