using Assignment3.Core;
using Microsoft.EntityFrameworkCore;
namespace Assignment3.Entities;


public sealed class TaskRepository : ITaskRepository
{   
     private readonly KanbanContext _context;

    public TaskRepository(KanbanContext context)
    {
        _context = context;
    }
    public (Response Response, int TaskId) Create(TaskCreateDTO task) {
        
        Response response;

        
          var entity = new Task();
            entity.Created = DateTime.UtcNow;
            entity.Title = task.Title;
            entity.description = task.Description;
            entity.state = State.New;
            entity.StateUpdated = DateTime.UtcNow;
            var tagList = new List<Tag>();
            foreach (string s in task.Tags){
                if((from t in _context.Tags where t.Name== s select t).FirstOrDefault()!= null){
                    tagList.Add((from t in _context.Tags where t.Name== s select t).FirstOrDefault());
                }
            }
            entity.Tags = tagList;



            if((from u in _context.Users where u.Id == task.AssignedToId select u).FirstOrDefault()==null){
                response = Response.BadRequest;
            }
            else {
                entity.AssignedTo = (from u in _context.Users where u.Id == task.AssignedToId select u).FirstOrDefault()!;
                 response = Response.Created;
            }

            
            _context.Tasks.Add(entity);
            _context.SaveChanges();
    
        var created = new TaskDTO(entity.Id, entity.Title, entity.AssignedTo.Name, entity.Tags.Select(t=>t.Name).ToList().AsReadOnly(), entity.state);
        
        return (response, created.Id);
    }
    public IReadOnlyCollection<TaskDTO> ReadAll() {
         var tasks = from t in _context.Tasks
                    orderby t.Title
                    select new TaskDTO(t.Id, t.Title, t.AssignedTo.Name, t.Tags.Select(t=>t.Name).ToList().AsReadOnly(), t.state);

        if (tasks.Any()){
            return tasks.ToArray()!;
        }
        else {
            return null!;
        }     

    }
    public IReadOnlyCollection<TaskDTO> ReadAllRemoved() {
        var tasks = from t in _context.Tasks
                    orderby t.Title
                    where t.state == State.Removed
                    select new TaskDTO(t.Id, t.Title, t.AssignedTo.Name, t.Tags.Select(t=>t.Name).ToList().AsReadOnly(), t.state);

        if (tasks.Any()){
            return tasks.ToArray()!;
        }
        else {
            return null!;
        }    

    }
    public IReadOnlyCollection<TaskDTO> ReadAllByTag(string tag) {
        var tasks = from t in _context.Tasks
                    orderby t.Title
                    where t.Tags.Select(t=>t.Name).ToList().AsReadOnly().Contains(tag)
                    select new TaskDTO(t.Id, t.Title, t.AssignedTo.Name, t.Tags.Select(t=>t.Name).ToList().AsReadOnly(), t.state);

         if (tasks.Any()){
            return tasks.ToArray()!;
        }
        else {
            return null!;
        }    

    }
    public IReadOnlyCollection<TaskDTO> ReadAllByUser(int userId) {
        var tasks = from t in _context.Tasks
                    orderby t.Title
                    where t.AssignedTo.Id == userId 
                    select new TaskDTO(t.Id, t.Title, t.AssignedTo.Name, t.Tags.Select(t=>t.Name).ToList().AsReadOnly(), t.state);

         if (tasks.Any()){
            return tasks.ToArray()!;
        }
        else {
            return null!;
        }  

    }
    public IReadOnlyCollection<TaskDTO> ReadAllByState(State state) {
        var tasks = from t in _context.Tasks
                    orderby t.Title
                    where t.state == state 
                    select new TaskDTO(t.Id, t.Title, t.AssignedTo.Name, t.Tags.Select(t=>t.Name).ToList().AsReadOnly(), t.state);

         if (tasks.Any()){
            return tasks.ToArray()!;
        }
        else {
            return null!;
        }  

    }
    public TaskDetailsDTO Read(int taskId) {
        var tasks = from t in _context.Tasks
                    where t.Id == taskId
                    select new TaskDetailsDTO(t.Id, t.Title, t.description, t.Created, t.AssignedTo.Name, t.Tags.Select(t=>t.Name).ToList().AsReadOnly(), t.state, t.StateUpdated);
         if (tasks.Any()){
            return tasks.FirstOrDefault()!;
        }
        else {
            return null!;
        }
    }
    public Response Update(TaskUpdateDTO task) {
         var entity = _context.Tasks.Find(task.Id);
        Response response;

        if (entity is null)
        {
            response = Response.NotFound;
        }
        else if (_context.Tasks.FirstOrDefault(c => c.Id != task.Id) != null)
        {
            response = Response.Conflict;
        }
        else
        {   entity.Id = task.Id;
            entity.Title = task.Title;
            entity.description = task.Description!;
            if(entity.state != task.State){
                entity.state = task.State;
                entity.StateUpdated = DateTime.UtcNow;
            }
        
            var tagList = new List<Tag>();
            foreach (string s in task.Tags){
                if((from t in _context.Tags where t.Name== s select t).FirstOrDefault()!= null){
                    tagList.Add((from t in _context.Tags where t.Name== s select t).FirstOrDefault()!);
                }
            }
            entity.Tags = tagList;
            if((from u in _context.Users where u.Id == task.AssignedToId select u).FirstOrDefault()==null){
                response = Response.BadRequest;
            }
            else {
                entity.AssignedTo = (from u in _context.Users where u.Id == task.AssignedToId select u).FirstOrDefault()!;
                  response = Response.Updated;
            }

            _context.SaveChanges();
        }

        return response;

    }
    public Response Delete(int taskId) {
        var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);
        Response response;

        if (task is null){
            response = Response.NotFound;
        }

        else if (task.state == State.Active){
            task.state = State.Removed;
            response = Response.Updated;
        }
        else if (task.state == State.Resolved ||task.state == State.Closed || task.state == State.Removed){
            response = Response.Conflict;
        }
        else {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            response = Response.Deleted;
        }
        return response;

    }

}
