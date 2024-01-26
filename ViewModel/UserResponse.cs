namespace ToDoList.ViewModel
{
	public class UserResponse
	{
		public int Id { get; set; }
		public string Token { get; set; }
		public string Username { get; set; }

		public string? Mail { get; set; }

		public string ImagePath { get; set; }
		public string Role { get; set; }
	}
}
