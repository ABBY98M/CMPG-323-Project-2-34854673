using Microsoft.EntityFrameworkCore;
using API_Project_2_34854673.Models;

namespace API_Project_2_34854673.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<JobTelemetry> JobTelemetries { get; set; }
    }
}

