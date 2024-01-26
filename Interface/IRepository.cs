using System.Linq.Expressions;

namespace ToDoList.Interface
{
	public interface IRepository<T>
	{
		public List<T> Get(Expression<Func<T, bool>> predicate);

		public T GetByField(Expression<Func<T, bool>> predicate);

		public T Post(T entity);

		public T Update(T entity);

		public void Delete(T entity);

	}
}
