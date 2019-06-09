using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows;

using WelldonePOS.Models.Repositories;
using WelldonePOS.Presenters;
using WelldonePOS.Presenters.Shells;

namespace WelldonePOS.UserControls.Shells
{
    public partial class AppShell : UserControl
    {
        #region ctor

        public AppShell()
        {
            InitializeComponent();

            this.DataContext = new AppPresenter(this, new CategoryRepository(), new CustomerRepository(), new GeneralSettingsRepository(), new ItemRepository(), new SalesTransactionRepository(), new SupplierRepository(), new UserRepository(), new UserRightRepository());

            this.Presenter.StatusText = string.Format("Selamat datang {0}, selamat beraktivitas.", this.Presenter.CurrentLogin.Name);
            
            this.RunTimer();
        }

        #endregion

        #region Public Properties

        public AppPresenter Presenter {
            get {
                return this.DataContext as AppPresenter;
            }
        }

        #endregion

        #region Private Methods

        private void RunTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromSeconds(1);

            timer.Tick += Timer_Tick;
            timer.Start();
        }

        #endregion

        #region Public Methods

        public void AddTab<T>(PresenterBase<T> presenter)
        {
            TabItem newTabItem = null;

            for (int i = 0; i < this.contentTabs.Items.Count; i++)
            {
                TabItem existingTabItem = (TabItem)this.contentTabs.Items[i];

                if (existingTabItem.DataContext.Equals(presenter))
                {
                    this.contentTabs.Items.Remove(existingTabItem);
                    newTabItem = existingTabItem;
                    break;
                }
            }

            if (newTabItem == null)
            {
                newTabItem = new TabItem();

                HeaderedContentControl headerContainer = new HeaderedContentControl()
                {
                    Style = FindResource("hccTabHeader") as Style
                };

                Binding headerBinding = new Binding(presenter.TabHeaderPath);
                BindingOperations.SetBinding(headerContainer, HeaderedContentControl.ContentProperty, headerBinding);

                newTabItem.Header = headerContainer;

                newTabItem.DataContext = presenter;
                newTabItem.Content = presenter.View;
            }

            this.contentTabs.Items.Insert(0, newTabItem);
            newTabItem.Focus();
        }

        public void RemoveTab<T>(PresenterBase<T> presenter)
        {
            for (int i = 0; i < this.contentTabs.Items.Count; i++)
            {
                TabItem tabItem = (TabItem)this.contentTabs.Items[i];

                if (tabItem.DataContext.Equals(presenter))
                {
                    this.contentTabs.Items.Remove(tabItem);
                    break;
                }
            }
        }

        public void NavigateToLogin()
        {
            NavigationService navigation = NavigationService.GetNavigationService(this);

            navigation.Navigate(new Uri(@"\UserControls\Shells\LoginShell.xaml", UriKind.Relative));
        }

        #endregion

        #region Handlers

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Presenter.CurrentDateTime = DateTime.Now;
        }

        #endregion
    }
}
