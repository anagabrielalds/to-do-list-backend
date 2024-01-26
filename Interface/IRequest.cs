using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using ToDoList.Data.Models;
using ToDoList.Utils;

namespace ToDoList.Interface
{
    public interface IRequest<T,R>
    {
        public IActionResult Get();
        public IActionResult Get(int id);
        public IActionResult Delete(int id);
        public IActionResult Update(int id, R data);
        public IActionResult Post(T data);
    }
}
