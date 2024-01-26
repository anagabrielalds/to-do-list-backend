using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Services;
using ToDoList.ViewModel;
using Microsoft.AspNetCore.Http;
using ToDoList.Data.Models;
using ToDoList.Utils;

namespace ToDoList.Controllers
{
	[Route("api/login/")]
	[ApiController]
	public class LoginController : ControllerBase
	{
		private readonly LoginService _services;
		private readonly TokenService _tokenServices;

		public LoginController(LoginService service, TokenService tokenServices)
		{
			_services = service;
			_tokenServices = tokenServices;
		}

		[HttpPost]
		[Route("authenticate")]
		[AllowAnonymous]
		public IActionResult Authenticate([FromBody] LoginRequest model)
		{
			ApiResponse<UserResponse> userResponse = _services.Login(model.Username, model.Password);

			if (userResponse != null && userResponse.Data != null)
			{
				var token = _tokenServices.GenerateToken(userResponse.Data);
				userResponse.Data.Token = token;
			}

			return new ObjectResult(userResponse) { StatusCode = userResponse?.Status };
		}

		[HttpPost]
		[Route("authenticateWithMailAndPasswordRecovery")]
		[AllowAnonymous]
		public IActionResult AuthenticateWithMailAndPasswordRecovery([FromBody] LoginRecoveryRequest model)
		{
			ApiResponse<UserResponse> userResponse = _services.LoginWithMailAndPasswordRecovery(model.Mail, model.Password);

			if (userResponse != null && userResponse.Data != null)
			{
				var token = _tokenServices.GenerateToken(userResponse.Data);
				userResponse.Data.Token = token;
			}

			return new ObjectResult(userResponse) { StatusCode = userResponse?.Status };
		}

		[HttpGet]
		[Route("authenticated")]
		[Authorize]
		public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

		[HttpPost("passwordRecovery/{mail}")]
		[AllowAnonymous]
		public IActionResult Recovery(string mail)
		{
			var result = _services.GeneratePasswordRecovery(mail);
			return new ObjectResult(result) { StatusCode = result.Status };
		}
	}
}