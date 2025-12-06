using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class TaskComment : BaseEntity
    {
        public int TaskId { get; set; }
        public string Comment { get; set; } = string.Empty;

        // Komentarze są tworzone przez użytkownika (domyślnie, używamy CreatedBy)
        // Jeśli wymagany jest ChangedBy, można dodać ChangedById
        // int CreatedById { get; set; } 

        public Task Task { get; set; } = null!;
    }
}