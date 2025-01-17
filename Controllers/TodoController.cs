using Microsoft.AspNetCore.Mvc;
using TodoAPI.Contracts;
using TodoAPI.Interface;

namespace TodoAPI.Controllers
{
    [ApiController] //this attribute indicates class is an API controller that handle HTTP requests, part of ASP.NET Core framework
    [Route("api/[controller]")] //this attribute specifies the route template for the controller, defining how the controller responds to incoming HTTP requests
    //How the above api/controller works is that [controller] is a placeholder that gets replaced by the name of the controller class minus the "controller" suffix
    //In this case, the route template is "api/todo" because the controller class is named TodoController
    public class TodoController : ControllerBase
    {
        private readonly ITodoServices _todoServices;

        public TodoController(ITodoServices todoServices)
        {
            _todoServices = todoServices;
        }

        [HttpPost] //signifies creating a new resource
        public async Task<IActionResult> CreateTodoAsync(CreateTodoRequest request)
        {
            //This checks if the model state is valid. Any attributes of CreateTodoRequest missing/incorrect will be flagged as errors
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //Create a new Todo item in DB using the ITodoServices interface
                await _todoServices.CreateTodoAsync(request);
                return Ok(new { message = "Blog post successfully created" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the  crating Todo Item", error = ex.Message });

            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var todo = await _todoServices.GetAllAsync();
                if (todo == null || !todo.Any())
                {
                    return Ok(new { message = "No Todo Items  found" });
                }
                return Ok(new { message = "Successfully retrieved all blog posts", data = todo });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving all Todo it posts", error = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var todo = await _todoServices.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"No Todo item with Id {id} found." });
                }
                return Ok(new { message = $"Successfully retrieved Todo item with Id {id}.", data = todo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while retrieving the Todo item with Id {id}.", error = ex.Message });
            }
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var todo = await _todoServices.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"No Todo item with Id {id} found." });
                }
                await _todoServices.UpdateTodoAsync(id, request);
                return Ok(new { message = $"Todo item with Id {id} successfully updated." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while updating the Todo item with Id {id}.", error = ex.Message });
            }
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTodoAsync(Guid id)
        {
            try
            {
                var todo = await _todoServices.GetByIdAsync(id);
                if (todo == null)
                {
                    return NotFound(new { message = $"No Todo item with Id {id} found." });
                }
                await _todoServices.DeleteTodoAsync(id);
                return Ok(new { message = $"Todo item with Id {id} successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred while deleting the Todo item with Id {id}.", error = ex.Message });
            }
        }
    }
}