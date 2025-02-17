using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace toomuchtodo_backend.Models;

[Table("users")]
public class User
{
    [Key]
    public int id { get; private set; }
    [Required]
    [StringLength(50)]
    public string username { get; private set; }
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string email { get; private set; }
    [Required]
    public string password { get; private set; }
    public DateTime created_at { get; private set; } = DateTime.UtcNow;
    public DateTime updated_at { get; private set; } = DateTime.UtcNow;


    public User(string username, string email, string password)
    {
        this.username = username ?? throw new ArgumentNullException(nameof(username));
        this.email = email ?? throw new ArgumentNullException(nameof(email));
        this.password = password ?? throw new ArgumentNullException(nameof(password));
        this.created_at = DateTime.UtcNow;
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