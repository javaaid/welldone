using System.Collections.ObjectModel;

using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Shells;
using WelldonePOS.UserControls.Views;

namespace WelldonePOS.Presenters.Views
{
    public class SetupDataSupplierPresenter : PresenterBase<SetupDataSupplierView>
    {
        #region Private Fields

        private readonly AppPresenter _appPresenter;

        private bool _isIdle;

        private ObservableCollection<Supplier> _currentSupplierCollection;
        private ObservableCollection<Supplier> _currentSupplierCollectionTemp;
        private Supplier _currentSupplier;
        private Supplier _currentSupplierTemp;

        #endregion

        #region ctor

        public SetupDataSupplierPresenter(AppPresenter appPresenter, SetupDataSupplierView view)
            : base(view, "TabHeader")
        {
            this._appPresenter = appPresenter;

            this._isIdle = true;

            this._currentSupplierCollection = appPresenter.CurrentSupplierCollection;
            this._currentSupplierCollectionTemp = new ObservableCollection<Supplier>();
            this._currentSupplier = new Supplier();
            this._currentSupplierTemp = null;
        }

        #endregion

        #region Public Properties

        public string TabHeader {
            get {
                return "Setup Data Supplier";
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

        public ObservableCollection<Supplier> CurrentSupplierCollection {
            get {
                return this._currentSupplierCollection;
            }
            set {
                this._currentSupplierCollection = value;
                OnPropertyChanged("CurrentSupplierCollection");
            }
        }
        public ObservableCollection<Supplier> CurrentSupplierCollectionTemp {
            get {
                return this._currentSupplierCollectionTemp;
            }
            set {
                this._currentSupplierCollectionTemp = value;
                OnPropertyChanged("CurrentSupplierCollectionTemp");
            }
        }
        public Supplier CurrentSupplier {
            get {
                return this._currentSupplier;
            }
            set {
                this._currentSupplier = value;
                OnPropertyChanged("CurrentSupplier");
            }
        }
        public Supplier CurrentSupplierTemp {
            get {
                return this._currentSupplierTemp;
            }
            set {
                this._currentSupplierTemp = value;
                OnPropertyChanged("CurrentSupplierTemp");
            }
        }

        #endregion

        #region Private Methods

        #region Fill Temp

        private void FillTemp(Supplier supplier)
        {
            this.CurrentSupplierTemp = new Supplier()
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Phone1 = supplier.Phone1,
                Phone2 = supplier.Phone2,
                Email = supplier.Email,
                Address1 = supplier.Address1,
                Address2 = supplier.Address2,
                City = supplier.City,
                Status = supplier.Status
            };
        }

        #endregion

        #region Return Temp

        private void ReturnTemp()
        {
            this.CurrentSupplier = new Supplier()
            {
                Id = this.CurrentSupplierTemp.Id,
                Name = this.CurrentSupplierTemp.Name,
                Phone1 = this.CurrentSupplierTemp.Phone1,
                Phone2 = this.CurrentSupplierTemp.Phone2,
                Email = this.CurrentSupplierTemp.Email,
                Address1 = this.CurrentSupplierTemp.Address1,
                Address2 = this.CurrentSupplierTemp.Address2,
                City = this.CurrentSupplierTemp.City,
                Status = this.CurrentSupplierTemp.Status
            };
        }

        #endregion

        #endregion

        #region Public Methods

        #region Select & Deselect

        public void SelectThis(Supplier supplier)
        {
            this.CurrentSupplierCollectionTemp.Add(supplier);
        }

        public void DeselectThis(Supplier supplier)
        {
            for (int i = 0; i < this.CurrentSupplierCollectionTemp.Count; i++)
            {
                if (this.CurrentSupplierCollectionTemp[i].Equals(supplier))
                    this.CurrentSupplierCollectionTemp.Remove(supplier);
            }
        }

        #endregion

        #region Save

        public bool Save()
        {
            bool isSaved = false;

            this.ReturnTemp();

            if (this.Presenter.Save(this.CurrentSupplier))
                isSaved = true;

            return isSaved;
        }

        #endregion

        #region Delete

        public void Delete()
        {
            for (int i = 0; i < this.CurrentSupplierCollectionTemp.Count; i++)
            {
                for (int j = 0; j < this.CurrentSupplierCollection.Count; j++)
                {
                    if (this.CurrentSupplierCollection[j].Equals(this.CurrentSupplierCollectionTemp[i]))
                    {
                        this.Presenter.Delete(this.CurrentSupplierCollection[j]);
                        break;
                    }
                }
            }
        }

        #endregion

        #region Detail

        public void Detail()
        {
            this.CurrentSupplier = this.CurrentSupplierCollectionTemp[0];
            this.FillTemp(this.CurrentSupplierCollectionTemp[0]);
        }

        #endregion

        #region Clear Detail

        public void ClearDetail()
        {
            this.CurrentSupplier = new Supplier();
            this.CurrentSupplierTemp = new Supplier();
        }

        #endregion

        #region etc

        public void Filter(string prop, string status, string keywords)
        {
            this.Presenter.FilterSupplier(prop, status, keywords);
        }

        public void ClearSelection()
        {
            this.CurrentSupplierCollectionTemp = new ObservableCollection<Supplier>();
            this.Presenter.Reload("Supplier");
        }

        public void Close()
        {
            this.Presenter.CloseTab(this);
            this.Presenter.Reload("Supplier");
        }

        #endregion

        #endregion

        #region Equality

        public bool Equals(SetupDataSupplierPresenter other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (this.GetType() == other.GetType());
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as SetupDataSupplierPresenter);
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

        public static bool operator ==(SetupDataSupplierPresenter x, SetupDataSupplierPresenter y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(SetupDataSupplierPresenter x, SetupDataSupplierPresenter y)
        {
            return !(x == y);
        }

        #endregion
    }
}
