using SecondBrain.Application.DTOs;
using SecondBrain.Application.Interfaces;
using SecondBrain.Domain.Entities; // Potrzebne dla encji Task

namespace SecondBrain.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync(int userId)
        {
            // 1. Pobierz zadania z repozytorium (używając warunku lambda)
            // Uwaga: Nasze generyczne repo nie ma jeszcze Include (do dociągania Priorytetów),
            // więc na razie PriorityName może być puste. Naprawimy to później dodając Specification Pattern lub Include.
            var tasks = await _unitOfWork.Tasks.GetAsync(t => t.UserId == userId);

            // 2. Mapowanie encji na DTO (ręczne na razie, później AutoMapper)
            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                IsCompleted = t.IsCompleted,
                // Proste mapowanie, w przyszłości rozbudujemy o Include()
                PriorityName = t.PriorityId.ToString() ?? "None",
                CategoryName = t.CategoryId.ToString() ?? "None"
            });
        }

        public async Task<TaskDto?> GetTaskByIdAsync(int id, int userId)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);

            // Sprawdzenie czy zadanie istnieje i należy do użytkownika
            if (task == null || task.UserId != userId)
                return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted
            };
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto dto)
        {
            var newTask = new Domain.Entities.Task
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                PriorityId = dto.PriorityId,
                CategoryId = dto.CategoryId,
                UserId = dto.UserId,
                IsCompleted = false,
                IsDeleted = false
            };

            await _unitOfWork.Tasks.AddAsync(newTask);
            await _unitOfWork.CompleteAsync(); // Zapis do bazy (commit)

            // Zwracamy utworzone DTO
            return new TaskDto
            {
                Id = newTask.Id,
                Title = newTask.Title,
                Description = newTask.Description,
                DueDate = newTask.DueDate,
                IsCompleted = newTask.IsCompleted
            };
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (task == null) return false;

            _unitOfWork.Tasks.Delete(task);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> ToggleTaskCompletionAsync(int id)
        {
            var task = await _unitOfWork.Tasks.GetByIdAsync(id);
            if (task == null) return false;

            task.IsCompleted = !task.IsCompleted;
            if (task.IsCompleted)
            {
                task.CompletedAt = DateTime.UtcNow;
            }
            else
            {
                task.CompletedAt = null;
            }

            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}