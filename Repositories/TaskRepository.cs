using ToDoList.Data;
using ToDoList.Interface;
using Task = ToDoList.Data.Models.Task;

namespace ToDoList.Repositories
{
	public class TaskRepository :  Repository<Task>, IRepository<Task>
	{
		public TaskRepository(AppDbContext context) : base(context){ }
	}
}
