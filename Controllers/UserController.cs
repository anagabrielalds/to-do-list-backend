using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Services;
using ToDoList.ViewModel;
using ToDoList.Interface;
using ToDoList.Utils;

namespace ToDoList.Controllers
{
	[Authorize]
	[Route("api/user")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserService _services;

		public UserController(UserService service)
		{
			_services = service;
		}

		[HttpGet("admin")]
		[Authorize(Roles = "ADMIN")]
		public IActionResult GetListUser()
		{
			var response = _services.Get();

			return new ObjectResult(response) { StatusCode = response.Status };
		}

		[HttpGet("admin/{id}")]
		[Authorize(Roles = "ADMIN")]
		public IActionResult Get(int id)
		{
			var response = _services.GetUser(id);

			return new ObjectResult(response) { StatusCode = response.Status };
		}

		[HttpGet]
		public IActionResult Get()
		{
			var response = _services.GetUser(null);

			return new ObjectResult(response) { StatusCode = response.Status };
		}

		[HttpDelete]
		public IActionResult Delete()
		{
			var response = _services.Delete();

			return new ObjectResult(response) { StatusCode = response.Status };
		}

		[HttpPut]
		public IActionResult Update(UserUpdateRequest data)
		{
			var response = _services.Update(data);

			return new ObjectResult(response) { StatusCode = response.Status };
		}

		[HttpPost]
		[Route("register")]
		[AllowAnonymous]
		public IActionResult Post([FromBody] UserRequest model)
		{
			var user = _services.Post(model);

			return new ObjectResult(user) { StatusCode = user.Status };
		}

		[HttpPost]
		[Route("resetPassword")]
		public IActionResult ResetPassword([FromBody] UserResetPasswordRequest model)
		{
			ApiResponse<UserResponse> userResponse = _services.ResetPassword(model.PasswordOld, model.PasswordNew);

			return new ObjectResult(userResponse) { StatusCode = userResponse?.Status };
		}

	}
}