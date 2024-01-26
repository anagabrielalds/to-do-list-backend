using System.Security.Claims;
using ToDoList.Data;
using ToDoList.Interface;
using ToDoList.Data.Models;
using ToDoList.Repositories;

namespace ToDoList.Services
{
	public abstract class BaseService
	{
		protected string? UserLogado { get; }
		protected int IdUserLogado { get; }
		protected string? Role { get; }

		protected IHttpContextAccessor? HttpContextAccessor { get; }

		protected BaseService( IHttpContextAccessor httpContextAccessor) 
		{
			HttpContextAccessor = httpContextAccessor;

			UserLogado = HttpContextAccessor?.HttpContext?.User?.Identity?.Name;
			var userId = HttpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			IdUserLogado = (userId != null ) ? int.Parse(userId) : 0;
			Role = HttpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
		}
	}
}
