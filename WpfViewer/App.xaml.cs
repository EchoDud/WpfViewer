using Prism.Ioc;
using Prism.Unity;
using System.Configuration;
using System.Data;
using System.Windows;
using WpfViewer.Services;
using WpfViewer.ViewModels;

namespace WpfViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register the ModelService with the interface IModelService
            containerRegistry.RegisterSingleton<IModelService, ModelService>();

            // Optionally, ensure MainViewModel is also registered if not done automatically
            containerRegistry.Register<MainViewModel>();
        }
    }

}
