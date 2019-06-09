using System.Windows;
using WelldonePOS.Presenters.Shells;

namespace WelldonePOS
{
    public partial class MainShell : Window
    {
        #region

        public MainShell()
        {
            InitializeComponent();

            this.DataContext = new MainPresenter(this);
        }

        #endregion
    }
}
