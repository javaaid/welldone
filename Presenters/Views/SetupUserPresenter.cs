using System.Collections.ObjectModel;

using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Shells;
using WelldonePOS.UserControls.Views;

namespace WelldonePOS.Presenters.Views
{
    public class SetupUserPresenter : PresenterBase<SetupUserView>
    {
        #region Private Fields

        private readonly AppPresenter _appPresenter;

        private bool _isIdle;

        private ObservableCollection<User> _currentUserCollection;
        private ObservableCollection<User> _currentUserCollectionTemp;
        private User _currentUser;
        private User _currentUserTemp;

        private ObservableCollection<UserRight> _currentUserRightCollection;

        #endregion

        #region ctor

        public SetupUserPresenter(AppPresenter appPresenter, SetupUserView view)
            : base(view, "TabHeader")
        {
            this._appPresenter = appPresenter;

            this._isIdle = true;

            this._currentUserCollection = appPresenter.CurrentUserCollection;
            this._currentUserCollectionTemp = new ObservableCollection<User>();
            this._currentUser = new User();
            this._currentUserTemp = null;

            this._currentUserRightCollection = new ObservableCollection<UserRight>(appPresenter.FindUserRight(true));
        }

        #endregion

        #region Public Properties

        public string TabHeader {
            get {
                return "Pengaturan User";
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

        public ObservableCollection<User> CurrentUserCollection {
            get {
                return this._currentUserCollection;
            }
            set {
                this._currentUserCollection = value;
                OnPropertyChanged("CurrentUserCollection");
            }
        }
        public ObservableCollection<User> CurrentUserCollectionTemp {
            get {
                return this._currentUserCollectionTemp;
            }
            set {
                this._currentUserCollectionTemp = value;
                OnPropertyChanged("CurrentUserCollectionTemp");
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

        public ObservableCollection<UserRight> CurrentUserRightCollection {
            get {
                return this._currentUserRightCollection;
            }
            set {
                this._currentUserRightCollection = value;
                OnPropertyChanged("CurrentUserRightCollection");
            }
        }

        #endregion

        #region Private Methods

        #region Fill Temp

        private void FillTemp(User user)
        {
            this.CurrentUserTemp = new User()
            {
                Id = user.Id,
                Name = user.Name,
                Accessibility = user.Accessibility,
                PhotoPath = user.PhotoPath,
                LoggingIn = user.LoggingIn,
                Status = user.Status
            };
        }

        #endregion

        #region Return Temp

        private void ReturnTemp()
        {
            this.CurrentUser = new User()
            {
                Id = this.CurrentUserTemp.Id,
                Name = this.CurrentUserTemp.Name,
                Password = this.CurrentUserTemp.Password,
                Accessibility = this.CurrentUserTemp.Accessibility,
                PhotoPath = this.CurrentUserTemp.PhotoPath,
                LoggingIn = this.CurrentUserTemp.LoggingIn,
                Status = this.CurrentUserTemp.Status
            };
        }

        #endregion

        #endregion

        #region Public Methods

        #region Select & Delete

        public void SelectThis(User user)
        {
            this.CurrentUserCollectionTemp.Add(user);
        }

        public void DeselectThis(User user)
        {
            for (int i = 0; i < this.CurrentUserCollectionTemp.Count; i++)
            {
                if (this.CurrentUserCollectionTemp[i].Equals(user))
                    this.CurrentUserCollectionTemp.Remove(user);
            }
        }

        #endregion

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

        #region Delete

        public void Delete()
        {
            for (int i = 0; i < this.CurrentUserCollectionTemp.Count; i++)
            {
                for (int j = 0; j < this.CurrentUserCollection.Count; j++)
                {
                    if (this.CurrentUserCollection[j].Equals(this.CurrentUserCollectionTemp[i]))
                    {
                        this.Presenter.Delete(this.CurrentUserCollection[j]);
                        break;
                    }
                }
            }
        }

        #endregion

        #region Detail

        public void Detail()
        {
            this.CurrentUser = this.CurrentUserCollectionTemp[0];
            this.FillTemp(this.CurrentUserCollectionTemp[0]);
        }

        #endregion

        #region Clear Detail

        public void ClearDetail()
        {
            this.CurrentUser = new User();
            this.CurrentUserTemp = new User();
        }

        #endregion

        #region etc

        public void ClearSelection()
        {
            this.CurrentUserCollectionTemp = new ObservableCollection<User>();
            this.Presenter.Reload("User");
        }

        public void Close()
        {
            this.Presenter.CloseTab(this);
            this.Presenter.Reload("User");
        }

        #endregion

        #endregion

        #region Equality

        public bool Equals(SetupUserPresenter other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (this.GetType() == other.GetType());
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as SetupUserPresenter);
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

        public static bool operator ==(SetupUserPresenter x, SetupUserPresenter y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(SetupUserPresenter x, SetupUserPresenter y)
        {
            return !(x == y);
        }

        #endregion
    }
}
