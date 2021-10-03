using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Services.ViewModels;
using TodoApi.Data.Models;

namespace TodoApi.Mappers
{
    using Abstract;

    public class TodoMapper : IBaseMapper<TodoItem, TodoItemDTO>
    {
        public TodoItemDTO FromDb(TodoItem dbItem) 
        {
            if (dbItem is null)
                throw new ArgumentNullException("Db item is null");

            return new TodoItemDTO
            {
                Id = dbItem.Id,
                IsComplete = dbItem.IsComplete,
                Name = dbItem.Name
            };
        }

        public TodoItem ToDb(TodoItemDTO viewModel) 
        {
            if (viewModel is null)
                throw new ArgumentNullException("View item is null");

            return new TodoItem
            {
                Name = viewModel.Name,
                IsComplete = viewModel.IsComplete
            };
        }

        public void UpdateDb(TodoItem dbItem, TodoItemDTO viewModel) 
        {
            if (dbItem is null)
                throw new ArgumentNullException("Db item is null");

            if (viewModel is null)
                throw new ArgumentNullException("View item is null");

            dbItem.Name = viewModel.Name;
            dbItem.IsComplete = viewModel.IsComplete;
        }
    }
}
