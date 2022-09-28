using Assignment3.Core;
namespace Assignment3.Entities;

public class TaskRepository
{   
    private readonly KanbanContext _context;

    public TaskRepository(KanbanContext context)
    {
        _context = context;
    }


    public Response Delete(int taskId)
    {   State state;
        Response response;
        //Is this right?   
        if(state == State.New) 
        {
            response = Response.Deleted;
        }
        else if(state == State.Active)
        {
            state = State.Removed;
        }
        else 
        {
            return Response.Conflict;
        }
        //Not sure this is the right return
        return Response.Conflict;
    }

    public (State, TaskDTO) Create(TaskCreateDTO task) 
    {
        State state;
        state = State.New;
    }

    public 
}
