using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Data.Models
{
	public class PasswordRecovery
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public string NewPassword { get; set; }

		[Required]
		public DateTime ExpirationDate { get; set; }

		public string Mail { get; set; }

	}
}
