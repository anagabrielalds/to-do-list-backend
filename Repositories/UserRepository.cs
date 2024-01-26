using ToDoList.Data;
using ToDoList.Data.Models;
using ToDoList.Interface;

namespace ToDoList.Repositories
{
	public class UserRepository : Repository<User>, IRepository<User>
	{
		public UserRepository(AppDbContext context) : base(context){ }

		public User? ResetPasswordRevoveryToUser(string mail, string password)
		{
			var user = GetByField(u => u.Mail == mail);
			user.Password = password;
			_context.SaveChanges();

			return user;
		}

		public bool IsValidMail(string mail)
		{
			var model = GetByField(u => u.Mail == mail);

			return model != null;
		}
	}
}