global using Microsoft.EntityFrameworkCore;
using Task4AppMvc.Models;

namespace Task4AppMvc.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<User> Users { get; set; }
    }
}
