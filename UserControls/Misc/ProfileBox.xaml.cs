using System.Windows;
using System.Windows.Controls;

using WelldonePOS.Presenters.Shells;

namespace WelldonePOS.UserControls.Misc
{
    public partial class ProfileBox : UserControl
    {
        #region ctor

        public ProfileBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public AppPresenter Presenter
        {
            get {
                return this.DataContext as AppPresenter;
            }
        }

        #endregion

        #region Handlers

        private void ManageProfile_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.SwapProfileBoxState();
            this.Presenter.DisplayManageProfile();
        }

        private void Logout_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.SwapProfileBoxState();

            if (MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Proses", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                this.Presenter.Logout();
        }

        #endregion
    }
}
