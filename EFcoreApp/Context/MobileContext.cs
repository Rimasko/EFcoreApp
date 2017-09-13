using EFcoreApp.Models;
using EFcoreApp.MapsModels;
using Microsoft.EntityFrameworkCore;

namespace EFcoreApp.Context
{
    public class MobileContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Place> Places { get; set; }


        public MobileContext(DbContextOptions<MobileContext> dbContext) : base(dbContext)
        { }
    }
}
