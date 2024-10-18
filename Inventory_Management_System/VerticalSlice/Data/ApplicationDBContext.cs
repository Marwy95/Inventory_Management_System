using Inventory_Management_System.VerticalSlice.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Inventory_Management_System.VerticalSlice.Data
{
    public class ApplicationDBContext: DbContext 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("DATABASE_URL"))
               .LogTo(log => Debug.WriteLine(log), LogLevel.Information);
        }
        DbSet<Product>Products { get; set; }
    }
}
