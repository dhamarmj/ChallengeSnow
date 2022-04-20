using ChallengeSnow.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeSnow.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne<Item>(s => s.Item_Number)
                .WithMany(g => g.Orders)
                .HasForeignKey(s => s.Item_NumberId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        //creating all the tables in my BD
        public DbSet<Item> Items { get; set; }
        public DbSet<Deal_Item> Deal_Items { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}