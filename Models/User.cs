using System.ComponentModel.DataAnnotations;

namespace Task4AppMvc.Models
{
    public class User
    {
        public Int32 Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime LastLoginTime { get; set; }
        public DateTime RegistrationTime { get; set; }
        public bool IsActive { get; set; }
    }
}
