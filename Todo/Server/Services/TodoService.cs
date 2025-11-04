using Microsoft.EntityFrameworkCore;
using Server.DTOs;
using Server.Exceptions;
using Server.Models;

namespace Server.Services;

public class TodoService : ITodoService
{

    private readonly TodolistContext _todolistContext;
    

    public TodoService(TodolistContext todolistContext)
    {
        _todolistContext = todolistContext;
    }

    public async Task CreateTodoAsync(TodoCreateRequestDto dto)
    {
        if (dto.Description.Length == 0)
        {
            throw new ArgumentException("Description can't be empty");
        }

        if (dto.Title.Length < 3)
        {
            throw new ArgumentException("Title should contain at least 3 characters");
        }
        
        Todo todo = new Todo
        {
            Description = dto.Description,
            Title = dto.Title,
            IsDone = dto.Status
        };

        await _todolistContext.AddAsync(todo);
        await _todolistContext.SaveChangesAsync();
    }

    public async Task<TodoResponseDto> GetTodoByIdAsync(int id)
    {
        Todo todo = await FindByIdAsync(id, false);
        return new TodoResponseDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Status = todo.IsDone
            };
    }

    public async Task<IEnumerable<TodoResponseDto>> GetAllTodosAsync()
    {
        return await _todolistContext.Todos
            .Select(t => new TodoResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.IsDone
            })
            .ToListAsync();
    }

    public async Task CompleteTodoAsync(int id)
    {
        Todo todo = await FindByIdAsync(id, false);

        todo.IsDone = true;
        _todolistContext.Todos.Update(todo);
        await _todolistContext.SaveChangesAsync();
    }

    public async Task UpdateTodoAsync(TodoPatchRequestDto dto, int id)
    {
        Todo todo = await FindByIdAsync(id, false);

        if (dto.Description != null)
        {
            if (dto.Description.Length == 0)
            {
                throw new ArgumentException("Description can't be empty");
            }

            todo.Description = dto.Description;
        }
        
        if (dto.Title != null)
        {
            if (dto.Title.Length < 3)
            {
                throw new ArgumentException("Title should contain at least 3 characters");
            }

            todo.Title = dto.Title;
        }

        _todolistContext.Todos.Update(todo);
        await _todolistContext.SaveChangesAsync();

    }

    private async Task<Todo?> FindByIdAsync(int id, bool allowNull)
    {
        Todo? todo = await _todolistContext.Todos.FirstOrDefaultAsync(t => t.Id == id);

        if (!allowNull && todo == null) 
        {
            throw new NotFoundException($"Todo with id {id} was not found");
        }

        return todo;
    }
}