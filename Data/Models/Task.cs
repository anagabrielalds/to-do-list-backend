using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Data.Models
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int IdCategory { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int IdUser { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime DateOfCreation { get; set; }

        public bool Checked { get; set; } 

        public virtual Category Category { get; set; }

        public virtual User User { get; set; }
    }
}
