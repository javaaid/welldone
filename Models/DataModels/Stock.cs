using System;

using WelldonePOS.Helpers;
using WelldonePOS.Models.Enums;

namespace WelldonePOS.Models.DataModels
{
    public class Stock : Notifier
    {
        #region Private Fields

        private string _id = string.Empty;
        private string _clusterId = string.Empty;
        private DateTime _receptionDate = DateTime.Now;
        private StockOrigin _origin = StockOrigin.None;
        private int _initialQty = 0;
        private int _currentQty = 0;
        private decimal _priceOfPurchase;
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
        public DateTime ReceptionDate {
            get {
                return this._receptionDate;
            }
            set {
                this._receptionDate = value;
                OnPropertyChanged("ReceptionDate");
            }
        }
        public StockOrigin Origin {
            get {
                return this._origin;
            }
            set {
                this._origin = value;
                OnPropertyChanged("Origin");
            }
        }
        public int InitialQty {
            get {
                return this._initialQty;
            }
            set {
                this._initialQty = value;
                OnPropertyChanged("InitialQty");
            }
        }
        public int CurrentQty {
            get {
                return this._currentQty;
            }
            set {
                this._currentQty = value;
                OnPropertyChanged("CurrentQty");
            }
        }
        public decimal PriceOfPurchase {
            get {
                return this._priceOfPurchase;
            }
            set {
                this._priceOfPurchase = value;
                OnPropertyChanged("PriceOfPurchase");
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
            return string.Format("[{0}] {1}/{2} [{3}]", this.ReceptionDate.ToShortDateString(), this.CurrentQty.ToString(), this.InitialQty.ToString(), this.Origin.ToString());
        }

        #endregion

        #region Equality

        public bool Equals(Stock other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (string.Equals(this.Id, other.Id));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Stock);
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

        public static bool operator ==(Stock x, Stock y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(Stock x, Stock y)
        {
            return !(x == y);
        }

        #endregion
    }
}
