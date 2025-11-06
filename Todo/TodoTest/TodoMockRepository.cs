using Server.Models;

namespace TodoTest;

public static class TodoMockRepository
{

    public static List<Todo> Todos = new List<Todo>
    {
        new Todo {Id = 1, Title = "Go to shop", Description = "Buy meat, milk, bread, fruit, vegetables", IsDone = false},
        new Todo {Id = 2, Title = "Go GYM", Description = "Leg day", IsDone = false},
        new Todo {Id = 3, Title = "Complete Homework", Description = "Mathematics, Physics", IsDone = false},
        new Todo {Id = 4, Title = "Finish Project", Description = "Review code & approve", IsDone = false}
    };

}