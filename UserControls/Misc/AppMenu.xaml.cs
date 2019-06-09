using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using WelldonePOS.Presenters.Shells;

namespace WelldonePOS.UserControls.Misc
{
    public partial class AppMenu : UserControl
    {
        #region ctor

        public AppMenu()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public AppPresenter Presenter {
            get {
                return this.DataContext as AppPresenter;
            }
        }

        #endregion

        #region Handlers

        private void ManageProfile_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.DisplayManageProfile();
        }

        private void TFormSales_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.DisplayTFormSales();
        }

        private void TFormReceipt_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.DisplayTFormReceipt();
        }

        private void TFormPurchase_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.DisplayTFormPurchase();
        }

        private void SetupDataItem_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.DisplaySetupDataItem();
        }

        private void SetupDataCustomer_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.DisplaySetupDataCustomer();
        }

        private void SetupDataSupplier_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.DisplaySetupDataSupplier();
        }

        private void SetupDataCategory_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.DisplaySetupDataCategory();
        }

        private void SetupGeneralSettings_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.DisplaySetupGeneralSettings();
        }

        private void SetupUser_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.DisplaySetupUser();
        }

        private void SetupUserRight_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.DisplaySetupUserRight();
        }

        #endregion
    }
}
