using Microsoft.Extensions.Configuration.UserSecrets;
using ToDoList.Data.Models;
using ToDoList.Interface;
using ToDoList.Repositories;
using ToDoList.Utils;
using ToDoList.ViewModel;

namespace ToDoList.Services
{
	public class CategoryService : BaseService, IValidation
	{
		public readonly CategoryRepository _repository;

		public CategoryService(IHttpContextAccessor httpContextAccessor, CategoryRepository repository) : base(httpContextAccessor)
		{
			_repository = repository;
		}

		public ApiResponse<List<CategoryResponse>> Get()
		{
			List<Category> lista = _repository.Get(c => c.IdUser == IdUserLogado);

			var convertToModelResponse = new CategoryResponse().ToListCategoryResponse(lista);

			if (lista != null && lista.Count > 0)
				return new ApiResponse<List<CategoryResponse>>(200, convertToModelResponse, "Sucesso");
			else
				return new ApiResponse<List<CategoryResponse>>(404, null, "Não há categorias cadastradas");
		}

		public ApiResponse<CategoryResponse> GetById(int id)
		{
			if (!IsValidId(id)) return new ApiResponse<CategoryResponse>(404, null, "o Id informado não é válido");

			if (!IsResourceOwner(id)) return new ApiResponse<CategoryResponse>(401, null, "Você não tem permissão para essa ação");
	
			Category item = _repository.GetByField(e => e.Id == id);

			var convertToModelResponse = new CategoryResponse().ToModelResponse(item);

			return new ApiResponse<CategoryResponse>(200, convertToModelResponse, "Sucesso");
		}

		public ApiResponse<CategoryResponse> Post(CategoryRequest category)
		{
			if(string.IsNullOrEmpty(category.Description)) return new ApiResponse<CategoryResponse>(401, null, "O campo de descrição é obrigatório");

			try
			{
				var entity = category.ToModel(null, IdUserLogado);
				var result = _repository.Post(entity);

				var convertToModelResponse = new CategoryResponse().ToModelResponse(result);

				return new ApiResponse<CategoryResponse>(200, convertToModelResponse, "Categoria adicionada com sucesso");
			}
			catch
			{
				return new ApiResponse<CategoryResponse>(500, null, "Erro ao adicionar categoria");
			}
		}

		public ApiResponse<CategoryResponse> Update(int id, CategoryRequest request)
		{
			if (!IsValidId(id)) return new ApiResponse<CategoryResponse>(404, null, "o Id informado não é válido");

			if (!IsResourceOwner(id)) return new ApiResponse<CategoryResponse>(401, null, "Você não tem permissão para essa ação");

			try
			{
				var update = _repository.GetByField(c => c.Id == id);

				update.Description = request.Description;

				var category = _repository.Update(update);

				var convertToModelResponse = new CategoryResponse().ToModelResponse(category);

				return new ApiResponse<CategoryResponse>(200, convertToModelResponse, "Categoria atualizada com sucesso");
			}
			catch
			{
				return new ApiResponse<CategoryResponse>(500, null, "Erro ao atualizar categoria");

			}
		}
		
		public ApiResponse<CategoryResponse> Delete(int id)
		{
			if (!IsValidId(id)) return new ApiResponse<CategoryResponse>(404, null, "o Id informado não é válido");

			if (!IsResourceOwner(id)) return new ApiResponse<CategoryResponse>(401, null, "Você não tem permissão para essa ação");

			try
			{
				var entity = _repository.GetByField(e => e.Id == id);

				_repository.Delete(entity);

				return new ApiResponse<CategoryResponse>(200, null, "Categoria excluída com sucesso");
			}
			catch (Exception ex)
			{
				return new ApiResponse<CategoryResponse>(500, null, "Erro ao excluir Tarefas...");
			}
		}

		public bool IsResourceOwner(int resourceId)
		{
			var retorno = _repository.GetByField(i => i.Id == resourceId && i.IdUser == IdUserLogado);

			return retorno != null;
		}

		public bool IsValidId(int id)
		{
			var retorno = _repository.GetByField(i => i.Id == id);

			return retorno != null;
		}
	}
}