using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TyrionBanker.FrontUI.Views;
using Microsoft.Practices.Unity;

namespace TyrionBanker.FrontUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var login = UnityConfig.Resolve<Login>();
            login.ShowDialog();
        }
    }

    
}
