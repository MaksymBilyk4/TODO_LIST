using Server.DTOs;

namespace Server.Services;

public interface ITodoService
{
    Task CreateTodoAsync(TodoCreateRequestDto dto);

    Task<TodoResponseDto> GetTodoByIdAsync(int id);

    Task<IEnumerable<TodoResponseDto>> GetAllTodosAsync();

    Task UpdateTodoAsync(TodoPatchRequestDto dto, int id);

    Task CompleteTodoAsync(int id);
}