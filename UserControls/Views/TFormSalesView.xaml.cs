using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Views;

namespace WelldonePOS.UserControls.Views
{
    public partial class TFormSalesView : UserControl
    {
        #region ctor

        public TFormSalesView()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public TFormSalesPresenter Presenter {
            get {
                return this.DataContext as TFormSalesPresenter;
            }
        }

        #endregion

        #region Private Methods

        #region Handlers

        private void Select_Clicked(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;

            if (button != null)
                this.Presenter.SelectThis(button.DataContext as Item);

            this.filterText.Clear();
            this.filterResult.Visibility = Visibility.Collapsed;
        }

        private void Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Presenter.SelectUnit();
        }

        private void NumericKey_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            e.Handled = !validation.HasNoErrors(e.Key);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;

            this.Presenter.RemoveSalesHistory(button.DataContext as SalesHistory);
        }

        private void AddSalesHistory_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            if ((this.Presenter != null) && (this.Presenter.CurrentSalesHistory != null))
                e.CanExecute = ((validation.HasNoErrors(sender as DependencyObject)) && (validation.FindInvalid(this.Presenter.CurrentSalesHistory).Count == 0));
            else
                e.CanExecute = false;
        }

        private void AddSalesHistory_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!this.Presenter.AddSalesHistory())
                MessageBox.Show("Jumlah dijual melebihi jumlah barang tersedia. Pastikan Anda memasukkan data dengan benar sebelum melanjutkan proses.");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            bool x;
            bool y;
            bool z;

            if ((this.Presenter != null) && (this.Presenter.CurrentSalesTransaction != null))
            {
                MessageBox.Show("Oye");

                y = validation.FindInvalid(this.Presenter).Count == 0;
                z = validation.FindInvalid(this.Presenter.CurrentSalesTransaction).Count == 0;

                MessageBox.Show(string.Format("{0}", string.Join(", ", validation.FindInvalid(this.Presenter.CurrentSalesTransaction).ToArray())));

                x = y && z;
            }
            else
                x = false;

            MessageBox.Show(string.Format("x value is {0}", x.ToString()));
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            if ((this.Presenter != null) && (this.Presenter.CurrentSalesTransaction != null))
                e.CanExecute = (validation.FindInvalid(this.Presenter).Count == 0) && (validation.FindInvalid(this.Presenter.CurrentSalesTransaction).Count == 0);
            else
                e.CanExecute = false;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Kay!");

            //if (this.Presenter.Save())
            //{
            //    MessageBox.Show("Transaksi berhasil disimpan.");
            //}
        }

        private void Filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = e.OriginalSource as TextBox;
            ComboBox comboBoxFilter = (textBox.Parent as DockPanel).Children[0] as ComboBox;

            ScrollViewer scrollViewer = this.filterResult.Child as ScrollViewer;
            ItemsControl itemsControl = scrollViewer.Content as ItemsControl;

            if ((textBox != null) && (textBox.Text.Length > 0) && (comboBoxFilter != null) && (scrollViewer != null) && (itemsControl != null))
            {
                this.Presenter.Filter(comboBoxFilter.SelectedItem as string, textBox.Text);

                if (itemsControl.Items.Count > 0)
                    this.filterResult.Visibility = Visibility.Visible;
            }
            else
                this.filterResult.Visibility = Visibility.Collapsed;
        }

        private void CashPay_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = e.OriginalSource as TextBox;

            if ((textBox != null) && (textBox.Text.Length > 0))
                this.Presenter.UpdateChange(textBox.Text);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Presenter.Close();
        }

        #endregion

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion
    }
}
