using ToDoList.Data.Models;

namespace ToDoList.ViewModel
{
	public class CategoryResponse
	{
		public string Description { get; set; }

		public int Id { get; set; }

		public CategoryResponse ToModelResponse(Category category)
		{
			return new CategoryResponse { Id = category.Id, Description = category.Description };
		}

		public List<CategoryResponse> ToListCategoryResponse(List<Category> categories)
		{
			if (categories.Count == 0) return new List<CategoryResponse>();

			List<CategoryResponse> categoryResponses = categories
				.Select(category => new CategoryResponse
				{
					Id = category.Id,
					Description = category.Description
				})
				.ToList();

			return categoryResponses;

		}

	}
}
