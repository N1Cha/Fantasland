using Autofac;
using Fantasland.WarehouseModule;

namespace Fantasland.Infrastructure
{
    public static class Bootstraper
    {
        public static IContainer Container { get; set; }

        public static void Init()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<MainScreenViewModel>().AsSelf();
            builder.RegisterType<FullStockViewModel>().AsSelf();
            builder.RegisterType<FullStockView>().AsSelf();

            Container = builder.Build();
        }
    }
}
