using System.Collections.ObjectModel;

using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Shells;
using WelldonePOS.UserControls.Views;

namespace WelldonePOS.Presenters.Views
{
    public class SetupDataCustomerPresenter : PresenterBase<SetupDataCustomerView>
    {
        #region Private Fields

        private readonly AppPresenter _appPresenter;

        private bool _isIdle;

        private ObservableCollection<Customer> _currentCustomerCollection;
        private ObservableCollection<Customer> _currentCustomerCollectionTemp;
        private Customer _currentCustomer;
        private Customer _currentCustomerTemp;

        #endregion

        #region ctor

        public SetupDataCustomerPresenter(AppPresenter appPresenter, SetupDataCustomerView view)
            : base(view, "TabHeader")
        {
            this._appPresenter = appPresenter;

            this._isIdle = true;

            this._currentCustomerCollection = appPresenter.CurrentCustomerCollection;
            this._currentCustomerCollectionTemp = new ObservableCollection<Customer>();
            this._currentCustomer = new Customer();
            this._currentCustomerTemp = null;
        }

        #endregion

        #region Public Properties

        public string TabHeader {
            get {
                return "Setup Data Customer";
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

        public ObservableCollection<Customer> CurrentCustomerCollection {
            get {
                return this._currentCustomerCollection;
            }
            set {
                this._currentCustomerCollection = value;
                OnPropertyChanged("CurrentCustomerCollection");
            }
        }
        public ObservableCollection<Customer> CurrentCustomerCollectionTemp {
            get {
                return this._currentCustomerCollectionTemp;
            }
            set {
                this._currentCustomerCollectionTemp = value;
                OnPropertyChanged("CurrentCustomerCollectionTemp");
            }
        }
        public Customer CurrentCustomer {
            get {
                return this._currentCustomer;
            }
            set {
                this._currentCustomer = value;
                OnPropertyChanged("CurrentCustomer");
            }
        }
        public Customer CurrentCustomerTemp {
            get { 
                return this._currentCustomerTemp; 
            }
            set {
                this._currentCustomerTemp = value;
                OnPropertyChanged("CurrentCustomerTemp");
            }
        }

        #endregion

        #region Private Methods

        #region Fill Temp

        private void FillTemp(Customer customer)
        {
            this.CurrentCustomerTemp = new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone1 = customer.Phone1,
                Phone2 = customer.Phone2,
                Email = customer.Email,
                Address1 = customer.Address1,
                Address2 = customer.Address2,
                City = customer.City,
                Status = customer.Status
            };
        }

        #endregion

        #region Return Temp

        private void ReturnTemp()
        {
            this.CurrentCustomer = new Customer()
            {
                Id = this.CurrentCustomerTemp.Id,
                Name = this.CurrentCustomerTemp.Name,
                Phone1 = this.CurrentCustomerTemp.Phone1,
                Phone2 = this.CurrentCustomerTemp.Phone2,
                Email = this.CurrentCustomerTemp.Email,
                Address1 = this.CurrentCustomerTemp.Address1,
                Address2 = this.CurrentCustomerTemp.Address2,
                City = this.CurrentCustomerTemp.City,
                Status = this.CurrentCustomerTemp.Status
            };
        }

        #endregion

        #endregion

        #region Public Methods

        #region Select & Deselect

        public void SelectThis(Customer customer)
        {
            this.CurrentCustomerCollectionTemp.Add(customer);
        }

        public void DeselectThis(Customer customer)
        {
            for (int i = 0; i < this.CurrentCustomerCollectionTemp.Count; i++)
            {
                if (this.CurrentCustomerCollectionTemp[i].Equals(customer))
                    this.CurrentCustomerCollectionTemp.Remove(customer);
            }
        }

        #endregion

        #region Save

        public bool Save()
        {
            bool isSaved = false;

            this.ReturnTemp();

            if (this.Presenter.Save(this.CurrentCustomer))
                isSaved = true;

            return isSaved;
        }

        #endregion

        #region Delete

        public void Delete()
        {
            for (int i = 0; i < this.CurrentCustomerCollectionTemp.Count; i++)
            {
                for (int j = 0; j < this.CurrentCustomerCollection.Count; j++)
                {
                    if (this.CurrentCustomerCollection[j].Equals(this.CurrentCustomerCollectionTemp[i]))
                    {
                        this.Presenter.Delete(this.CurrentCustomerCollection[j]);
                        break;
                    }
                }
            }
        }

        #endregion

        #region Detail

        public void Detail()
        {
            this.CurrentCustomer = this.CurrentCustomerCollectionTemp[0];
            this.FillTemp(this.CurrentCustomerCollectionTemp[0]);
        }

        #endregion

        #region Clear Detail

        public void ClearDetail()
        {
            this.CurrentCustomer = new Customer();
            this.CurrentCustomerTemp = new Customer();
        }

        #endregion

        #region etc

        public void Filter(string prop, string status, string keywords)
        {
            this.Presenter.FilterCustomer(prop, status, keywords);
        }

        public void ClearSelection()
        {
            this.CurrentCustomerCollectionTemp = new ObservableCollection<Customer>();
            this.Presenter.Reload("Customer");
        }

        public void Close()
        {
            this.Presenter.CloseTab(this);
            this.Presenter.Reload("Customer");
        }

        #endregion

        #endregion

        #region Equality

        public bool Equals(SetupDataCustomerPresenter other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (this.GetType() == other.GetType());
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as SetupDataCustomerPresenter);
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

        public static bool operator ==(SetupDataCustomerPresenter x, SetupDataCustomerPresenter y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(SetupDataCustomerPresenter x, SetupDataCustomerPresenter y)
        {
            return !(x == y);
        }

        #endregion
    }
}
