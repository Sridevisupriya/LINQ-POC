using System.ComponentModel.DataAnnotations;

namespace LearnStudentAPI.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "User name required")]
        public string?  UserName { get; set; }

        [Required(ErrorMessage ="Email Id is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage ="Password required")]
        public string? Password { get; set; }
    }
}
