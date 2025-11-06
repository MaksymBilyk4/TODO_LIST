using Microsoft.EntityFrameworkCore;
using Server.DTOs;
using Server.Exceptions;
using Server.Models;

namespace Server.Services;

public class TodoService : ITodoService
{

    private const int  MinTitleLength = 5; 
    private readonly TodolistContext _todolistContext;
    
    public TodoService() {}

    public TodoService(TodolistContext todolistContext)
    {
        _todolistContext = todolistContext;
    }

    public async Task CreateTodoAsync(TodoCreateRequestDto dto)
    {
        ValidateTodo(dto.Title, dto.Description);

        var todo = new Todo
        {
            Title = dto.Title,
            Description = dto.Description,
            IsDone = dto.Status
        };

        _todolistContext.Todos.Add(todo);
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
        await _todolistContext.SaveChangesAsync();
    }

    public async Task UpdateTodoAsync(TodoPatchRequestDto dto, int id)
    {
        Todo todo = await FindByIdAsync(id, false);
        ValidateTodo(dto.Title, dto.Description);
        
        if (dto.Description != null)
            todo.Description = dto.Description;

        if (dto.Title != null)
            todo.Title = dto.Title;

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
    
    public void ValidateTodo(string? title, string? description)
    {
        if (description != null && description.Length == 0)
            throw new ArgumentException("Description can't be empty");

        if (title != null && title.Length < MinTitleLength)
            throw new ArgumentException($"Title should contain at least {MinTitleLength} characters");
    }
}