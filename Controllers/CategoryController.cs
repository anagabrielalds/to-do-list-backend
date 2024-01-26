using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoList.Interface;
using ToDoList.Services;
using ToDoList.Utils;
using ToDoList.ViewModel;

[Authorize]
[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase, IRequest<CategoryRequest, CategoryRequest>
{

	private readonly CategoryService _services;

	public CategoryController(CategoryService service)
	{
		_services = service;
	}

	[HttpGet]
	public IActionResult Get()
	{
		var response =  _services.Get();

		return new ObjectResult(response) {  StatusCode = response.Status};
	}

	[HttpGet("{id}")]
	public IActionResult Get(int id)
	{
		var response = _services.GetById(id);

		return new ObjectResult(response) { StatusCode = response.Status };
	}

	[HttpPost]
	public IActionResult Post([FromBody] CategoryRequest category)
	{
		var response = _services.Post(category);

		return new ObjectResult(response) { StatusCode = response.Status };
	}

	[HttpPut("{id}")]
	public IActionResult Update(int id, [FromBody] CategoryRequest category)
	{
		var response = _services.Update(id, category);

		return new ObjectResult(response) { StatusCode = response.Status };
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var response = _services.Delete(id);

		return new ObjectResult(response) { StatusCode = response.Status };
	}
}
