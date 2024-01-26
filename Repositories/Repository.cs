using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoList.Data;
using ToDoList.Data.Models;
using ToDoList.Interface;
using ToDoList.ViewModel;

namespace ToDoList.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
	{
		public AppDbContext _context { get; set; }
		public Repository(AppDbContext context)
		{
			_context = context;
		}

		public List<T> Get(Expression<Func<T, bool>> predicate)
		{
			return  _context.Set<T>().Where(predicate).ToList();
		}

		public T GetByField(Expression<Func<T, bool>> predicate)
		{
			#pragma warning disable CS8603 // Possible null reference return.
			return _context.Set<T>().FirstOrDefault(predicate);
		}

		public T Post(T entity)
		{
			_context.Set<T>().Add(entity);
			_context.SaveChanges();
			return entity;
		}

		public T Update(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
			_context.Set<T>().Update(entity);
			_context.SaveChanges();

			return entity;
		}

		public void Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
			_context.SaveChanges();
		}

		public User Login(string user, string password)
		{
			return _context.Users.First(c => c.Username == user && c.Password == password);
		}
	}
}
