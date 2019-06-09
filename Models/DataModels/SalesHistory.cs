using WelldonePOS.Helpers;

namespace WelldonePOS.Models.DataModels
{
    public class SalesHistory : Notifier
    {
        #region Private Fields

        private string _id = string.Empty;
        private string _clusterId = string.Empty;
        private Item _soldItem = new Item();
        private UnitOfSales _soldUnit = new UnitOfSales();
        private decimal _soldPrice = 0;
        private int _soldQty = 1;
        private decimal _subTotal = 0;
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
        public string ClusterId {
            get {
                return this._clusterId;
            }
            set {
                this._clusterId = value;
                OnPropertyChanged("ClusterId");
            }
        }
        public Item SoldItem {
            get {
                return this._soldItem;
            }
            set {
                this._soldItem = value;
                OnPropertyChanged("SoldItem");
            }
        }
        public UnitOfSales SoldUnit {
            get {
                return this._soldUnit;
            }
            set {
                this._soldUnit = value;
                OnPropertyChanged("SoldUnit");
            }
        }
        public decimal SoldPrice {
            get {
                return this._soldPrice;
            }
            set {
                this._soldPrice = value;
                OnPropertyChanged("SoldPrice");
            }
        }
        public int SoldQty {
            get {
                return this._soldQty;
            }
            set {
                this._soldQty = value;
                OnPropertyChanged("SoldQty");
            }
        }
        public decimal SubTotal {
            get {
                return this._subTotal;
            }
            set {
                this._subTotal = value;
                OnPropertyChanged("SubTotal");
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
            return string.Format("[{0}] {1}", this.ClusterId, this.SubTotal.ToString());
        }

        #endregion

        #region Equality

        public bool Equals(SalesHistory other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (string.Equals(this.Id, other.Id) && (Item.Equals(this.SoldItem, other.SoldItem)));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as SalesHistory);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                int hash = hashingBase;

                hash = (hash * hashingMultiplier) ^ (!object.ReferenceEquals(null, this.Id) ? this.Id.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!object.ReferenceEquals(null, this.SoldItem) ? this.SoldItem.GetHashCode() : 0);

                return hash;
            }
        }

        public static bool operator ==(SalesHistory x, SalesHistory y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(SalesHistory x, SalesHistory y)
        {
            return !(x == y);
        }

        #endregion
    }
}
