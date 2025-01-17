using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoAPI.AppDataContext;
using TodoAPI.Contracts;
using TodoAPI.Interface;
using TodoAPI.Models;

namespace TodoAPI.Services
{
    public class TodoServices : ITodoServices
    {
        //An instance of TodoDbContext class, enabling us to interact with the database.
        private readonly TodoDbContext _context;
        //An instance of ILogger class, enabling us to log information to the console.
        private readonly ILogger<TodoServices> _logger;
        //An instance of IMapper class, enabling us to map objects using AutoMapper.
        private readonly IMapper _mapper;

        public TodoServices(TodoDbContext context, ILogger<TodoServices> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task CreateTodoAsync(CreateTodoRequest request)
        {
            try
            {
                // Use AutoMapper to convert the CreateTodoRequest object to a Todo object.
                var todo = _mapper.Map<Todo>(request);
                // Set the CreatedAt property to the current date and time.
                todo.CreatedAt = DateTime.UtcNow;
                // Add the Todo object to the Todos DbSet, saving changes asynchronously.
                _context.Todos.Add(todo);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the Todo item.");
                throw new Exception("An error occurred while creating the Todo item.");
            }
        }

        public async Task DeleteTodoAsync(Guid id)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                if (todo == null)
                {
                    throw new Exception($"Todo item with id : {id} not found");
                }
                else
                {
                    _context.Todos.Remove(todo);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the Todo item.");
                throw new Exception("An error occurred while deleting the Todo item.");
            }
        }

        // Get all TODO Items from the database 
        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            // fetches all Todo items from the database and returns them as a list.
            var todo = await _context.Todos.ToListAsync();
            if (todo == null)
            {
                throw new Exception(" No Todo items found");
            }
            return todo;
        }

        public async Task<Todo> GetByIdAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                _logger.LogWarning($"No Todo item found with ID: {id}");
                throw new Exception("No Todo item found");
            }
            return todo;
        }

        public async Task UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            try
            {
                var todo = await _context.Todos.FindAsync(id);
                //locate the todo item with the specified id.
                if (todo == null)
                {
                    throw new Exception($"Todo item with id : {id} not found");
                }
                // the update field is not null, update the todo item with the new value.
                if (request.Title != null)
                {
                    todo.Title = request.Title;
                }
                if (request.Description != null)
                {
                    todo.Description = request.Description;
                }
                if (request.IsComplete != null)
                {
                    todo.IsComplete = request.IsComplete.Value;
                }
                if (request.DueDate != null)
                {
                    todo.DueDate = request.DueDate.Value;
                }
                if (request.Priority != null)
                {
                    todo.Priority = request.Priority.Value;
                }
                // Set the UpdatedAt property to the current date and time.
                todo.UpdatedAt = DateTime.UtcNow;
                // Save changes asynchronously.
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the Todo item.");
                throw new Exception("An error occurred while updating the Todo item.");
            }
        }
    }
}