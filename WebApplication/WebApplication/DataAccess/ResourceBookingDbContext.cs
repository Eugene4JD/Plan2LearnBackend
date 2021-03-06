using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.DataAccess
{
    public class ResourceBookingDbContext : DbContext
    {
       public DbSet<Booking>  Bookings { get; set; }
       public DbSet<Resource> Resources { get; set; }

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {
           optionsBuilder.UseSqlite("Data source = ResourceBooking.db");
           optionsBuilder.EnableSensitiveDataLogging();
       }
    }
}