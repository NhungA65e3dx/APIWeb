using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIWeb.Entities
{
    [Table("User")]
    public class User 

    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreateAt { get; set; }= DateTime.Now;
        public DateTime UpdateAt { get; internal set; }
    }
}
