using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TodoApi.Services;
using TodoApi.Services.ViewModels;
using System;

namespace TodoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoItemsController(TodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Get all ToDo items
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns all items</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoItemDTO>))]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return await _todoService.GetTodos();
        }

        /// <summary>
        /// Get one ToDo items
        /// </summary>
        /// <param name="id">Id of existing ToDo items</param>
        /// <returns></returns>
        /// <response code="200">Return item with requested id</response>
        /// <response code="404">Item not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TodoItemDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            try
            {
                var todoItem = await _todoService.GetTodo(id);
                return todoItem;
            }
            catch (Exception) 
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Update ToDo item
        /// </summary>
        /// <param name="id">Id of existing ToDo items</param>
        /// <param name="todoItemDTO">New data for existing ToDo item</param>
        /// <returns></returns>
        /// <response code="204">Item with requested id is updated</response>
        /// <response code="404">Item with requested id is not found</response>
        /// <response code="400">requested id is not equals with id in body</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            try
            {
                var isSuccess = await _todoService.UpdateTodoAsync(id, todoItemDTO);
                if (isSuccess) 
                {
                    return NoContent();
                } 
                else
                {
                    return NotFound();
                }
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
            catch (ArgumentException) 
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Create new ToDo item
        /// </summary>
        /// <param name="todoItemDTO">New data for ToDo item</param>
        /// <returns></returns>
        /// <response code="201">Return created item</response>
        /// <response code="400">Input data is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TodoItemDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoModel = await _todoService.CreateTodoAsync(todoItemDTO);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoModel.Id },
                todoModel);
        }

        /// <summary>
        /// Delete ToDo item
        /// </summary>
        /// <param name="id">Id of existing ToDo items</param>
        /// <returns></returns>
        /// <response code="204">ToDo item with requested id is deleted</response>
        /// <response code="404">ToDo item with requested id not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            try
            {
                await _todoService.DeleteTodoAsync(id);
                return NoContent();
            }
            catch (Exception) 
            {
                return NotFound();
            }
        }
    }
}
