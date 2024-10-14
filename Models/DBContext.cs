using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Models
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options):base(options)
        {
            
        }
        public DbSet<User> User { get; set; }
        public DbSet<Admin> Admin { get; set; }
    }
}
