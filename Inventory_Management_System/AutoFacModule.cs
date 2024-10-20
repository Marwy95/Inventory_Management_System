using Autofac;
using Autofac.Core;
using FluentValidation;
using Inventory_Management_System.VerticalSlice.Common.Services.BackgroundJobServices;
using Inventory_Management_System.VerticalSlice.Common.Services.EmailServices;
using Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQConsumers;
using Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQPublishers;
using Inventory_Management_System.VerticalSlice.Common.Services.TokenService;
using Inventory_Management_System.VerticalSlice.Data;
using Inventory_Management_System.VerticalSlice.Data.Repositories;
using Inventory_Management_System.VerticalSlice.Features.Users.RegisterUser;
using Inventory_Management_System.VerticalSlice.Features.Users.RegisterUser.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            //
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerLifetimeScope();
            //
            //// Register FluentValidation validators
            //builder.RegisterType<RegisterUserEndPointValidator>()
            //       .As<IValidator<RegisterUserCommand>>()
            //       .InstancePerLifetimeScope();

            //// Register all other validators if needed
            //builder.RegisterAssemblyTypes(typeof(RegisterUserEndPointValidator).Assembly)
            //       .Where(t => t.IsSubclassOf(typeof(AbstractValidator<>)))
            //       .As<IValidator>()
            //       .InstancePerLifetimeScope();
            //builder.RegisterType<RegisterUserEndPointValidation>().InstancePerLifetimeScope();
            //
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(typeof(IRabbitMQPublisher).Assembly)
                       .AsImplementedInterfaces()
                       .InstancePerLifetimeScope();
            //
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<JWTTokenService>().As<ITokenService>().InstancePerLifetimeScope();
            builder.RegisterType<RabbitMQConsumer>().As<IHostedService>().SingleInstance();
            //

            //
            builder.RegisterType<LowStockbackgroundJob>()
               .AsSelf()
               .InstancePerLifetimeScope();
        }
    }
}
