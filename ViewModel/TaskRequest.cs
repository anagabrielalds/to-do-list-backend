using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Task = ToDoList.Data.Models.Task;

namespace ToDoList.ViewModel
{
    public class TaskRequest
	{
		public int IdCategory { get; set; }

		public string Description { get; set; }

		public bool Checked { get; set; } = false;

		public Task ToModel(int? id, int idUser)
		{
			var model =  new Task
			{
				IdCategory = IdCategory,
				Description = Description,
				Checked = Checked,
				DateOfCreation = DateTime.Now,
				IdUser = idUser
				
			};

			if (id != null) model.Id = id.Value;

			return model;

		}
	}
}
