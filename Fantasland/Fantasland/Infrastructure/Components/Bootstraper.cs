using Autofac;
using Fantasland.Sales;
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
            builder.RegisterType<NewProductViewModel>().AsSelf();
            builder.RegisterType<InsertStockViewModel>().AsSelf();
            builder.RegisterType<NewSaleViewModel>().AsSelf();
            builder.RegisterType<AllSalesViewModel>().AsSelf();
            builder.RegisterType<PriceListViewModel>().AsSelf();

            builder.RegisterType<FullStockView>().AsSelf();
            builder.RegisterType<CategoryView>().AsSelf();
            builder.RegisterType<NewCategoryView>().AsSelf();
            builder.RegisterType<NewProductView>().AsSelf();
            builder.RegisterType<InsertStockView>().AsSelf();
            builder.RegisterType<NewSaleView>().AsSelf();
            builder.RegisterType<AllSalesView>().AsSelf();
            builder.RegisterType<PriceListView>().AsSelf();

            Container = builder.Build();
        }
    }
}
