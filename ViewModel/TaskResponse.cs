using System.Threading.Tasks;
using ToDoList.Data.Models;
using ToDoList.Repositories;
using ToDoList.Utils;
using Task = ToDoList.Data.Models.Task;

namespace ToDoList.ViewModel
{
	public class TaskResponse
	{
		public int Id { get; set; }
		public string Description { get; set; }

		public DateTime DateOfCreation { get; set; }

		public bool Checked { get; set; }

		public CategoryResponse Category { get; set; }

		public TaskResponse ToResponseModel(Task model, CategoryRepository categoryRepository)
		{
			return new TaskResponse
			{
				Id = model.Id,
				Description = model.Description,
				Checked = model.Checked,
				DateOfCreation = model.DateOfCreation,
				Category = GetCategory(model, categoryRepository),

			};

		}

		private CategoryResponse GetCategory(Task model, CategoryRepository categoryRepository)
		{
			var categoryResponse = new CategoryResponse();


			if (model.Category == null)
			{
				var category = categoryRepository.GetByField(c => c.Id == model.IdCategory);
				categoryResponse = categoryResponse.ToModelResponse(category);

			}
			else
			{
				categoryResponse = categoryResponse.ToModelResponse(model.Category);
			}
			return categoryResponse;
		}

		public List<TaskResponse> ToListTasksResponse(List<Task> tasks, CategoryRepository categoryRepository)
		{
			if (tasks.Count == 0) return new List<TaskResponse>();

			List<TaskResponse> tasksResponse = tasks
				.Select(task => new TaskResponse
				{
					Id = task.Id,
					Description = task.Description,
					Checked = task.Checked,
					DateOfCreation = task.DateOfCreation,
					Category = GetCategory(task, categoryRepository),
				})
				.ToList();

			return tasksResponse;

		}
	}
}
