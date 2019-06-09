using WelldonePOS.Helpers;

namespace WelldonePOS.Models.DataModels
{
    public class UnitOfPurchase : Notifier
    {
        #region Private Fields

        private string _id = string.Empty;
        private string _clusterId = string.Empty;
        private string _barcode = string.Empty;
        private string _code = string.Empty;
        private string _name = string.Empty;
        private int _qtyPerUOP = 0;
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
        public string Barcode {
            get {
                return this._barcode;
            }
            set {
                this._barcode = value;
                OnPropertyChanged("Barcode");
            }
        }
        public string Code {
            get {
                return this._code;
            }
            set {
                this._code = value;
                OnPropertyChanged("Code");
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
        public int QtyPerUOP {
            get {
                return this._qtyPerUOP;
            }
            set {
                this._qtyPerUOP = value;
                OnPropertyChanged("QtyPerUOP");
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
            return this.Name;
        }

        #endregion

        #region Equality

        public bool Equals(UnitOfPurchase other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (string.Equals(this.Id, other.Id));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as UnitOfPurchase);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                int hash = hashingBase;

                hash = (hash * hashingMultiplier) ^ (!object.ReferenceEquals(null, this.Id) ? this.Id.GetHashCode() : 0);

                return hash;
            }
        }

        public static bool operator ==(UnitOfPurchase x, UnitOfPurchase y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(UnitOfPurchase x, UnitOfPurchase y)
        {
            return !(x == y);
        }

        #endregion
    }
}
