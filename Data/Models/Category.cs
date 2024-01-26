using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Data.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [ForeignKey(nameof(User))]
        public int IdUser { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
