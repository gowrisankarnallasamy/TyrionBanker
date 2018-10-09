using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TyrionBanker.FrontUI.ViewModels;

namespace TyrionBanker.FrontUI.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window, IHavePassword
    {
        public Login(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                TextBoxLoginUserid.Focus();
            };
            DataContext = loginViewModel;
        }

        public string Password
        {
            get
            {
                return PasswordBoxLoginPassward.Password;
            }
        }
    }
}
