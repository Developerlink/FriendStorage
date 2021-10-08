using FriendStorage.DataAccess;
using FriendStorage.UI.DataProvider;
using FriendStorage.UI.View;
using FriendStorage.UI.ViewModel;
using System.Windows;
using Autofac;
using FriendStorage.UI.Startup;

namespace FriendStorage.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootStrapper = new Bootstrapper();
            var container = bootStrapper.Bootstrap();

            var mainWindow = container.Resolve<MainWindow>(); 
            mainWindow.Show();
        }
    }
}
