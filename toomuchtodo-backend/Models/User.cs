using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace toomuchtodo_backend.Models;

[Table("users")]
public class User
{
    [Key]
    public int id { get; private set; }
    public string username { get; private set; }
    public string email { get; private set; }
    public string password { get; private set; }
    public DateTime updated_at { get; private set; }

    public User(string username, string email, string password)
    {
        this.username = username ?? throw new ArgumentNullException(nameof(username));
        this.email = email ?? throw new ArgumentNullException(nameof(email));
        this.password = password ?? throw new ArgumentNullException(nameof(password));
        this.updated_at = DateTime.UtcNow;
    }

    public User(string email, string password)
    {
        this.email = email ?? throw new ArgumentNullException(nameof(email));
        this.password = password ?? throw new ArgumentNullException(nameof(password));
    }

    public void UpdatePassword(string newPasswordHash)
    {
        this.password = newPasswordHash ?? throw new ArgumentNullException(nameof(newPasswordHash));
        this.updated_at = DateTime.UtcNow;
    }
}