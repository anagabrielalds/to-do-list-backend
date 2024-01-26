using ToDoList.Data;
using ToDoList.Data.Models;
using ToDoList.Interface;

namespace ToDoList.Repositories
{
	public class CategoryRepository : Repository<Category>, IRepository<Category>
	{
		public CategoryRepository(AppDbContext context) : base(context){ }

	}
}
