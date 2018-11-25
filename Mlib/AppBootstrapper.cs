namespace Mlib
{
    using Autofac;
    using Caliburn.Micro;
    using Mlib.Data;
    using Mlib.Infrastructure;
    using Mlib.Properties;
    using Mlib.UI;
    using Mlib.UI.Additional;
    using Mlib.UI.Interfaces;
    using Mlib.UI.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Dynamic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    public class AppBootstrapper : BootstrapperBase
    {
        public IContainer Container { get; private set; }
        public AppBootstrapper()
        {
            ViewLocator.AddSubNamespaceMapping("Mlib.ViewModels", "Mlib.Views");
            ViewLocator.AddSubNamespaceMapping("Mlib.UI.ViewModels", "Mlib.UI.Views");

            Container = BuildContainer();

            Initialize();

            Database.SetInitializer(new CreateDatabaseIfNotExists<MlibData>());
            using (var context = new MlibData())
            {
                context.Database.CreateIfNotExists();
            }

        }
        private IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MlibData>().As<DbContext>().SingleInstance();
            builder.RegisterType<UnitOfWork>().AsSelf().SingleInstance();

            builder.RegisterType<ShellViewModel>().As<IViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<MainViewModel>().As<IMainViewModel>().SingleInstance();

            builder.RegisterType<AudioControlsViewModel>().As<IViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<SidePanelViewModel>().As<IViewModel>().AsSelf().SingleInstance();


            builder.RegisterType<MusicLibrary>().AsSelf().SingleInstance();
            builder.RegisterType<AudioPlayer>().AsSelf().SingleInstance();
            builder.RegisterType<PlaylistCreator>().AsSelf().SingleInstance();


            builder.Register<IWindowManager>(c => new WindowManager()).InstancePerLifetimeScope();
            builder.Register<IEventAggregator>(c => new EventAggregator()).InstancePerLifetimeScope();

            return builder.Build();
        }
        protected override object GetInstance(Type serviceType, string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                if (Container.IsRegistered(serviceType))
                    return Container.Resolve(serviceType);
            }
            else
            {
                if (Container.IsRegistered(serviceType))
                    return Container.Resolve(serviceType);
            }
            throw new Exception(string.Format("Could not locate any instances of contract {0}.", key ?? serviceType.Name));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return Container.Resolve(typeof(IEnumerable<>).MakeGenericType(serviceType)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            Container.InjectProperties(instance);
        }
        protected override void OnStartup(object sender, StartupEventArgs args)
        {
            try
            {
                AppWindowManager.ShowWindow<IMainViewModel>();
                Settings.Default.Save();
            }
            catch (Exception e)
            {
#if DEBUG
                Debug.WriteLine(string.Join(Environment.NewLine, Environment.NewLine, DateTime.Now, e.Message, e.Source, e.StackTrace));
#else
                using (StreamWriter writer = new StreamWriter(ConnectionStrings.Log, true))
                {
                    writer.Write(string.Join(Environment.NewLine, Environment.NewLine, DateTime.Now, e.Message, e.Source, e.StackTrace));
                }
#endif
                throw e;
            }
        }
    }
}
