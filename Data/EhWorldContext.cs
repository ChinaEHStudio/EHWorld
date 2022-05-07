using Microsoft.EntityFrameworkCore;
using EHWorld.Models;
namespace EHWorld.Data
{
    public class EhWorldContext : DbContext
    {
        public EhWorldContext(DbContextOptions<EhWorldContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; } //22:17

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
        }
        
    }
}
