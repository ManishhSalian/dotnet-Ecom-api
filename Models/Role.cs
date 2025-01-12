using System.Text.Json.Serialization;

namespace BagAPI.Models
{
    public class Role
    {
        public int Id { get; set; } 
        public string? Name { get; set; }

      
        [JsonIgnore]
        public ICollection<User>? Users { get; set; }
    }
}