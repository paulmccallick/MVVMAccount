using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AccountDomain;

namespace MVVMAccount
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs eventArgs)
        {
            var account = new Account {ShortName = "fredMarius"};
            var vm = new MainWindowViewModel(account);
            var window = new MainWindow(vm);
            window.Show();
        }
    }
}
