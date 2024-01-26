namespace ToDoList.Utils
{
	public class ApiResponse<T>
	{
		public int Status { get; set; }
		public string Message { get; set; }
		public T? Data { get; set; }

		public ApiResponse(int status, T? data, string message = null)
		{
			Status = status;
			Data = data;
			Message = message;
		}
	}

}
