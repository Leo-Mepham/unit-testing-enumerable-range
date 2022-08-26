using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class UserContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; } = null!;
    }
}
