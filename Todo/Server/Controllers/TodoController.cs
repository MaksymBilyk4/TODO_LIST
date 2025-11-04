using Microsoft.AspNetCore.Mvc;
using Server.DTOs;
using Server.Services;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateTodoAsync([FromBody] TodoCreateRequestDto dto)
    {
        await _todoService.CreateTodoAsync(dto);
        return Created();
    }

    [HttpPatch]
    [Route("{id:int}")]
    public async Task<IActionResult> PartialTodoUpdateAsync(int id, [FromBody] TodoPatchRequestDto dto)
    {
        await _todoService.UpdateTodoAsync(dto, id);
        return NoContent();
    }

    [HttpPost]
    [Route("complete/{id:int}")]
    public async Task<IActionResult> CompleteTodoAsync(int id)
    {
        await _todoService.CompleteTodoAsync(id);
        return NoContent();
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetTodoByIdAsync(int id)
    {
        var todo = await _todoService.GetTodoByIdAsync(id);
        return Ok(todo);
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetTodosAsync()
    {
        var todos = await _todoService.GetAllTodosAsync();
        return Ok(todos);
    }
}