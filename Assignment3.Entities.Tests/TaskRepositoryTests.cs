using Microsoft.EntityFrameworkCore;
using Assignment3.Core;
using Microsoft.Data.Sqlite;

namespace Assignment3.Entities.Tests;

public class TaskRepositoryTests
{   
    private readonly KanbanContext _context;
    private readonly TaskRepository _repository;

    public TaskRepositoryTests()
    {
        
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<KanbanContext>();
        options.UseSqlite(connection);
        var context = new KanbanContext(options.Options);
        context.Database.EnsureCreated();
        var user1 = new User("Oliver", "OllesEmail.dk") { Id = 1 };
        context.Tasks.Add(new Task() {AssignedTo = user1, Title = "Do stuff", description = "...", state = State.New, Id = 1});
        context.SaveChanges();

        _context = context;
        _repository = new TaskRepository(_context);
        
    }

    [Fact] 
    public void Delete_Task_With_State_New_Returns_Deleted() 
    {
        var response = _repository.Delete(1);
        response.Should().Be(Response.Deleted);

        var entity = _context.Tasks.Find(1);       
        entity.Should().BeNull();
    }

    [Fact]
    public void Delete_Task_With_State_Active_Removed() 
    {
        var entity = _context.Tasks.Find(1);
        entity.state = State.Active;
        _context.SaveChanges();

        var state = _repository.Delete(1);
        entity.state.Should().Be(State.Removed);
    }   

    [Fact]
    public void Delete_Task_With_State_Resolved() 
    {
        var entity = _context.Tasks.Find(1);
        entity.state = State.Removed;
        _context.SaveChanges();

        var response = _repository.Delete(1);
        response.Should().Be(Response.Conflict);
    }

    [Fact]
    public void Delete_Task_With_State_Closed() 
    {
        var entity = _context.Tasks.Find(1);
        entity.state = State.Removed;
        _context.SaveChanges();

        var response = _repository.Delete(1);
        response.Should().Be(Response.Conflict);
    }

    [Fact]
    public void Delete_Task_With_State_Removed() {
        var entity = _context.Tasks.Find(1);
        entity.state = State.Removed;
        _context.SaveChanges();

        var response = _repository.Delete(1);
        response.Should().Be(Response.Conflict);
    }

    [Fact]
    public void Create_Task_Will_Set_State_New() 
    {
        //TODO Insert a Tag så create can do its thing
        var task = _repository.Create(new TaskCreateDTO("Procrastinating", 1, "Doing everything and nothing"));
        task.TaskId = 2;
        var entity = _context.Tasks.Find(2);
        entity.state.Should().Be(State.New);
    }
    
    [Fact]
    public void Updating_State_Of_Task_Will_Change_Current_Time() 
    {
        var response = _repository.Update(new TaskUpdateDTO(1, "Procrastinating", 1, "Doing everything and nothing", tag1, State.Active));
        var entity = _context.Tasks.Find(1);
        var actual = entity.stateUpdated;
        
        var expected = DateTime.UtcNow;
        actual.Should().BeCloseTo(expected, precision: TimeSpan.FromSeconds(5));
    }

    [Fact]
    public void Assigning_User_Which_Does_Not_Exist_Returns_BadRequest() 
    {
    }
}
