using System.Reflection;
using Autofac;
using Panel.Cache;
using Panel.Data;
using Panel.Data.Repositories;
using Panel.Repository;
using Panel.Service.Mapping;
using Panel.Services;
using Panel.Service.Services;
using Panel.UnitOfWork;
using Module = Autofac.Module;

namespace Panel.API.Modules;

public class RepoServiceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {

        builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(Services<>)).As(typeof(IService<>)).InstancePerLifetimeScope();
        builder.RegisterType<Data.UnitOfWork.UnitOfWork>().As<IUnitOfWork>();
        
        var apiAssembly = Assembly.GetExecutingAssembly();
        var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
        var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

        builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
            .Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();
        
        builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
            .Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
        
        //builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();
    }
}