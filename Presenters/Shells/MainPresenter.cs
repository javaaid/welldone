using System.Collections.Generic;
using System.Linq;

using WelldonePOS.Models.DataModels;

namespace WelldonePOS.Presenters.Shells
{
    public class MainPresenter : PresenterBase<MainShell>
    {
        #region Private Fields

        private User _currentLogin;

        #endregion

        #region ctor

        public MainPresenter(MainShell view)
            : base(view)
        {
            this._currentLogin = new User();
        }

        #endregion

        #region Public Properties

        public User CurrentLogin {
            get {
                return this._currentLogin;
            }
            set {
                this._currentLogin = value;
            }
        }

        #endregion

        #region Public Methods

        public List<int> FindRestrictedRights()
        {
            IEnumerable<int> found = from pair in this.CurrentLogin.Accessibility.Rights
                                     where (pair.Value == false)
                                     select pair.Key;

            return found.ToList();
        }

        #endregion
    }
}
