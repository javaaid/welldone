using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Views;

namespace WelldonePOS.UserControls.Views
{
    public partial class SetupDataCustomerView : UserControl
    {
        #region ctor

        public SetupDataCustomerView()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public SetupDataCustomerPresenter Presenter {
            get {
                return this.DataContext as SetupDataCustomerPresenter;
            }
        }

        #endregion

        #region Private Methods

        #region Handlers

        #region Select & Deselect

        private void Select_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = e.OriginalSource as CheckBox;

            if (checkBox != null)
                this.Presenter.SelectThis(checkBox.DataContext as Customer);
        }

        private void Deselect_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = e.OriginalSource as CheckBox;

            if (checkBox != null)
                this.Presenter.DeselectThis(checkBox.DataContext as Customer);
        }

        #endregion

        #region Save

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            if ((this.Presenter != null) && (this.Presenter.CurrentCustomerTemp != null))
                e.CanExecute = (validation.HasNoErrors(sender as DependencyObject) && (validation.FindInvalid(this.Presenter.CurrentCustomerTemp).Count == 0));
            else
                e.CanExecute = false;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Presenter.Save())
            {
                MessageBox.Show(string.Format("Customer dengan nama [{0}] telah disimpan.", this.Presenter.CurrentCustomer.Name), "Proses Berhasil", MessageBoxButton.OK);

                this.CloseDetailPanel();
                this.Presenter.ClearSelection();
            }
        }

        #endregion

        #region Delete

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ELDA! Jangan lupa! Validasi FindEquals terhadap banyak transaksi");

            if (this.Presenter.CurrentCustomerCollectionTemp.Count > 0)
            {
                if (MessageBox.Show("Apakah Anda yakin ingin menghapus data customer yang sudah dipilih?", "Konfirmasi Proses", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    this.Presenter.Delete();
            }
            else
                MessageBox.Show("Pastikan Anda telah memilih customer sebelum melanjutkan proses.", "Proses Gagal", MessageBoxButton.OK);

            this.Presenter.ClearSelection();
        }

        #endregion

        #region Open Detail

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.OpenDetailPanel();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (this.Presenter.CurrentCustomerCollectionTemp.Count == 1)
            {
                this.OpenDetailPanel();
                this.Presenter.Detail();
            }
            else
            {
                MessageBox.Show("Pastikan Anda memilih satu customer sebelum melanjutkan proses.", "Proses Gagal", MessageBoxButton.OK);
                this.Presenter.ClearSelection();
            }
        }

        #endregion

        #region Close Detail

        private void CloseDetail_Click(object sender, RoutedEventArgs e)
        {
            this.CloseDetailPanel();
            this.Presenter.ClearSelection();
        }

        #endregion

        #region etc

        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = e.OriginalSource as TextBox;
            ComboBox comboBoxFilter = (textBox.Parent as StackPanel).Children[1] as ComboBox;
            ComboBox comboBoxStatus = (textBox.Parent as StackPanel).Children[2] as ComboBox;

            if ((textBox != null) && (comboBoxFilter != null) && (comboBoxStatus != null))
                this.Presenter.Filter(comboBoxFilter.SelectedItem as string, comboBoxStatus.SelectedItem as string, textBox.Text);
        }

        private void NumericKey_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            e.Handled = !validation.HasNoErrors(e.Key);
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
    }
}
