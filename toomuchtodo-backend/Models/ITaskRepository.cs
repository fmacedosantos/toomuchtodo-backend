namespace toomuchtodo_backend.Models;

public interface ITaskRepository
{
    void Add(TaskItem task);
    TaskItem? GetById(int id);
    IEnumerable<TaskItem> GetByUserId(int userId);
    void Update(TaskItem task);
    void Delete(TaskItem task);
}