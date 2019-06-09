using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;
using WelldonePOS.Models.Enums;
using WelldonePOS.Presenters.Shells;
using WelldonePOS.UserControls.Views;

namespace WelldonePOS.Presenters.Views
{
    public class TFormSalesPresenter : PresenterBase<TFormSalesView>
    {
        #region Private Fields

        private readonly AppPresenter _appPresenter;

        private SalesTransaction _currentSalesTransaction;

        private ObservableCollection<SalesHistory> _currentSalesHistoryCollection;
        private SalesHistory _currentSalesHistory;

        private ObservableCollection<Customer> _currentCustomerCollection;
        private ObservableCollection<Item> _currentTItemCollection;
        private ObservableCollection<UnitOfSales> _currentUnitCollection;

        private int _currentAvailableStock;
        private decimal _currentPaid;
        private decimal _currentChange;

        #endregion

        #region ctor

        public TFormSalesPresenter(AppPresenter appPresenter, TFormSalesView view)
            : base(view, "TabHeader")
        {
            this._appPresenter = appPresenter;

            this._currentSalesTransaction = new SalesTransaction()
            {
                TransactionDate = DateTime.Now,
                Seller = appPresenter.CurrentLogin,
                Consumer = appPresenter.FindCustomer(true)[0]
            };

            this._currentSalesHistoryCollection = new ObservableCollection<SalesHistory>();
            this._currentSalesHistory = null;

            this._currentCustomerCollection = new ObservableCollection<Customer>(appPresenter.FindCustomer(true));
            this._currentTItemCollection = appPresenter.CurrentTItemCollection;
            this._currentUnitCollection = null;

            this._currentAvailableStock = 0;
            this._currentPaid = 0;
            this._currentChange = 0;
        }

        #endregion

        #region Public Properties

        public string TabHeader {
            get {
                return "Form Transaksi Penjualan";
            }
        }

        public AppPresenter Presenter {
            get {
                return this._appPresenter;
            }
        }

        public SalesTransaction CurrentSalesTransaction {
            get {
                return this._currentSalesTransaction;
            }
            set {
                this._currentSalesTransaction = value;
                OnPropertyChanged("CurrentSalesTransaction");
            }
        }

        public ObservableCollection<SalesHistory> CurrentSalesHistoryCollection {
            get {
                return this._currentSalesHistoryCollection;
            }
            set {
                this._currentSalesHistoryCollection = value;
                OnPropertyChanged("CurrentSalesHistoryCollection");
            }
        }
        public SalesHistory CurrentSalesHistory {
            get {
                return this._currentSalesHistory;
            }
            set {
                this._currentSalesHistory = value;
                OnPropertyChanged("CurrentSalesHistory");
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
        public ObservableCollection<Item> CurrentTItemCollection {
            get {
                return this._currentTItemCollection;
            }
            set {
                this._currentTItemCollection = value;
                OnPropertyChanged("CurrentTItemCollection");
            }
        }
        public ObservableCollection<UnitOfSales> CurrentUnitCollection {
            get {
                return this._currentUnitCollection;
            }
            set {
                this._currentUnitCollection = value;
                OnPropertyChanged("CurrentUnitCollection");
            }
        }

        public int CurrentAvailableStock {
            get {
                return this._currentAvailableStock;
            }
            set {
                this._currentAvailableStock = value;
                OnPropertyChanged("CurrentAvailableStock");
            }
        }
        public decimal CurrentPaid {
            get {
                return this._currentPaid;
            }
            set {
                this._currentPaid = value;
                OnPropertyChanged("CurrentPaid");
            }
        }
        public decimal CurrentChange {
            get {
                return this._currentChange;
            }
            set {
                this._currentChange = value;
                OnPropertyChanged("CurrentChange");
            }
        }

        #endregion

        #region Private Methods

        #region Calculation

        private void FindUnit(Item item)
        {
            //List<UnitNature> natures = new List<UnitNature>(new UnitNature[] { UnitNature.SalesOnly, UnitNature.Both });

            this.CurrentUnitCollection = new ObservableCollection<UnitOfSales>();

            for (int i = 0; i < item.UnitsOfSales.Count; i++)
            {
                if (item.UnitsOfSales[i].Status == true)
                    this.CurrentUnitCollection.Add(item.UnitsOfSales[i]);
            }

            //for (int i = 0; i < item.Units.Count; i++)
            //{
            //    for (int j = 0; j < natures.Count; j++)
            //    {
            //        if (natures[j] == item.Units[i].Nature)
            //        {
            //            this.CurrentUnitCollection.Add(item.Units[i]);

            //            break;
            //        }
            //    }
            //}
        }

        private void CalculateAvailableStock()
        {
            if (this.CurrentSalesHistory == null)
                return;

            if (this.CurrentSalesHistory.SoldUnit.QtyPerUOS == 0)
                return;

            this.CurrentAvailableStock = 0;

            for (int i = 0; i < this.CurrentSalesHistory.SoldItem.Stocks.Count; i++)
            {
                this.CurrentAvailableStock = this.CurrentAvailableStock + this.CurrentSalesHistory.SoldItem.Stocks[i].CurrentQty;
            }

            for (int i = 0; i < this.CurrentSalesHistoryCollection.Count; i++)
            {
                if (this.CurrentSalesHistoryCollection[i].SoldItem.Equals(this.CurrentSalesHistory.SoldItem))
                    this.CurrentAvailableStock = this.CurrentAvailableStock - (this.CurrentSalesHistoryCollection[i].SoldUnit.QtyPerUOS * this.CurrentSalesHistoryCollection[i].SoldQty);
            }

            this.CurrentAvailableStock = this.CurrentAvailableStock / this.CurrentSalesHistory.SoldUnit.QtyPerUOS;
        }

        private void CalculateSumTotal()
        {
            this.CurrentSalesTransaction.SumTotal = 0;

            for (int i = 0; i < this.CurrentSalesHistoryCollection.Count; i++)
            {
                this.CurrentSalesTransaction.SumTotal += this.CurrentSalesHistoryCollection[i].SubTotal;
            }
        }

        private void CalculateChange(decimal cashPay)
        {
            this.CurrentChange = cashPay - this.CurrentSalesTransaction.SumTotal;
        }

        #endregion

        #endregion

        #region Public Methods

        public void SelectThis(Item item)
        {
            this.CurrentSalesHistory = null;

            this.CurrentSalesHistory = new SalesHistory();
            
            this.CurrentSalesHistory.SoldItem = item;

            this.FindUnit(item);
        }

        public void SelectUnit()
        {
            this.CalculateAvailableStock();
        }

        public bool AddSalesHistory()
        {
            bool isAdded = false;

            if (this.CurrentAvailableStock >= this.CurrentSalesHistory.SoldQty)
            {
                if (this.CurrentSalesHistoryCollection.Count > 0)
                {
                    for (int i = 0; i < this.CurrentSalesHistoryCollection.Count; i++)
                    {
                        if (this.CurrentSalesHistoryCollection[i].SoldUnit.Equals(this.CurrentSalesHistory.SoldUnit))
                        {
                            this.CurrentSalesHistoryCollection[i].SoldQty = this.CurrentSalesHistoryCollection[i].SoldQty + this.CurrentSalesHistory.SoldQty;

                            this.CurrentSalesHistoryCollection[i].SubTotal = this.CurrentSalesHistoryCollection[i].SoldPrice * this.CurrentSalesHistoryCollection[i].SoldQty;

                            isAdded = true;
                        }
                    }
                }

                if (!isAdded)
                {
                    for (int i = 0; i < this.CurrentSalesHistory.SoldItem.UnitsOfSales.Count; i++)
                    {
                        if (this.CurrentSalesHistory.SoldItem.UnitsOfSales[i].Id == this.CurrentSalesHistory.SoldUnit.Id)
                        {
                            this.CurrentSalesHistory.SoldPrice = this.CurrentSalesHistory.SoldItem.UnitsOfSales[i].DefaultPriceOfSales;
                            break;
                        }
                    }

                    this.CurrentSalesHistory.SubTotal = this.CurrentSalesHistory.SoldPrice * this.CurrentSalesHistory.SoldQty;

                    this.CurrentSalesHistoryCollection.Add(this.CurrentSalesHistory);

                    isAdded = true;
                }

                this.CurrentPaid = 0;
                this.CurrentChange = 0;
                this.CurrentSalesHistory = null;
                this.CurrentAvailableStock = 0;

                this.CalculateSumTotal();
            }

            return isAdded;
        }

        public void RemoveSalesHistory(SalesHistory salesHistory)
        {
            this.CurrentSalesHistoryCollection.Remove(salesHistory);

            this.CurrentPaid = 0;
            this.CurrentChange = 0;
            this.CurrentSalesHistory = null;
            this.CurrentAvailableStock = 0;

            this.CalculateSumTotal();
        }

        public bool Save()
        {
            bool isSaved = false;

            this.CurrentSalesTransaction.SalesDetail = new List<SalesHistory>(this.CurrentSalesHistoryCollection);

            if (this.Presenter.Save(this.CurrentSalesTransaction))
                isSaved = true;

            for (int i = 0; i < this.CurrentSalesTransaction.SalesDetail.Count; i++)
            {
                this.CurrentSalesTransaction.SalesDetail[i].ClusterId = this.CurrentSalesTransaction.Id;
                this.Presenter.Save(this.CurrentSalesTransaction.SalesDetail[i]);
            }

            return isSaved;
        }

        public void Filter(string prop, string keywords)
        {
            this.Presenter.FilterTItem(prop, keywords);
        }

        public void UpdateChange(string cashPay)
        {
            this.CalculateChange(decimal.Parse(cashPay));
        }

        public void Close()
        {
            this.Presenter.CloseTab(this);
            this.Presenter.Reload("TItem");
            this.Presenter.Reload("SalesTransaction");
        }

        #endregion

        #region Equality

        public bool Equals(TFormSalesPresenter other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (this.GetType() == other.GetType());
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TFormSalesPresenter);
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

        public static bool operator ==(TFormSalesPresenter x, TFormSalesPresenter y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(TFormSalesPresenter x, TFormSalesPresenter y)
        {
            return !(x == y);
        }

        #endregion
    }
}
