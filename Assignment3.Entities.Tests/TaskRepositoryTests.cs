using Microsoft.EntityFrameworkCore;
using Assignment3.Core;
using Assignment3.Entities;
using Microsoft.Data.sqlite;

namespace Assignment3.Entities.Tests;

public class TaskRepositoryTests
{   
    public TaskRepositoryTests()
    {
        
        var connection = new Sqlite Connection("Filename=:memory:");
        connection.Open();
        var options = new DbContextOptionsBuilder<KanbanContext>()
        options.UseSqlite(connection);
        var context = new KanbanContext(options.Options);
        context.Database.EnsureCreated();

        context.Tasks.AddRange(new Task {Title = "Test", Description = "Test", State = State.New, Created = DateTime.Now, Updated = DateTime.Now});
        context.
        context.Cities.AddRange(new City("Metropolis") { Id = 1 }, new City("Gotham City") { Id = 2 });
        context.Characters.Add(new Character { Id = 1, AlterEgo = "Superman", CityId = 1 });
        context.SaveChanges();

        _context = context;
        _repository = new CityRepository(_context);
        
    }

    [Fact] 
    public void Delete_Task_With_State_New() {
    
    }

    [Fact]
    public void Delete_Task_With_State_Active() {
    
    }   

    [Fact]
    public void Delete_Task_With_State_Resolved() {
        
    }
}
