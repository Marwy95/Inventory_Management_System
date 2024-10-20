using Autofac;
using Inventory_Management_System.VerticalSlice.Data;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace Inventory_Management_System
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Configure DbContext with options
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDBContext>();
            optionsBuilder.UseSqlServer(connectionString); // Adjust if you're using a different provider

            builder.Register(c => new ApplicationDBContext(optionsBuilder.Options))
                   .AsSelf()
                   .InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerLifetimeScope();
        }
    }
}
