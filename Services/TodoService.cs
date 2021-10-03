using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Mappers.Abstract;
using TodoApi.Data.Models;
using TodoApi.Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Services
{
    using ViewModels;

    public class TodoService
    {
        private readonly IBaseMapper<TodoItem, TodoItemDTO> _todoMapper;
        private readonly IEntityBaseRepository<TodoItem> _todoRepository;

        public TodoService(IBaseMapper<TodoItem, TodoItemDTO> todoMapper,
            IEntityBaseRepository<TodoItem> todoRepository)
        {
            _todoMapper = todoMapper;
            _todoRepository = todoRepository;
        }

        /// <summary>
        /// Create todo
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<TodoItemDTO> CreateTodoAsync(TodoItemDTO data)
        {
            var dbModel = _todoMapper.ToDb(data);
            await _todoRepository.AddAsync(dbModel);
            return _todoMapper.FromDb(dbModel);
        }

        /// <summary>
        /// Update todo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItemDTO"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTodoAsync(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id || todoItemDTO is null)
                throw new InvalidOperationException("Incorrect input data");

            var dbModel = await _todoRepository.GetSingleAsync(id);
            _todoMapper.UpdateDb(dbModel, todoItemDTO);

            try
            {
                await _todoRepository.UpdateAsync(dbModel);
            }
            catch (DbUpdateConcurrencyException) when (!_todoRepository.IsExist(id))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Delete todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteTodoAsync(long id)
        {
            var dbModel = await _todoRepository.GetSingleAsync(id);
            await _todoRepository.DeleteAsync(dbModel);
        }

        /// <summary>
        /// Get one todo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TodoItemDTO> GetTodo(long id) 
        {
            var dbModel = await _todoRepository.GetSingleAsync(id);
            return _todoMapper.FromDb(dbModel);
        }

        /// <summary>
        /// Get all todos
        /// </summary>
        /// <returns></returns>
        public async Task<List<TodoItemDTO>> GetTodos() 
        {
            var todos = await _todoRepository.GetAllAsync();
            return todos
                .Select(_todoMapper.FromDb)
                .ToList();
        }
    }
}
