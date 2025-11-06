using Server.Services;

namespace TodoTest;

public class TodoServiceTest
{
    [Fact]
    public void ValidateTodo_WhenTitleUnderMinTitleLengthCharacters_ShouldThrowArgumentException()
    {
        TodoService todoService = new TodoService();
        
        Assert.Throws<ArgumentException>(() => todoService.ValidateTodo("ab", null));
    }
    
    [Fact]
    public void ValidateTodo_WhenDescriptionHasNoCharacters_ShouldThrowArgumentException()
    {
        TodoService todoService = new TodoService();
        
        Assert.Throws<ArgumentException>(() => todoService.ValidateTodo(null, ""));
    }

    [Fact]
    public void ValidateTodo_WhenTitleAndDescriptionIsNull_ShouldNotThrow()
    {
        TodoService todoService = new TodoService();

        var exception = Record.Exception(() => todoService.ValidateTodo(null, null));
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateTodo_WhenTitleAndDescriptionIsValid_ShouldNotThrow()
    {
        TodoService todoService = new TodoService();

        var exception = Record.Exception(() => todoService.ValidateTodo("Jesus", "Is Lord"));
        Assert.Null(exception);
    }
}