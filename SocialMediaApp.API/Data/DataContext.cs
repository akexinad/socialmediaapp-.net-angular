using Microsoft.EntityFrameworkCore;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Data
{
    // This is where EntityFrameworkCore knows about our Models.
    // DataContext DERIVES/IHERITS from DbContext
    public class DataContext : DbContext
    {
        // We need to build a constructor in order to pass in DbContext options which do all the work.
        // Here we declare DbContextOptions of type DataContext
        // This constructor needs to be chained to the base DbContext constructor, and we send up the options to the constructor.
        public DataContext(DbContextOptions<DataContext> options) : base (options) { }

        // It is convention to make the prop name plural as it will become the table name in our database.
        public DbSet<Value> Values { get; set; }

        // NOTE: whenever you add a new model or modify a model, we need to run a migration.
        public DbSet<User> Users { get; set; }
    }
}