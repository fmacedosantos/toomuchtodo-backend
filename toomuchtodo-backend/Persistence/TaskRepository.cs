using toomuchtodo_backend.Models;

namespace toomuchtodo_backend.Persistence;

public class TaskRepository : ITaskRepository
{
    private readonly ConnectionContext _context;

    public TaskRepository(ConnectionContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(TaskItem task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
    }

    public TaskItem? GetById(int id)
    {
        return _context.Tasks.Find(id);
    }

    public IEnumerable<TaskItem> GetByUserId(int userId)
    {
        return _context.Tasks.Where(t => t.user_id == userId).ToList();
    }

    public void Update(TaskItem task)
    {
        _context.Tasks.Update(task);
        _context.SaveChanges();
    }

    public void Delete(TaskItem task)
    {
        _context.Tasks.Remove(task);
        _context.SaveChanges();
    }
}