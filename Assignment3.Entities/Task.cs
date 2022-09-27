using System.ComponentModel.DataAnnotations;

namespace Assignment3.Entities;

public class Task
{
    public int Id;

    [Required]
    [StringLength(50)]
    public string Title {get; set;} 

    public User AssignedTo {get; set;} 

    [StringLength(100)]
    public string description {get; set;}
    public enum State {New, Active, Resolved, Closed, Removed}

    [Required]
    public State state{get; set;}
    public IEnumerable<Tag> Tags;
}
