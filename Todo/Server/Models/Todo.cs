using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class Todo
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public bool? IsDone { get; set; }
}
