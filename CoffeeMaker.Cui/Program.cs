using Autofac;
using CoffeeMaker.Application;
using CoffeeMaker.Application.Services;
using CoffeeMaker.Domain.Repositories;
using CoffeeMaker.Infrastructure.Data;
using CoffeeMaker.Infrastructure.Data.Repositories;

namespace CoffeeMaker.Cui
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<InventoryService>().AsSelf();
            builder.RegisterType<Screen>().AsSelf();
            builder.RegisterType<EFUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<CoffeeMakerDbContext>().AsSelf();
            builder.RegisterType<EFCoffeeMachineRepository>().As<ICoffeeMachineRepository>();
            builder.RegisterType<BeverageService>().AsSelf();
            IContainer container = builder.Build();

            while (true)
            {
                using (ILifetimeScope scope = container.BeginLifetimeScope())
                {
                    Screen screen = container.Resolve<Screen>();
                    screen.Run();
                }
            }
        }
    }
}
