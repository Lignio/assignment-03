namespace Assignment3.Entities;
using System.ComponentModel.DataAnnotations;

public class Tag
{
    public int Id {get; set;}
    
    [StringLength(50)]
    [Required]
    public String Name {get; set;} = null!;

    public virtual ICollection<Task> Tasks {get; set;}
}
