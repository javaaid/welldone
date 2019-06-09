using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Views;

namespace WelldonePOS.UserControls.Views
{
    public partial class TFormPurchaseView : UserControl
    {
        #region ctor

        public TFormPurchaseView()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public TFormPurchasePresenter Presenter {
            get {
                return this.DataContext as TFormPurchasePresenter;
            }
        }

        #endregion

        #region Private Methods

        #region Handlers
        #endregion

        #endregion
    }
}
