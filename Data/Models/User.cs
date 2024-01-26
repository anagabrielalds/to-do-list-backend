using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoList.ViewModel;

namespace ToDoList.Data.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

		[Required]
		public string Mail { get; set; }

        public string? ImagePath { get; set; }

		[Required]
		public string Password { get; set; }

        public string? Role { get; set; }

		public virtual ICollection<Task>? Tasks { get; set; }

        public virtual ICollection<Category>? Categories { get; set; }

        public UserResponse ToModelResponse()
        {
            return new UserResponse 
            { 
                Id = Id, 
                Username = Username, 
                ImagePath = ImagePath,
				Mail = Mail, 
                Role = Role 
            };
        }

    }
}