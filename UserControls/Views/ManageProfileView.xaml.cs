using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using WelldonePOS.Helpers;
using WelldonePOS.Presenters.Views;

namespace WelldonePOS.UserControls.Views
{
    public partial class ManageProfileView : UserControl
    {
        #region ctor

        public ManageProfileView()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public ManageProfilePresenter Presenter {
            get {
                return this.DataContext as ManageProfilePresenter;
            }
        }

        #endregion

        #region Private Methods

        #region Handlers

        #region Save

        private void Ok_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            if ((this.Presenter != null) && (this.Presenter.CurrentPasswordTemp != null))
            {
                validation.Reference = this.Presenter.CurrentUser.Password;

                e.CanExecute = ((validation.HasNoErrors(sender as DependencyObject)) && (validation.FindInvalid(this.Presenter.CurrentPasswordTemp).Count == 0));
            }
            else
                e.CanExecute = false;
        }

        private void Ok_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Presenter.ReplacePassword();
            this.CloseDetailPanel();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (this.Presenter.Save())
            {
                MessageBox.Show("Perubahan telah disimpan.", "Proses Berhasil", MessageBoxButton.OK);

                this.Presenter.Close();
            }
        }

        #endregion

        #region Open Detail

        private void EditPassword_Click(object sender, RoutedEventArgs e)
        {
            if (this.Presenter.IsIdle)
                this.OpenDetailPanel();
            else
                MessageBox.Show("Pastikan Anda menyelesaikan proses yang sedang berlangsung terlebih dahulu.", "Proses Gagal", MessageBoxButton.OK);
        }

        #endregion

        #region Close Detail

        private void CloseDetail_Click(object sender, RoutedEventArgs e)
        {
            this.CloseDetailPanel();
        }

        #endregion

        #region etc

        private void ConfirmOldPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = e.OriginalSource as TextBox;

            ValidationHelper validation = new ValidationHelper()
            {
                Type = "PasswordTemp",
                Property = "OldPassword",
                Reference = this.Presenter.CurrentUser.Password
            };

            Binding binding = BindingOperations.GetBinding(textBox, TextBox.TextProperty);

            binding.ValidationRules.Clear();
            binding.ValidationRules.Add(validation);
        }

        private void BrowsePhoto_Click(object sender, RoutedEventArgs e)
        {
            if (this.Presenter.IsIdle)
            {
                this.Presenter.IsIdle = false;
                this.Presenter.SelectPhoto();
            }
            else
                MessageBox.Show("Pastikan Anda menyelesaikan proses yang sedang berlangsung terlebih dahulu.", "Proses Gagal", MessageBoxButton.OK);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Presenter.Close();
        }

        #endregion

        #endregion

        #region etc

        #region Open Detail Panel

        private void OpenDetailPanel()
        {
            this.detailPanel.Visibility = Visibility.Visible;
            this.Presenter.IsIdle = false;
            this.Presenter.ClearDetail();
        }

        #endregion

        #region Close Detail Panel

        private void CloseDetailPanel()
        {
            this.detailPanel.Visibility = Visibility.Collapsed;
            this.Presenter.IsIdle = true;
            this.Presenter.ClearDetail();
        }

        #endregion

        #endregion

        #endregion

        #region Public Methods

        public string BrowsePhoto()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.ShowDialog();

            this.Presenter.IsIdle = true;

            return dialog.FileName;
        }

        #endregion
    }
}
