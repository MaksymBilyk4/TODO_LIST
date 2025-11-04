namespace Server.DTOs;

public class TodoPatchRequestDto
{
    public string? Description { get; set; }
    public string? Title { get; set; }
}