namespace Assignment3.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

[Index(nameof(User.Email), IsUnique = true)]
public class User
{
    public int? Id {get; set;}
    [Required]
    [StringLength(100)]
    public String Name {get; set;}
    [Required]
    [StringLength(100)]
    public String Email {get; set;}
    public virtual IEnumerable<Task> Tasks { get; set; }

    public User(string name, string email){
        Tasks = new List<Task>();
        Name = name;
        Email = email;
    }
}
