using ChallengeSnow.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeSnow.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        //creating all the tables in my BD
        public DbSet<Item> Items { get; set; }
        public DbSet<Deal_Item> Deal_Items { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}