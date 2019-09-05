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
            builder.RegisterType<CategoryViewModel>().AsSelf();
            builder.RegisterType<NewCategoryViewModel>().AsSelf();


            builder.RegisterType<FullStockView>().AsSelf();
            builder.RegisterType<CategoryView>().AsSelf();
            builder.RegisterType<NewCategoryView>().AsSelf();

            Container = builder.Build();
        }
    }
}
