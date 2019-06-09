using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Shells;
using WelldonePOS.UserControls.Views;

namespace WelldonePOS.Presenters.Views
{
    public class SetupUserRightPresenter : PresenterBase<SetupUserRightView>
    {
        #region Private Methods

        private readonly AppPresenter _appPresenter;

        private bool _isIdle;

        private ObservableCollection<UserRight> _currentUserRightCollection;
        private ObservableCollection<UserRight> _currentUserRightCollectionTemp;
        private UserRight _currentUserRight;
        private UserRight _currentUserRightTemp;

        private readonly ObservableCollection<User> _currentUserCollection;

        #endregion

        #region ctor

        public SetupUserRightPresenter(AppPresenter appPresenter, SetupUserRightView view)
            : base(view, "TabHeader")
        {
            this._appPresenter = appPresenter;

            this._isIdle = true;

            this._currentUserRightCollection = appPresenter.CurrentUserRightCollection;
            this._currentUserRightCollectionTemp = new ObservableCollection<UserRight>();
            this._currentUserRight = new UserRight();
            this._currentUserRightTemp = null;

            this._currentUserCollection = appPresenter.CurrentUserCollection;
        }

        #endregion

        #region Public Properties

        public string TabHeader {
            get {
                return "Pengaturan Hak User";
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

        public ObservableCollection<UserRight> CurrentUserRightCollection {
            get {
                return this._currentUserRightCollection;
            }
            set {
                this._currentUserRightCollection = value;
                OnPropertyChanged("CurrentUserRightCollection");
            }
        }
        public ObservableCollection<UserRight> CurrentUserRightCollectionTemp {
            get {
                return this._currentUserRightCollectionTemp;
            }
            set {
                this._currentUserRightCollectionTemp = value;
                OnPropertyChanged("CurrentUserRightCollectionTemp");
            }
        }
        public UserRight CurrentUserRight {
            get {
                return this._currentUserRight;
            }
            set {
                this._currentUserRight = value;
                OnPropertyChanged("CurrentUserRight");
            }
        }
        public UserRight CurrentUserRightTemp {
            get {
                return this._currentUserRightTemp;
            }
            set {
                this._currentUserRightTemp = value;
                OnPropertyChanged("CurrentUserRightTemp");
            }
        }

        public ObservableCollection<User> CurrentUserCollection {
            get {
                return this._currentUserCollection;
            }
        }

        #endregion

        #region Private Methods

        #region Fill Temp

        private void FillTemp(UserRight userRight)
        {
            this.CurrentUserRightTemp = new UserRight()
            {
                Id = userRight.Id,
                Name = userRight.Name,
                Rights = userRight.Rights,
                Status = userRight.Status
            };
        }

        #endregion

        #region Return Temp

        private void ReturnTemp()
        {
            this.CurrentUserRight = new UserRight()
            {
                Id = this.CurrentUserRightTemp.Id,
                Name = this.CurrentUserRightTemp.Name,
                Rights = this.CurrentUserRightTemp.Rights,
                Status = this.CurrentUserRightTemp.Status
            };
        }

        #endregion

        #endregion

        #region Public Methods

        #region Select & Deselect

        public void SelectThis(UserRight userRight)
        {
            this.CurrentUserRightCollectionTemp.Add(userRight);
        }

        public void DeselectThis(UserRight userRight)
        {
            for (int i = 0; i < this.CurrentUserRightCollectionTemp.Count; i++)
            {
                if (this.CurrentUserRightCollectionTemp[i].Equals(userRight))
                    this.CurrentUserRightCollectionTemp.Remove(userRight);
            }
        }

        #endregion

        #region Save

        public bool Save()
        {
            bool isSaved = false;

            this.ReturnTemp();

            if (this.Presenter.Save(this.CurrentUserRight))
                isSaved = true;

            return isSaved;
        }

        #endregion

        #region Delete

        public void Delete()
        {
            for (int i = 0; i < this.CurrentUserRightCollectionTemp.Count; i++)
            {
                for (int j = 0; j < this.CurrentUserRightCollection.Count; j++)
                {
                    if (this.CurrentUserRightCollection[j].Equals(this.CurrentUserRightCollectionTemp[i]))
                    {
                        this.Presenter.Delete(this.CurrentUserRightCollection[j]);
                        break;
                    }
                }
            }
        }

        #endregion

        #region Detail

        public void Detail()
        {
            this.CurrentUserRight = this.CurrentUserRightCollectionTemp[0];
            this.FillTemp(this.CurrentUserRightCollectionTemp[0]);
        }

        #endregion

        #region Clear Detail

        public void ClearDetail()
        {
            this.CurrentUserRight = new UserRight();
            this.CurrentUserRightTemp = new UserRight();
        }

        #endregion

        #region etc

        public void SwitchModulState(string modulName, bool value)
        {
            switch (modulName)
            {
                case "modul_1":
                    this.CurrentUserRightTemp.Rights[0] = value;
                    break;
                case "modul_2":
                    this.CurrentUserRightTemp.Rights[11] = value;
                    break;
                case "modul_3":
                    this.CurrentUserRightTemp.Rights[27] = value;
                    break;
                case "modul_4":
                    this.CurrentUserRightTemp.Rights[44] = value;
                    break;
            }
        }

        public void ClearSelection()
        {
            this.CurrentUserRightCollectionTemp = new ObservableCollection<UserRight>();
            this.Presenter.Reload("UserRight");
        }

        public void Close()
        {
            this.Presenter.CloseTab(this);
            this.Presenter.Reload("UserRight");
        }

        #endregion

        #endregion

        #region Equality

        public bool Equals(SetupUserRightPresenter other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (this.GetType() == other.GetType());
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as SetupUserRightPresenter);
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

        public static bool operator ==(SetupUserRightPresenter x, SetupUserRightPresenter y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(SetupUserRightPresenter x, SetupUserRightPresenter y)
        {
            return !(x == y);
        }

        #endregion
    }
}
