namespace Assignment3.Entities;
using System.ComponentModel.DataAnnotations;

public class Tag
{
    [Index(nameof(Tag.Name), IsUnique = true)]
    int id {get; set;}
    
    [StringLength(50)]
    [Required]
    public String Name {get; set;}


    public virtual ICollection<Task> Tasks {get; set;}
}
