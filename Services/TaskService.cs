using System.Threading.Tasks;
using ToDoList.Data.Models;
using ToDoList.Interface;
using ToDoList.Repositories;
using ToDoList.Utils;
using ToDoList.ViewModel;
using Task = ToDoList.Data.Models.Task;

namespace ToDoList.Services
{
	public class TaskService : BaseService, IValidation
	{
		public readonly TaskRepository _taskRepository;
		public readonly CategoryRepository _categoryRepository;

		public TaskService(IHttpContextAccessor httpContextAccessor,TaskRepository repository, CategoryRepository categoryRepository) : base(httpContextAccessor)
		{
			_taskRepository = repository;
			_categoryRepository = categoryRepository;
		}

		public ApiResponse<List<TaskResponse>> Get()
		{
			List<Task> lista = _taskRepository.Get(t => t.IdUser == IdUserLogado).ToList();
			
			var taskResponse = new TaskResponse().ToListTasksResponse(lista, _categoryRepository);
			if (taskResponse != null)
				return new ApiResponse<List<TaskResponse>>(200, taskResponse, "Sucesso");
			else
				return new ApiResponse<List<TaskResponse>>(404, null, "Não há tareafas cadastradas");
		}

		public ApiResponse<TaskResponse> GetById(int id)
		{
			if (!IsValidId(id)) return new ApiResponse<TaskResponse>(404, null, "o Id informado não é válido");

			if (!IsResourceOwner(id)) return new ApiResponse<TaskResponse>(401, null, "Você não tem permissão para essa ação");

			Task task = _taskRepository.GetByField(e => e.Id == id) ;

			var taskResponse = new TaskResponse().ToResponseModel(task, _categoryRepository);

			return new ApiResponse<TaskResponse>(200, taskResponse, "Sucesso");
		}

		public ApiResponse<TaskResponse> Post(TaskRequest task)
		{

			if(task.IdCategory == 0) return new ApiResponse<TaskResponse>(404, null, "o Id da categoria não foi informado");

			if (!IsValidIdCategory(task.IdCategory)) return new ApiResponse<TaskResponse>(404, null, "o Id da categoria informado não é válido");

			if (string.IsNullOrWhiteSpace(task.Description)) return new ApiResponse<TaskResponse>(404, null, "o Id informado não é válido");

			try
			{
				var newTask = task.ToModel(null, IdUserLogado);
				var result = _taskRepository.Post(newTask);

				var taskResponse = new TaskResponse().ToResponseModel(result, _categoryRepository);

				return new ApiResponse<TaskResponse>(200, taskResponse, "Tarefas adicionada com sucesso");
			}
			catch
			{
				return new ApiResponse<TaskResponse>(500, null, "Erro ao adicionar tarefa");
			}
		}

		public ApiResponse<TaskResponse> Update(int id, TaskRequest request)
		{
			if (!IsValidId(id)) return new ApiResponse<TaskResponse>(404, null, "o Id informado não é válido");

			if (!IsValidIdCategory(request.IdCategory)) return new ApiResponse<TaskResponse>(404, null, "o Id da categoria informado não é válido");

			if (!IsResourceOwner(id)) return new ApiResponse<TaskResponse>(401, null, "Você não tem permissão para essa ação");

			try
			{
				var update = _taskRepository.GetByField(i => i.Id == id);

				if(!string.IsNullOrWhiteSpace(request.Description)) 
					update.Description = request.Description;

				update.IdCategory = request.IdCategory;
				update.Checked = request.Checked;

				var task = _taskRepository.Update(update);

				var taskResponse = new TaskResponse().ToResponseModel(task, _categoryRepository);

				return new ApiResponse<TaskResponse>(200, taskResponse, "Tarefa atualizada com sucesso");

			}
			catch
			{
				return new ApiResponse<TaskResponse>(500, null, "Erro ao atualizar tarefa");
			}
		}

		public ApiResponse<TaskResponse> Delete(int id)
		{
			if (!IsValidId(id)) return new ApiResponse<TaskResponse>(404, null, "o Id informado não é válido");

			if (!IsResourceOwner(id)) return new ApiResponse<TaskResponse>(401, null, "Você não tem permissão para essa ação");

			try
			{
				var taskEntity = _taskRepository.GetByField(e => e.Id == id);

				_taskRepository.Delete(taskEntity);

				return new ApiResponse<TaskResponse>(200, null, "Tarefa excluída com sucesso");
			}
			catch (Exception ex)
			{
				return new ApiResponse<TaskResponse>(500, null, "Erro ao excluir Tarefas...");
			}
		}

		public bool IsResourceOwner(int resourceId)
		{
			var retorno = _taskRepository.GetByField(i => i.Id == resourceId && i.IdUser == IdUserLogado);

			return retorno != null;
		}

		public bool IsValidId(int id)
		{
			var retorno = _taskRepository.GetByField(i => i.Id == id);

			return retorno != null;
		}

		public bool IsValidIdCategory(int idCategory)
		{
			var retorno = _taskRepository._context.Categories.FirstOrDefault(i => i.Id == idCategory);

			return retorno != null;
		}
	} 
}