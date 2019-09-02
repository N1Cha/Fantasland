using Autofac;

namespace Fantasland.Infrastructure
{
    public static class Bootstraper
    {
        public static IContainer Container { get; set; }

        public static void Init()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<MainScreenViewModel>().AsSelf();

            Container = builder.Build();
        }
    }
}
