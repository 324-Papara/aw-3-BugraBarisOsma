
using Microsoft.Extensions.Configuration;       
using Autofac;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Para.Api.Middleware;
using Para.Base.Response;
using Para.Bussiness;
using Para.Bussiness.Command;
using Para.Bussiness.Cqrs;
using Para.Data.Context;
using Para.Data.DapperRepository;
using Para.Data.Domain;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Api.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    private readonly IConfiguration Configuration;

    public AutofacBusinessModule(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    
    protected override void Load(ContainerBuilder builder)
    {
       var connectionString = Configuration.GetConnectionString("PostgresSqlConnection");
        builder.Register(c => new ParaDbContext(new DbContextOptionsBuilder<ParaDbContext>()
                .UseNpgsql(connectionString).Options))   
            .AsSelf()
            .InstancePerLifetimeScope();
        
        // Mapper dosyasinin register edilmesi
        var mapConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        
        builder.RegisterInstance(mapConfig.CreateMapper())
            .As<IMapper>()
            .SingleInstance();
        
        // UnitOfWork regiter edilmesi
        builder.RegisterType<UnitOfWork>()
            .As<IUnitOfWork>()
            .InstancePerLifetimeScope();


        //Mediatr'in register edilmesi        
        builder.RegisterType<Mediator>()
            .As<IMediator>()
            .InstancePerLifetimeScope();
        
        // Butun commandlarin register edilmesi
        builder.RegisterAssemblyTypes(typeof(CreateCustomerCommand).Assembly)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<RequestResponseLoggerMiddleware>()
            .AsSelf()
            .InstancePerLifetimeScope();
        
        //Dapper repository
        builder
            .RegisterType<CustomerRepository>()
            .As<ICustomerRepository>()
            .InstancePerLifetimeScope();
        
        builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

    }
}