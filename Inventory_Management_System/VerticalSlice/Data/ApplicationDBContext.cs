using Inventory_Management_System.VerticalSlice.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Inventory_Management_System.VerticalSlice.Data
{
    public class ApplicationDBContext: DbContext 
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
       : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        DbSet<Product>Products { get; set; }
    }
}
