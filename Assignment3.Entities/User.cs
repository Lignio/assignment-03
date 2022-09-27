namespace Assignment3.Entities;
using System.ComponentModel.DataAnnotations;

public class User
{
    int Id {get; set;}
    [Required]
    [StringLength(100)]
    public String Name {get; set;}
    [Required]
    [StringLength(100)]
    public String Email {get; set;}
    public virtual IEnumerable<Task> Tasks { get; set; }
}
