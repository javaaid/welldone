using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Views;

namespace WelldonePOS.UserControls.Views
{
    public partial class TFormReceiptView : UserControl
    {
        #region ctor

        public TFormReceiptView()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Properties

        public TFormReceiptPresenter Presenter {
            get {
                return this.DataContext as TFormReceiptPresenter;
            }
        }

        #endregion

        #region Private Methods

        #region Handlers
        #endregion

        #endregion
    }
}
