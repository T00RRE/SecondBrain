using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class UserProfile : BaseEntity
    {
        public int UserId { get; set; }

        public string? Avatar { get; set; } 
        public string? Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public string TimeZone { get; set; } = "Europe/Warsaw";
        public string PreferredLanguage { get; set; } = "pl";

        public User User { get; set; } = null!;
    }
}