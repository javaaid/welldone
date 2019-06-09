using WelldonePOS.Helpers;

namespace WelldonePOS.Presenters
{
    public class PresenterBase<T> : Notifier
    {
        #region Private Fields

        private readonly T _view;
        private readonly string _tabHeaderPath;

        #endregion

        #region ctor

        public PresenterBase(T view)
        {
            this._view = view;
        }

        public PresenterBase(T view, string tabHeaderPath)
        {
            this._view = view;
            this._tabHeaderPath = tabHeaderPath;
        }

        #endregion

        #region Public Properties

        public T View {
            get {
                return this._view;
            }
        }
        public string TabHeaderPath {
            get {
                return this._tabHeaderPath;
            }
        }

        #endregion
    }
}
