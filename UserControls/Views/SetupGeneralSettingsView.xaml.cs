using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Views;

namespace WelldonePOS.UserControls.Views
{
    public partial class SetupGeneralSettingsView : UserControl
    {
        #region ctor

        public SetupGeneralSettingsView()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public SetupGeneralSettingsPresenter Presenter {
            get {
                return this.DataContext as SetupGeneralSettingsPresenter;
            }
        }

        #endregion

        #region Private Methods

        #region Handlers

        #region Save

        private void SaveCompanyProfile_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            if ((this.Presenter != null) && (this.Presenter.CurrentCompanyProfileTemp != null))
                e.CanExecute = ((validation.HasNoErrors(sender as DependencyObject)) && (validation.FindInvalid(this.Presenter.CurrentCompanyProfileTemp).Count == 0));
            else
                e.CanExecute = false;
        }

        private void SaveCompanyProfile_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Presenter.SaveCompanyProfile())
            {
                MessageBox.Show("Profil toko telah diperbarui.", "Proses Berhasil", MessageBoxButton.OK);

                this.CloseCompanyProfileDetailPanel();
            }
        }

        private void SaveCompanyPolicies_Click(object sender, RoutedEventArgs e)
        {
            if (this.Presenter.SaveCompanyPolicies())
            {
                MessageBox.Show("Kebijakan toko telah diperbarui.", "Proses Berhasil", MessageBoxButton.OK);

                this.CloseCompanyPoliciesDetailPanel();
            }
        }

        #endregion

        #region Open Detail

        private void EditCompanyProfile_Click(object sender, RoutedEventArgs e)
        {
            this.OpenCompanyProfileDetailPanel();
            this.Presenter.CompanyProfileDetail();
        }

        private void EditCompanyPolicies_Click(object sender, RoutedEventArgs e)
        {
            this.OpenCompanyPoliciesDetailPanel();
            this.Presenter.CompanyPoliciesDetail();
        }

        #endregion

        #region Close Detail

        private void CloseCompanyProfileDetail_Click(object sender, RoutedEventArgs e)
        {
            this.CloseCompanyProfileDetailPanel();
        }

        private void CloseCompanyPoliciesDetail_Click(object sender, RoutedEventArgs e)
        {
            this.CloseCompanyPoliciesDetailPanel();
        }

        #endregion

        #region etc

        private void NumericKey_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            e.Handled = !validation.HasNoErrors(e.Key);
        }

        private void BrowseLogo_Click(object sender, RoutedEventArgs e)
        {
            this.Presenter.SelectLogo();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Presenter.Close();
        }

        #endregion

        #endregion

        #region etc

        #region Open Detail Panel

        private void OpenCompanyProfileDetailPanel()
        {
            this.companyProfileDetailPanel.Visibility = Visibility.Visible;
            this.Presenter.IsIdle = false;
            this.Presenter.ClearCompanyProfileDetail();
        }

        private void OpenCompanyPoliciesDetailPanel()
        {
            this.companyPoliciesDetailPanel.Visibility = Visibility.Visible;
            this.Presenter.IsIdle = false;
            this.Presenter.ClearCompanyPoliciesDetail();
        }

        #endregion

        #region Close Detail Panel

        private void CloseCompanyProfileDetailPanel()
        {
            this.companyProfileDetailPanel.Visibility = Visibility.Collapsed;
            this.Presenter.IsIdle = true;
            this.Presenter.ClearCompanyProfileDetail();
        }

        private void CloseCompanyPoliciesDetailPanel()
        {
            this.companyPoliciesDetailPanel.Visibility = Visibility.Collapsed;
            this.Presenter.IsIdle = true;
            this.Presenter.ClearCompanyPoliciesDetail();
        }

        #endregion

        #endregion

        #endregion

        #region Public Methods

        public string BrowseLogo()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.ShowDialog();

            return dialog.FileName;
        }

        #endregion
    }
}
