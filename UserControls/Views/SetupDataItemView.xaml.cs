using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;
using WelldonePOS.Models.Enums;
using WelldonePOS.Presenters.Views;

namespace WelldonePOS.UserControls.Views
{
    public partial class SetupDataItemView : UserControl
    {
        #region ctor

        public SetupDataItemView()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public SetupDataItemPresenter Presenter {
            get {
                return this.DataContext as SetupDataItemPresenter;
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
                this.Presenter.SelectThis(checkBox.DataContext as Item);
        }

        private void Deselect_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = e.OriginalSource as CheckBox;

            if (checkBox != null)
                this.Presenter.DeselectThis(checkBox.DataContext as Item);
        }

        #endregion

        #region Save

        private void ReplaceCategory_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            if ((this.Presenter != null) && (this.Presenter.CurrentItemCollectionTemp != null))
                e.CanExecute = (this.Presenter.CurrentCategory != null);
            else
                e.CanExecute = false;
        }

        private void ReplaceCategory_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            for (int i = 0; i < this.Presenter.CurrentItemCollectionTemp.Count; i++)
            {
                this.Presenter.CurrentItemCollectionTemp[i].Group = this.Presenter.CurrentCategory;

                this.Presenter.Presenter.Save(this.Presenter.CurrentItemCollectionTemp[i]);
            }

            MessageBox.Show(string.Format("Proses migrasi barang ke dalam kategori [{0}] telah selesai.", this.Presenter.CurrentCategory.Name), "Proses Berhasil", MessageBoxButton.OK);

            this.CloseGroupDetailPanel();
            this.Presenter.ClearSelection();
        }

        private void SaveUnitOfSales_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            if ((this.Presenter != null) && (this.Presenter.CurrentUnitOfSalesTemp != null))
                e.CanExecute = ((validation.HasNoErrors(sender as DependencyObject)) && (validation.FindInvalid(this.Presenter.CurrentUnitOfSalesTemp).Count == 0));
            else
                e.CanExecute = false;
        }

        private void SaveUnitOfSales_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Presenter.SaveUnitOfSales())
            {
                MessageBox.Show(string.Format("Satuan jual [{0}] telah disimpan.", this.Presenter.CurrentUnitOfSales.Name), "Proses Berhasil", MessageBoxButton.OK);

                this.CloseUnitOfSalesDetailPanel();
            }
        }

        private void SaveUnitOfPurchase_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            if ((this.Presenter != null) && (this.Presenter.CurrentUnitOfPurchaseTemp != null))
                e.CanExecute = ((validation.HasNoErrors(sender as DependencyObject)) && (validation.FindInvalid(this.Presenter.CurrentUnitOfPurchaseTemp).Count == 0));
            else
                e.CanExecute = false;
        }

        private void SaveUnitOfPurchase_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Presenter.SaveUnitOfPurchase())
            {
                MessageBox.Show(string.Format("Satuan beli [{0}] telah disimpan.", this.Presenter.CurrentUnitOfPurchase.Name), "Proses Berhasil", MessageBoxButton.OK);

                this.CloseUnitOfPurchaseDetailPanel();
            }
        }

        private void SaveStock_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            if ((this.Presenter != null) && (this.Presenter.CurrentStockTemp != null))
                e.CanExecute = ((validation.HasNoErrors(sender as DependencyObject)) && (validation.FindInvalid(this.Presenter.CurrentStockTemp).Count == 0));
            else
                e.CanExecute = false;
        }

        private void SaveStock_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Presenter.SaveStock())
            {
                MessageBox.Show(string.Format("Persediaan dengan sumber [{0}] sejumlah [{1}] telah disimpan.", this.Presenter.CurrentStock.Origin.ToString(), this.Presenter.CurrentStock.InitialQty.ToString()), "Proses Berhasil", MessageBoxButton.OK);

                this.CloseStockDetailPanel();
            }
        }

        private void SaveItem_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            if ((this.Presenter != null) && (this.Presenter.CurrentItemTemp != null))
                e.CanExecute = ((validation.HasNoErrors(sender as DependencyObject)) && (validation.FindInvalid(this.Presenter.CurrentItemTemp).Count == 0));
            else
                e.CanExecute = false;
        }

        private void SaveItem_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Presenter.SaveItem())
            {
                MessageBox.Show(string.Format("Barang [{0}] dengan Kategori [{1}] telah disimpan.", this.Presenter.CurrentItem.Name, this.Presenter.CurrentItem.Group.Name), "Proses Berhasil", MessageBoxButton.OK);

                this.CloseItemDetailPanel();
                this.Presenter.ClearSelection();
            }
        }

        #endregion

        #region Delete

        private void DeleteUnitOfSales_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;

            if (button != null)
            {
                if (MessageBox.Show("Apakah Anda yakin ingin menghapus data satuan jual ini?", "Konfirmasi Proses", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    this.Presenter.DeleteUnitOfSales(button.DataContext as UnitOfSales);
            }
        }

        private void DeleteUnitOfPurchase_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;

            if (button != null)
            {
                if (MessageBox.Show("Apakah Anda yakin ingin menghapus data satuan beli ini?", "Konfirmasi Proses", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    this.Presenter.DeleteUnitOfPurchase(button.DataContext as UnitOfPurchase);
            }
        }

        private void DeleteStock_Click(object sender, RoutedEventArgs e)
        {
            if (this.Presenter.CurrentItem.Stocks.Count == 1)
            {
                if (this.Presenter.CurrentItem.Stocks[0].Origin == StockOrigin.InitialStock)
                {
                    if (this.Presenter.CurrentItem.Stocks[0].InitialQty == this.Presenter.CurrentItem.Stocks[0].CurrentQty)
                    {
                        if (MessageBox.Show(string.Format("Apakah Anda yakin ingin menghapus data saldo awal dari barang [{0}]?", this.Presenter.CurrentItem.Name), "Konfirmasi Proses", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            this.Presenter.DeleteStock();
                    }
                    else
                        MessageBox.Show("Saldo awal telah digunakan.", "Proses Gagal", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show("Tidak ada saldo awal untuk dihapus.", "Proses Gagal", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("Tidak ada saldo awal untuk dihapus.", "Proses Gagal", MessageBoxButton.OK);
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ELDA! Jangan lupa! Validasi FindEquals terhadap banyak transaksi");

            if (this.Presenter.CurrentItemCollectionTemp.Count > 0)
            {
                if (MessageBox.Show("Apakah Anda yakin ingin menghapus data barang yang sudah dipilih?", "Konfirmasi Proses", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    this.Presenter.DeleteItem();
            }
            else
                MessageBox.Show("Pastikan Anda telah memilih barang sebelum melanjutkan proses.", "Proses Gagal", MessageBoxButton.OK);

            this.Presenter.ClearSelection();
        }

        #endregion

        #region Open Detail

        private void AddUnitOfSales_Click(object sender, RoutedEventArgs e)
        {
            this.OpenUnitOfSalesDetailPanel();
        }

        private void AddUnitOfPurchase_Click(object sender, RoutedEventArgs e)
        {
            this.OpenUnitOfPurchaseDetailPanel();
        }

        private void AddStock_Click(object sender, RoutedEventArgs e)
        {
            if (this.Presenter.CurrentItem.Stocks.Count == 0)
                this.OpenStockDetailPanel();
            else
                MessageBox.Show("Pastikan persediaan masih kosong sebelum menambahkan saldo awal.", "Proses Gagal", MessageBoxButton.OK);
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            this.OpenItemDetailPanel();
        }

        private void GroupMigration_Click(object sender, RoutedEventArgs e)
        {
            if (this.Presenter.CurrentItemCollectionTemp.Count > 0)
                this.OpenGroupDetailPanel();
            else
                MessageBox.Show("Pastikan Anda memilih satu barang sebelum melanjutkan proses.", "Proses Gagal", MessageBoxButton.OK);
        }

        private void UnitOfSalesDetail_Click(object sender, RoutedEventArgs e)
        {
            this.OpenUnitOfSalesDetailPanel();

            Button button = e.OriginalSource as Button;

            if (button != null)
                this.Presenter.UnitOfSalesDetail(button.DataContext as UnitOfSales);
        }

        private void UnitOfPurchaseDetail_Click(object sender, RoutedEventArgs e)
        {
            this.OpenUnitOfPurchaseDetailPanel();

            Button button = e.OriginalSource as Button;

            if (button != null)
                this.Presenter.UnitOfPurchaseDetail(button.DataContext as UnitOfPurchase);
        }

        private void EditUnits_Click(object sender, RoutedEventArgs e)
        {
            if (this.Presenter.CurrentItemCollectionTemp.Count == 1)
            {
                this.OpenUnitsDetailPanel();
                this.Presenter.UnitsDetail();
            }
            else
            {
                MessageBox.Show("Pastikan Anda memilih satu barang sebelum melanjutkan proses.", "Proses Gagal", MessageBoxButton.OK);
                this.Presenter.ClearSelection();
            }
        }

        private void EditStocks_Click(object sender, RoutedEventArgs e)
        {
            if (this.Presenter.CurrentItemCollectionTemp.Count == 1)
            {
                this.OpenStocksDetailPanel();
                this.Presenter.StocksDetail();
            }
            else
            {
                MessageBox.Show("Pastikan Anda memilih satu barang sebelum melanjutkan proses.", "Proses Gagal", MessageBoxButton.OK);
                this.Presenter.ClearSelection();
            }
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (this.Presenter.CurrentItemCollectionTemp.Count == 1)
            {
                this.OpenItemDetailPanel();
                this.Presenter.ItemDetail();
            }
            else
            {
                MessageBox.Show("Pastikan Anda memilih satu barang sebelum melanjutkan proses.", "Proses Gagal", MessageBoxButton.OK);
                this.Presenter.ClearSelection();
            }
        }

        #endregion

        #region Close Detail

        private void CloseGroupDetail_Click(object sender, RoutedEventArgs e)
        {
            this.CloseGroupDetailPanel();
            this.Presenter.ClearSelection();
        }

        private void CloseUnitOfSalesDetail_Click(object sender, RoutedEventArgs e)
        {
            this.CloseUnitOfSalesDetailPanel();
        }

        private void CloseUnitOfPurchaseDetail_Click(object sender, RoutedEventArgs e)
        {
            this.CloseUnitOfPurchaseDetailPanel();
        }

        private void CloseUnitsDetail_Click(object sender, RoutedEventArgs e)
        {
            this.CloseUnitOfSalesDetailPanel();
            this.CloseUnitsDetailPanel();
            this.Presenter.ClearSelection();
        }

        private void CloseStockDetail_Click(object sender, RoutedEventArgs e)
        {
            this.CloseStockDetailPanel();
        }

        private void CloseStocksDetail_Click(object sender, RoutedEventArgs e)
        {
            this.CloseStockDetailPanel();
            this.CloseStocksDetailPanel();
            this.Presenter.ClearSelection();
        }

        private void CloseItemDetail_Click(object sender, RoutedEventArgs e)
        {
            this.CloseItemDetailPanel();
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

        private void QtyPerUnit_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = e.OriginalSource as TextBox;

            if ((textBox != null) && (!string.IsNullOrEmpty(textBox.Text)) && (int.Parse(textBox.Text) > 0))
                this.Presenter.UpdateStockPreview(textBox.Text);
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

        private void OpenGroupDetailPanel()
        {
            this.groupDetailPanel.Visibility = Visibility.Visible;
            this.Presenter.IsItemIdle = false;
        }

        private void OpenUnitOfSalesDetailPanel()
        {
            this.unitOfSalesDetailPanel.Visibility = Visibility.Visible;
            this.Presenter.IsUnitsIdle = false;
            this.Presenter.ClearUnitOfSalesDetail();
        }

        private void OpenUnitOfPurchaseDetailPanel()
        {
            this.unitOfPurchaseDetailPanel.Visibility = Visibility.Visible;
            this.Presenter.IsUnitsIdle = false;
            this.Presenter.ClearUnitOfPurchaseDetail();
        }

        private void OpenUnitsDetailPanel()
        {
            this.unitsDetailPanel.Visibility = Visibility.Visible;
            this.Presenter.IsItemIdle = false;
            this.Presenter.ClearUnitsDetail();
        }

        private void OpenStockDetailPanel()
        {
            this.stockDetailPanel.Visibility = Visibility.Visible;
            this.Presenter.IsStocksIdle = false;
            this.Presenter.ClearStockDetail();
        }

        private void OpenStocksDetailPanel()
        {
            this.stocksDetailPanel.Visibility = Visibility.Visible;
            this.Presenter.IsItemIdle = false;
            this.Presenter.ClearStocksDetail();
        }

        private void OpenItemDetailPanel()
        {
            this.itemDetailPanel.Visibility = Visibility.Visible;
            this.Presenter.IsItemIdle = false;
            this.Presenter.ClearItemDetail();
        }

        #endregion

        #region Close Detail Panel

        private void CloseGroupDetailPanel()
        {
            this.groupDetailPanel.Visibility = Visibility.Collapsed;
            this.Presenter.IsItemIdle = true;
            this.Presenter.ClearGroupDetail();
        }

        private void CloseUnitOfSalesDetailPanel()
        {
            this.unitOfSalesDetailPanel.Visibility = Visibility.Collapsed;
            this.Presenter.IsUnitsIdle = true;
            this.Presenter.ClearUnitOfSalesDetail();
        }

        private void CloseUnitOfPurchaseDetailPanel()
        {
            this.unitOfPurchaseDetailPanel.Visibility = Visibility.Collapsed;
            this.Presenter.IsUnitsIdle = true;
            this.Presenter.ClearUnitOfPurchaseDetail();
        }

        private void CloseUnitsDetailPanel()
        {
            this.unitsDetailPanel.Visibility = Visibility.Collapsed;
            this.Presenter.IsItemIdle = true;
            this.Presenter.ClearUnitsDetail();
        }

        private void CloseStockDetailPanel()
        {
            this.stockDetailPanel.Visibility = Visibility.Collapsed;
            this.Presenter.IsStocksIdle = true;
            this.Presenter.ClearStockDetail();
        }

        private void CloseStocksDetailPanel()
        {
            this.stocksDetailPanel.Visibility = Visibility.Collapsed;
            this.Presenter.IsItemIdle = true;
            this.Presenter.ClearStocksDetail();
        }

        private void CloseItemDetailPanel()
        {
            this.itemDetailPanel.Visibility = Visibility.Collapsed;
            this.Presenter.IsItemIdle = true;
            this.Presenter.ClearItemDetail();
        }

        #endregion

        #endregion

        #endregion
    }
}
