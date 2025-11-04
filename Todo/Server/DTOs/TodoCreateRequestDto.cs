namespace Server.DTOs;

public class TodoCreateRequestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Status { get; set; }
}