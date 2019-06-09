using WelldonePOS.Models.DataModels;
using WelldonePOS.Models.Misc;
using WelldonePOS.Presenters.Shells;
using WelldonePOS.UserControls.Views;

namespace WelldonePOS.Presenters.Views
{
    public class ManageProfilePresenter : PresenterBase<ManageProfileView>
    {
        #region Private Fields

        private readonly AppPresenter _appPresenter;

        private bool _isIdle;

        private User _currentUser;
        private User _currentUserTemp;

        private PasswordTemp _currentPasswordTemp;

        #endregion

        #region ctor

        public ManageProfilePresenter(AppPresenter appPresenter, ManageProfileView view)
            : base(view, "TabHeader")
        {
            this._appPresenter = appPresenter;

            this._isIdle = true;

            this._currentUser = appPresenter.CurrentLogin;
            this._currentUserTemp = new User()
            {
                Id = appPresenter.CurrentLogin.Id,
                Name = appPresenter.CurrentLogin.Name,
                Password = appPresenter.CurrentLogin.Password,
                Accessibility = appPresenter.CurrentLogin.Accessibility,
                PhotoPath = appPresenter.CurrentLogin.PhotoPath,
                Status = appPresenter.CurrentLogin.Status
            };

            this._currentPasswordTemp = null;
        }

        #endregion

        #region Public Properties

        public string TabHeader {
            get {
                return "Pengaturan Profil";
            }
        }

        public AppPresenter Presenter {
            get {
                return this._appPresenter;
            }
        }

        public bool IsIdle {
            get {
                return this._isIdle;
            }
            set {
                this._isIdle = value;
                OnPropertyChanged("IsIdle");
            }
        }

        public User CurrentUser {
            get {
                return this._currentUser;
            }
            set {
                this._currentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }
        public User CurrentUserTemp {
            get {
                return this._currentUserTemp;
            }
            set {
                this._currentUserTemp = value;
                OnPropertyChanged("CurrentUserTemp");
            }
        }

        public PasswordTemp CurrentPasswordTemp {
            get {
                return this._currentPasswordTemp;
            }
            set {
                this._currentPasswordTemp = value;
                OnPropertyChanged("CurrentPasswordTemp");
            }
        }

        #endregion

        #region Private Methods

        #region Return Temp

        private void ReturnTemp()
        {
            this.CurrentUser.Password = this.CurrentUserTemp.Password;
            this.CurrentUser.PhotoPath = this.CurrentUserTemp.PhotoPath;
        }

        #endregion

        #endregion

        #region Public Methods

        #region Save

        public bool Save()
        {
            bool isSaved = false;

            this.ReturnTemp();

            if (this.Presenter.Save(this.CurrentUser))
                isSaved = true;

            return isSaved;
        }

        #endregion

        #region Clear Detail

        public void ClearDetail()
        {
            this.CurrentPasswordTemp = new PasswordTemp();
        }

        #endregion

        #region etc

        public void ReplacePassword()
        {
            this.CurrentUserTemp.Password = this.CurrentPasswordTemp.NewPassword;
        }

        public void SelectPhoto()
        {
            string photoPath = View.BrowsePhoto();

            if (!string.IsNullOrEmpty(photoPath))
                this.CurrentUserTemp.PhotoPath = photoPath;
        }

        public void Close()
        {
            this.Presenter.CloseTab(this);
        }

        #endregion

        #endregion

        #region Equality

        public bool Equals(ManageProfilePresenter other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (this.GetType() == other.GetType());
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ManageProfilePresenter);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                int hash = hashingBase;

                hash = (hash * hashingMultiplier) ^ (!object.ReferenceEquals(null, this.GetType()) ? this.GetType().GetHashCode() : 0);

                return hash;
            }
        }

        public static bool operator ==(ManageProfilePresenter x, ManageProfilePresenter y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(ManageProfilePresenter x, ManageProfilePresenter y)
        {
            return !(x == y);
        }

        #endregion
    }
}
