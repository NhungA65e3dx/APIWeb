using System.ComponentModel.DataAnnotations;

namespace APIWeb.Model
{
    public class Update
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]

        public String Password { get; set; }
        public DateTime DueDate { get; set; }
    }
}
