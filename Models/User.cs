using System.Text.Json.Serialization;

namespace BagAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }

        public ICollection<Review>? Reviews { get; set; } = new List<Review>();
        [JsonIgnore]
        public Role? Role { get; set; }
    }
}