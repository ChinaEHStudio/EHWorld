using EHWorld.Models;
using Microsoft.EntityFrameworkCore;
namespace EHWorld.Data
{
    public class EhWorldContext : DbContext
    {
        public EhWorldContext(DbContextOptions<EhWorldContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; } //22:17
        public DbSet<Vnu> Vnumbers { get; set; } //11:59 5/8/2022


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Vnu>().ToTable("Vnumber");
        }

    }
}
