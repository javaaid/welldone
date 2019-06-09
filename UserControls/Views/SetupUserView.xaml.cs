using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Views;

namespace WelldonePOS.UserControls.Views
{
    public partial class SetupUserView : UserControl
    {
        #region ctor

        public SetupUserView()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public SetupUserPresenter Presenter {
            get {
                return this.DataContext as SetupUserPresenter;
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
                this.Presenter.SelectThis(checkBox.DataContext as User);
        }

        private void Deselect_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = e.OriginalSource as CheckBox;

            if (checkBox != null)
                this.Presenter.DeselectThis(checkBox.DataContext as User);
        }

        #endregion

        #region Save

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            if ((this.Presenter != null) && (this.Presenter.CurrentUserTemp != null))
                e.CanExecute = ((validation.HasNoErrors(sender as DependencyObject)) && (validation.FindInvalid(this.Presenter.CurrentUserTemp).Count == 0));
            else
                e.CanExecute = false;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Presenter.Save())
            {
                MessageBox.Show(string.Format("User [{0}] dengan Level akses [{1}] telah disimpan.", this.Presenter.CurrentUser.Name, this.Presenter.CurrentUser.Accessibility.Name), "Proses Berhasil", MessageBoxButton.OK);

                this.CloseDetailPanel();
                this.Presenter.ClearSelection();
            }
        }

        #endregion

        #region Delete

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ELDA! Jangan lupa! Validasi FindEquals terhadap banyak transaksi");

            if (this.Presenter.CurrentUserCollectionTemp.Count > 0)
            {
                if (MessageBox.Show("Apakah Anda yakin ingin menghapus data user yang sudah dipilih?", "Konfirmasi Proses", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    this.Presenter.Delete();
            }
            else
                MessageBox.Show("Pastikan Anda telah memilih user sebelum melanjutkan proses.", "Proses Gagal", MessageBoxButton.OK);

            this.Presenter.ClearSelection();
        }

        #endregion

        #region Open Detail

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.OpenDetaiPanel();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (this.Presenter.CurrentUserCollectionTemp.Count == 1)
            {
                if (MessageBox.Show("Proses edit mengharuskan Anda mereset password user yang bersangkutan. Apakah Anda yakin ingin melanjutkan proses?", "Konfirmasi Proses", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.OpenDetaiPanel();
                    this.Presenter.Detail();
                }
            }
            else
            {
                MessageBox.Show("Pastikan Anda memilih satu user sebelum melanjutkan proses.", "Proses Gagal", MessageBoxButton.OK);
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

        private void OpenDetaiPanel()
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
