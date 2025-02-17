namespace toomuchtodo_backend.Models;

public interface IUserRepository
{
    void Add(User user);
    
    User? GetById(int id);
    
    User? GetByEmail(string email);
    
    User? GetByUsername(string username);
    
    void Update(User user);
    
    void Delete(User user);
}