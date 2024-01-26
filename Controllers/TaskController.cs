using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoList.Interface;
using ToDoList.Services;
using ToDoList.Utils;
using ToDoList.ViewModel;

[Authorize]
[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase, IRequest<TaskRequest, TaskRequest>
{

	private readonly TaskService _services;

	public TaskController(TaskService service)
	{
		_services = service;
	}

	[HttpGet]
	public IActionResult Get()
	{
		var response = _services.Get();

		return new ObjectResult(response) { StatusCode = response.Status };
	}

	[HttpGet("{id}")]
	public IActionResult Get(int id)
	{
		var response = _services.GetById(id);

		return new ObjectResult(response) { StatusCode = response.Status };
	}

	[HttpPost]
	public IActionResult Post([FromBody] TaskRequest request)
	{
		var response = _services.Post(request);

		return new ObjectResult(response) { StatusCode = response.Status };
	}

	[HttpPut("{id}")]
	public IActionResult Update(int id, [FromBody] TaskRequest request)
	{
		var response = _services.Update(id, request);

		return new ObjectResult(response) { StatusCode = response.Status };
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var response = _services.Delete(id);

		return new ObjectResult(response) { StatusCode = response.Status };
	}
}
