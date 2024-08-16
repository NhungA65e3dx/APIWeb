using System.ComponentModel.DataAnnotations;

namespace APIWeb.Model
{
    public class Login
    {
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]

        public String Password { get; set; }

    }
}
