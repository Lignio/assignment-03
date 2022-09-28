namespace Assignment3.Entities;
using Assignment3.Core;

public sealed class TaskRepository
{   
    public State Delete(int taskId)
    {   State state;

        if(state == State.New) 
        {
            state = State.Deleted;
        }
        else if(state == State.Active)
        {
            state = State.Removed;
        }
        else 
        {
            return Conflict;
        }
    }

    public (State, TaskDTO) Create(TaskCreateDTO task) 
    {
        State state;
        state = State.New;
    }

    public 
}
