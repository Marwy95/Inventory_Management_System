using Autofac;
using Inventory_Management_System.VerticalSlice.Data;
using Inventory_Management_System.VerticalSlice.Data.Repositories;

namespace Inventory_Management_System
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDBContext>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerLifetimeScope();
        }
    }
}
