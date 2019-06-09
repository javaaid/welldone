using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Shells;

namespace WelldonePOS.UserControls.Misc
{
    public partial class LoginBox : UserControl
    {
        #region ctor

        public LoginBox()
        {
            InitializeComponent();

            this.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.LoginBox_IsVisibleChanged);
        }

        #endregion

        #region Public Properties

        public LoginPresenter Presenter {
            get {
                return this.DataContext as LoginPresenter;
            }
        }

        #endregion

        #region Handlers

        private void LoginBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(delegate()
                    {
                        this.passBox.Focus();
                    }));
            }
        }

        private void Detail_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = e.OriginalSource as ComboBox;

            if (comboBox != null)
            {
                this.Presenter.SelectThis(comboBox.SelectedItem as User);

                Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(delegate()
                {
                    this.passBox.Focus();
                }));
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            Button loginButton = (this.passBox.Parent as StackPanel).Children[4] as Button;

            if (loginButton != null)
            {
                if (e.Key == Key.Enter)
                    this.Presenter.Login();
            }
        }

        private void Password_Changed(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = e.OriginalSource as PasswordBox;

            if (passwordBox != null)
                this.Presenter.CurrentUserTemp.Password = passwordBox.Password;
        }

        private void Login_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.Login();
        }

        private void Shutdown_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.Shutdown();
        }

        #endregion
    }
}
