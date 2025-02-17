using toomuchtodo_backend.Models;

namespace toomuchtodo_backend.Persistence;

public class UserRepository : IUserRepository
{
    private readonly ConnectionContext _context;

    public UserRepository(ConnectionContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User? GetById(int id)
    {
        return _context.Users.Find(id);
    }

    public User? GetByEmail(string email)
    {
        return _context.Users.FirstOrDefault(u => u.email == email);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}