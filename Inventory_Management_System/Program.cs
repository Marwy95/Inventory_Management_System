
using Autofac.Extensions.DependencyInjection;
using Autofac;
using AutoMapper;
using Inventory_Management_System.VerticalSlice.Common.MapperHelper;
using DotNetEnv;
using Inventory_Management_System.VerticalSlice.Common.Profiles;
using Inventory_Management_System.VerticalSlice.Middlewares;

namespace Inventory_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Enviroment
            Env.Load();
            //AUTOFAC
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            builder.RegisterModule(new AutoFacModule()));
            //MEDIATR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
            builder.Services.AddAutoMapper(typeof(ProductProfile));
            var app = builder.Build();
            //AUTOMAPPER
            MapperService.Mapper = app.Services.GetService<IMapper>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<GlobalErrorHandlerMiddleware>();
            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
