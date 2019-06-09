using System.Collections.Generic;

using WelldonePOS.Helpers;

namespace WelldonePOS.Models.DataModels
{
    public class Item : Notifier
    {
        #region Private Fields

        private string _id = string.Empty;
        private string _name = string.Empty;
        private Category _group = new Category();
        private string _refUnit = string.Empty;
        private decimal _minimumProfit = 0;
        private int _safetyStockPortion = 0;
        private List<Stock> _stocks = new List<Stock>();
        private List<UnitOfPurchase> _unitsOfPurchase = new List<UnitOfPurchase>();
        private List<UnitOfSales> _unitsOfSales = new List<UnitOfSales>();
        private bool _status = true;

        #endregion

        #region Public Properties

        public string Id {
            get {
                return this._id;
            }
            set {
                this._id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Name {
            get {
                return this._name;
            }
            set {
                this._name = value;
                OnPropertyChanged("Name");
            }
        }
        public Category Group {
            get {
                return this._group;
            }
            set {
                this._group = value;
                OnPropertyChanged("Group");
            }
        }
        public string RefUnit {
            get {
                return this._refUnit;
            }
            set {
                this._refUnit = value;
                OnPropertyChanged("RefUnit");
            }
        }
        public decimal MinimumProfit {
            get {
                return this._minimumProfit;
            }
            set {
                this._minimumProfit = value;
                OnPropertyChanged("MinimumProfit");
            }
        }
        public int SafetyStockPortion {
            get {
                return this._safetyStockPortion;
            }
            set {
                this._safetyStockPortion = value;
                OnPropertyChanged("SafetyStockPortion");
            }
        }
        public List<Stock> Stocks {
            get {
                return this._stocks;
            }
            set {
                this._stocks = value;
                OnPropertyChanged("Stocks");
            }
        }
        public List<UnitOfPurchase> UnitsOfPurchase {
            get {
                return this._unitsOfPurchase;
            }
            set {
                this._unitsOfPurchase = value;
                OnPropertyChanged("UnitsOfPurchase");
            }
        }
        public List<UnitOfSales> UnitsOfSales {
            get {
                return this._unitsOfSales;
            }
            set {
                this._unitsOfSales = value;
                OnPropertyChanged("UnitsOfSales");
            }
        }
        public bool Status {
            get {
                return this._status;
            }
            set {
                this._status = value;
                OnPropertyChanged("Status");
            }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return string.Format("[{0}] {1}", this.Group.Name, this.Name);
        }

        #endregion

        #region Equality

        public bool Equals(Item other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return ((string.Equals(this.Id, other.Id)) && (string.Equals(this.Name, other.Name)));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Item);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                int hash = hashingBase;

                hash = (hash * hashingMultiplier) ^ (!object.ReferenceEquals(null, this.Id) ? this.Id.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!object.ReferenceEquals(null, this.Name) ? this.Name.GetHashCode() : 0);

                return hash;
            }
        }

        public static bool operator ==(Item x, Item y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(Item x, Item y)
        {
            return !(x == y);
        }

        #endregion
    }
}
