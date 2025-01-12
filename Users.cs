using System.ComponentModel.DataAnnotations;

namespace BagAPI
{
    public class Users
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [Range(1, 120)]
        public int age { get; set; }

        [Phone]
        public int mobile { get; set; }

        [Required]
        public string password { get; set; }
    }
}
