using ToDoList.Data.Models;

namespace ToDoList.ViewModel
{
	public class UserUpdateRequest
	{
		public string Username { get; set; }

		public string? Mail { get; set; }

		public string? ImagePath { get; set; }

		public User ToModel(int? id)
		{
			var model = new User { Username = Username, Mail = Mail, ImagePath = ImagePath };

			if (id != null) model.Id = id.Value;

			return model;
		}
	}
}
