using SecondBrain.Application.DTOs;

namespace SecondBrain.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync(int userId);
        Task<TaskDto?> GetTaskByIdAsync(int id, int userId);
        Task<TaskDto> CreateTaskAsync(CreateTaskDto dto);
        Task<bool> DeleteTaskAsync(int id);
        Task<bool> ToggleTaskCompletionAsync(int id);
    }
}