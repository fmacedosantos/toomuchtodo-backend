namespace toomuchtodo_backend.Models;

public interface IUserRepository
{
    void Add(User user);
    
    User GetById(int id);
}