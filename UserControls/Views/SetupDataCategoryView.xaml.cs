using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Views;

namespace WelldonePOS.UserControls.Views
{
    public partial class SetupDataCategoryView : UserControl
    {
        #region ctor

        public SetupDataCategoryView()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public SetupDataCategoryPresenter Presenter {
            get {
                return this.DataContext as SetupDataCategoryPresenter;
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
                this.Presenter.SelectThis(checkBox.DataContext as Category);
        }

        private void Deselect_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = e.OriginalSource as CheckBox;

            if (checkBox != null)
                this.Presenter.DeselectThis(checkBox.DataContext as Category);
        }

        #endregion

        #region Save

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            if ((this.Presenter != null) && (this.Presenter.CurrentCategoryTemp != null))
                e.CanExecute = (validation.HasNoErrors(sender as DependencyObject) && (validation.FindInvalid(this.Presenter.CurrentCategoryTemp).Count == 0));
            else
                e.CanExecute = false;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Presenter.Save())
            {
                MessageBox.Show(string.Format("Kategori [{0}] telah disimpan.", this.Presenter.CurrentCategory.Name), "Proses Berhasil", MessageBoxButton.OK);

                this.CloseDetailPanel();
                this.Presenter.ClearSelection();
            }
        }

        #endregion

        #region Delete

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (this.Presenter.CurrentCategoryCollectionTemp.Count > 0)
            {
                ValidationHelper validation = new ValidationHelper();

                List<string> referenceList = validation.FindReference(this.Presenter.CurrentCategoryCollectionTemp, this.Presenter.CurrentItemCollection);

                if (referenceList.Count == 0)
                {
                    if (MessageBox.Show("Apakah Anda yakin ingin menghapus data kategori yang sudah dipilih?", "Konfirmasi Proses", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        this.Presenter.Delete();
                }
                else
                {
                    MessageBox.Show("Pastikan Anda memilih kategori dengan benar sebelum melanjutkan proses.\n\n * Masih terdapat kategori yang terkait dengan satu atau lebih data barang.", "Proses Gagal", MessageBoxButton.OK);
                    return;
                }
            }
            else
                MessageBox.Show("Pastikan Anda telah memilih kategori sebelum melanjutkan proses.", "Proses Gagal", MessageBoxButton.OK);

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
            if (this.Presenter.CurrentCategoryCollectionTemp.Count == 1)
            {
                this.OpenDetailPanel();
                this.Presenter.Detail();
            }
            else
            {
                MessageBox.Show("Pastikan Anda memilih satu kategori sebelum melanjutkan proses.", "Proses Gagal", MessageBoxButton.OK);
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
