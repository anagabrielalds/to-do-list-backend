using ToDoList.Data;
using ToDoList.Data.Models;
using ToDoList.Interface;

namespace ToDoList.Repositories
{
	public class LoginRepository : Repository<User>
	{
		public LoginRepository(AppDbContext context) : base(context)
		{

		}

		public User? Login(string user, string password)
		{
			return _context.Users.FirstOrDefault(c => c.Username == user && c.Password == password);
		}

		public void DeletePasswordRecovery(PasswordRecovery password)
		{
			_context.PasswordRecoveries.Remove(password);
			_context.SaveChanges();
		}

		public PasswordRecovery? LoginWithMailAndPasswordRecovery(string mail, string password)
		{
			return _context.PasswordRecoveries.FirstOrDefault(c => c.Mail == mail && c.NewPassword == password);
			
		}


		public void PasswordRevovery(string mail, string codigo, int expirationTimeMinutes)
		{
			var now = DateTime.Now;
			var recovery = new PasswordRecovery { Mail = mail, ExpirationDate = now.AddMinutes(expirationTimeMinutes), NewPassword = codigo };

			_context.PasswordRecoveries.Add(recovery);

			_context.SaveChanges();
		}

	}

}
