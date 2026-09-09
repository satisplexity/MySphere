using System.Windows;

namespace MySphere.Presentation
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = new Views.Windows.MainWindow();
            mainWindow.Show();
        }
    }
}