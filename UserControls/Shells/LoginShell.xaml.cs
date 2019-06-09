using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using WelldonePOS.Models.Repositories;
using WelldonePOS.Presenters.Shells;

namespace WelldonePOS.UserControls.Shells
{
    public partial class LoginShell : UserControl
    {
        #region ctor

        public LoginShell()
        {
            InitializeComponent();

            this.DataContext = new LoginPresenter(this, new UserRepository());
        }

        #endregion

        #region Public Methods

        public void NavigateToApp()
        {
            NavigationService navigation = NavigationService.GetNavigationService(this);

            navigation.Navigate(new Uri(@"\UserControls\Shells\AppShell.xaml", UriKind.Relative));
        }

        #endregion

    }
}
