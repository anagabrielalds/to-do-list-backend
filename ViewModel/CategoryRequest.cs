using ToDoList.Data.Models;

namespace ToDoList.ViewModel
{
    public class CategoryRequest
	{
		public string Description { get; set; }

		public Category ToModel(int? id, int idUser)
		{
			var model = new Category {IdUser = idUser, Description = Description };

			if (id != null) model.Id = id.Value;

			return model;
		}

		public Category ToModelUpdate(int? id)
		{
			var model = new Category { Description = Description };

			if (id != null) model.Id = id.Value;

			return model;
		}
	}

}
