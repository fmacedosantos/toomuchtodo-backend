using Microsoft.EntityFrameworkCore;
using toomuchtodo_backend.Models;

namespace toomuchtodo_backend.Persistence
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}