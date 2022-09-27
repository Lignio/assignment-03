namespace Assignment3.Entities;
using Assignment3.Core;

public sealed class UserRepository : IUserRepository
{
    private readonly KanbanContext _context;

    public UserRepository(KanbanContext context)
    {
        _context = context;
    }

    (Response Response, int UserId) Create(UserCreateDTO user) {
        var entity = _context.Users.
        
        //return (status, created);
    }
    IReadOnlyCollection<UserDTO> ReadAll();
    UserDTO Read(int userId);
    Response Update(UserUpdateDTO user);
    Response Delete(int userId, bool force = false);

}
