namespace ToDoList.Interface
{
	public interface IValidation
	{
		bool IsResourceOwner(int resourceId);

		bool IsValidId(int id);
	}
}
