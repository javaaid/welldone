using System.Windows;
using System.Windows.Controls;

using WelldonePOS.Presenters.Shells;

namespace WelldonePOS.UserControls.Misc
{
    public partial class AppHeader : UserControl
    {
        #region ctor

        public AppHeader()
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

        private void MyProfile_Clicked(object sender, RoutedEventArgs e)
        {
            this.Presenter.SwapProfileBoxState();
        }

        #endregion
    }
}
