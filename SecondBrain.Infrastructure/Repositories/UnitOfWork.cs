using SecondBrain.Application.Interfaces;
using SecondBrain.Domain.Entities;
using SecondBrain.Infrastructure.Data;

namespace SecondBrain.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        // Prywatne pola przechowujące instancje repozytoriów
        private IRepository<User>? _users;
        private IRepository<Domain.Entities.Task>? _tasks;
        private IRepository<Habit>? _habits;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lazy Loading - tworzymy repozytorium dopiero, gdy ktoś o nie zapyta
        public IRepository<User> Users => _users ??= new Repository<User>(_context);
        public IRepository<Domain.Entities.Task> Tasks => _tasks ??= new Repository<Domain.Entities.Task>(_context);
        public IRepository<Habit> Habits => _habits ??= new Repository<Habit>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}