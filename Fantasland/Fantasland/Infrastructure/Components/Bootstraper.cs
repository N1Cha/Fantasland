﻿using Autofac;
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

            builder.RegisterType<FullStockView>().AsSelf();
            builder.RegisterType<CategoryView>().AsSelf();
            builder.RegisterType<NewCategoryView>().AsSelf();
            builder.RegisterType<NewProductView>().AsSelf();
            builder.RegisterType<InsertStockView>().AsSelf();

            Container = builder.Build();
        }
    }
}
