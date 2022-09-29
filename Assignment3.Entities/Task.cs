using System.ComponentModel.DataAnnotations;
using Assignment3.Core;

namespace Assignment3.Entities;

public class Task
{
    public int Id {get; set;}

    [Required]
    [StringLength(50)]
    public string Title {get; set;} = null!;

    public User AssignedTo {get; set;} = null!;

    [StringLength(100)]
    public string description {get; set;} = null!;

    [Required]
    public State state{get; set;}
    public ICollection<Tag> Tags {get; set;} = null!;

    public DateTime Created {get; set;}

    public DateTime StateUpdated {get; set;}

}

