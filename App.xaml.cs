using System.Configuration;
using System.Data;
using System.Windows;

namespace TP_MESERP_Core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var login = new TP_MESERP_Core.Views.LoginView();
            login.Show();
        }

    }

}
