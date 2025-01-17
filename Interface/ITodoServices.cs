using TodoAPI.Contracts;
using TodoAPI.Models;

namespace TodoAPI.Interface
{
     public interface ITodoServices
     {
        //Retrieves all Todo items from DB
         Task<IEnumerable<Todo>> GetAllAsync(); //IEnumerable allows us to iterate over a collection of Todo objects
         //Retrieves a single Todo item by its id
         Task<Todo> GetByIdAsync(Guid id);
         //Creates a new Todo item
         Task CreateTodoAsync(CreateTodoRequest request);
         //Updates an existing Todo item
         Task UpdateTodoAsync(Guid id, UpdateTodoRequest request);
         //Deletes an existing Todo item from DB
         Task DeleteTodoAsync(Guid id);
     }
}