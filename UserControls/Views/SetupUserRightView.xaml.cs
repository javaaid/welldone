using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Views;

namespace WelldonePOS.UserControls.Views
{
    public partial class SetupUserRightView : UserControl
    {
        #region ctor

        public SetupUserRightView()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public SetupUserRightPresenter Presenter {
            get {
                return this.DataContext as SetupUserRightPresenter;
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
                this.Presenter.SelectThis(checkBox.DataContext as UserRight);
        }

        private void Deselect_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = e.OriginalSource as CheckBox;

            if (checkBox != null)
                this.Presenter.DeselectThis(checkBox.DataContext as UserRight);
        }

        #endregion

        #region Save

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ValidationHelper validation = new ValidationHelper();

            if ((this.Presenter != null) && (this.Presenter.CurrentUserRightTemp != null))
                e.CanExecute = (validation.HasNoErrors(sender as DependencyObject) && (validation.FindInvalid(this.Presenter.CurrentUserRightTemp).Count == 0));
            else
                e.CanExecute = false;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Presenter.Save())
            {
                MessageBox.Show(string.Format("Hak user [{0}] telah disimpan.", this.Presenter.CurrentUserRight.Name), "Proses Berhasil", MessageBoxButton.OK);

                this.CloseDetailPanel();
                this.Presenter.ClearSelection();
            }
        }

        #endregion

        #region Delete

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (this.Presenter.CurrentUserRightCollectionTemp.Count > 0)
            {
                ValidationHelper validation = new ValidationHelper();

                List<string> referenceList = validation.FindReference(this.Presenter.CurrentUserRightCollectionTemp, this.Presenter.CurrentUserCollection);

                if (referenceList.Count == 0)
                {
                    if (MessageBox.Show("Apakah Anda yakin ingin menghapus data hak user yang sudah dipilih?", "Konfirmasi Proses", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        this.Presenter.Delete();
                }
                else
                {
                    MessageBox.Show("Pastikan Anda memilih hak user dengan benar sebelum melanjutkan proses.\n\n * Masih terdapat hak user yang terkait dengan satu atau lebih akun user.", "Proses Gagal", MessageBoxButton.OK);
                    return;
                }
            }
            else
                MessageBox.Show("Pastikan Anda telah memilih hak user sebelum melanjutkan proses.", "Proses Gagal", MessageBoxButton.OK);

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
            if (this.Presenter.CurrentUserRightCollectionTemp.Count == 1)
            {
                this.OpenDetaiPanel();
                this.Presenter.Detail();
            }
            else
            {
                MessageBox.Show("Pastikan Anda memilih satu hak user sebelum melanjutkan proses.", "Proses Gagal", MessageBoxButton.OK);
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

        private void Modul_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = e.OriginalSource as CheckBox;

            bool checkValue = (checkBox.IsChecked == true);

            this.SynchronizeActions(checkBox, checkValue);
        }

        private void View_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = e.OriginalSource as CheckBox;
            Grid grid = checkBox.Parent as Grid;

            if (checkBox.IsChecked == true)
            {
                for (int column = 2; column < grid.ColumnDefinitions.Count; column++)
                {
                    if (this.GetGridElement((grid), column, Grid.GetRow(checkBox)) as CheckBox != null)
                        (this.GetGridElement(grid, column, Grid.GetRow(checkBox)) as CheckBox).IsEnabled = true;
                }
            }
            else
            {
                for (int column = 2; column < grid.ColumnDefinitions.Count; column++)
                {
                    if (this.GetGridElement((grid), column, Grid.GetRow(checkBox)) as CheckBox != null)
                        (this.GetGridElement(grid, column, Grid.GetRow(checkBox)) as CheckBox).IsEnabled = false;
                }
            }

            this.CheckModulState(checkBox);
        }

        private void Action_CheckedChanged(object sender, RoutedEventArgs e)
        {
            bool isActionsEqual = false;
            CheckBox checkBox = e.OriginalSource as CheckBox;
            Grid grid = checkBox.Parent as Grid;

            for (int column = 2; column < grid.ColumnDefinitions.Count; column++)
            {
                if (this.GetGridElement(grid, column, Grid.GetRow(checkBox)) as CheckBox != null)
                    isActionsEqual = isActionsEqual || ((this.GetGridElement(grid, column, Grid.GetRow(checkBox)) as CheckBox).IsChecked == true);
            }

            if (isActionsEqual == true)
                (this.GetGridElement(grid, 1, Grid.GetRow(checkBox)) as CheckBox).IsEnabled = false;
            else
                (this.GetGridElement(grid, 1, Grid.GetRow(checkBox)) as CheckBox).IsEnabled = true;

            this.CheckModulState(checkBox);
        }

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

        #region Misc

        private UIElement GetGridElement(Grid grid, int column, int row)
        {
            for (int i = 0; i < grid.Children.Count; i++)
            {
                UIElement e = grid.Children[i];

                if ((Grid.GetColumn(e) == column) && (Grid.GetRow(e) == row))
                    return e;
            }

            return null;
        }

        private void SynchronizeActions(CheckBox checkBox, bool checkValue)
        {
            Grid grid = checkBox.Parent as Grid;
            List<int> modulRows = new List<int>();
            int currentRow = Grid.GetRow(checkBox);
            int nextRow = 0;

            for (int row = 0; row < grid.RowDefinitions.Count; row++)
            {
                if ((this.GetGridElement(grid, 1, row) as CheckBox) == null)
                    modulRows.Add(row);
            }

            for (int i = 0; i < modulRows.Count; i++)
            {
                if (modulRows[i] == currentRow)
                {
                    if (modulRows[i] != modulRows[modulRows.Count - 1])
                    {
                        nextRow = modulRows[i + 1];
                        break;
                    }
                    else
                    {
                        nextRow = grid.RowDefinitions.Count;
                        break;
                    }

                }
            }

            for (int column = 1; column < grid.ColumnDefinitions.Count; column++)
            {
                for (int row = currentRow + 1; row < nextRow; row++)
                {
                    if (this.GetGridElement(grid, column, row) as CheckBox != null)
                        (this.GetGridElement(grid, column, row) as CheckBox).IsChecked = checkValue;
                }
            }
        }

        private void CheckModulState(CheckBox checkBox)
        {
            bool isActionsEqual = true;
            bool modulState = true;
            Grid grid = checkBox.Parent as Grid;
            List<int> modulRows = new List<int>();
            int currentRow = Grid.GetRow(checkBox);
            int nextRow = 0;

            for (int row = currentRow; row > -1; row--)
            {
                if ((this.GetGridElement(grid, 1, row) as CheckBox) == null)
                {
                    currentRow = row;
                    break;
                }
            }

            for (int row = 0; row < grid.RowDefinitions.Count; row++)
            {
                if ((this.GetGridElement(grid, 1, row) as CheckBox) == null)
                    modulRows.Add(row);
            }

            for (int i = 0; i < modulRows.Count; i++)
            {
                if (modulRows[i] == currentRow)
                {
                    if (modulRows[i] != modulRows[modulRows.Count - 1])
                    {
                        nextRow = modulRows[i + 1];
                        break;
                    }
                    else
                    {
                        nextRow = grid.RowDefinitions.Count;
                        break;
                    }

                }
            }

            (this.GetGridElement(grid, 0, currentRow) as CheckBox).IsChecked = null;

            for (int column = 1; column < grid.ColumnDefinitions.Count; column++)
            {
                for (int row = currentRow + 1; row < nextRow; row++)
                {
                    if (this.GetGridElement(grid, column, row) as CheckBox != null)
                        isActionsEqual = isActionsEqual && ((this.GetGridElement(grid, column, row) as CheckBox).IsChecked == true);
                }
            }

            if (isActionsEqual == true)
                (this.GetGridElement(grid, 0, currentRow) as CheckBox).IsChecked = true;
            else
                isActionsEqual = true;

            for (int column = 1; column < grid.ColumnDefinitions.Count; column++)
            {
                for (int row = currentRow + 1; row < nextRow; row++)
                {
                    if (this.GetGridElement(grid, column, row) as CheckBox != null)
                        isActionsEqual = isActionsEqual && ((this.GetGridElement(grid, column, row) as CheckBox).IsChecked == false);
                }
            }

            if (isActionsEqual == true)
            {
                (this.GetGridElement(grid, 0, currentRow) as CheckBox).IsChecked = false;
                modulState = false;
            }

            this.Presenter.SwitchModulState((this.GetGridElement(grid, 0, currentRow) as CheckBox).Name, modulState);
        }

        #endregion

        #endregion

        #endregion
    }
}
