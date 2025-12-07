using System;

namespace SecondBrain.Application.DTOs
{
    // To zwracamy do użytkownika (odczyt)
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public string PriorityName { get; set; } = string.Empty; // Zamiast PriorityId, zwrócimy nazwę
        public string CategoryName { get; set; } = string.Empty;
    }

    // Tego używamy do tworzenia zadania (zapis)
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int? PriorityId { get; set; }
        public int? CategoryId { get; set; }
        public int UserId { get; set; } // Na razie podajemy ręcznie, później weźmiemy z tokena
    }
}