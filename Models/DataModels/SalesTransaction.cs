using System;
using System.Collections.Generic;

using WelldonePOS.Helpers;

namespace WelldonePOS.Models.DataModels
{
    public class SalesTransaction : Notifier
    {
        #region Private Fields

        private string _id = string.Empty;
        private string _code = string.Empty;
        private DateTime _transactionDate = DateTime.Now;
        private User _seller = new User();
        private Customer _consumer = new Customer();
        private List<SalesHistory> _salesDetail = new List<SalesHistory>();
        private decimal _sumTotal = 0;
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
        public string Code {
            get {
                return this._code;
            }
            set {
                this._code = value;
                OnPropertyChanged("Code");
            }
        }
        public DateTime TransactionDate {
            get {
                return this._transactionDate;
            }
            set {
                this._transactionDate = value;
                OnPropertyChanged("TransactionDate");
            }
        }
        public User Seller {
            get {
                return this._seller;
            }
            set {
                this._seller = value;
                OnPropertyChanged("Seller");
            }
        }
        public Customer Consumer {
            get {
                return this._consumer;
            }
            set {
                this._consumer = value;
                OnPropertyChanged("Consumer");
            }
        }
        public List<SalesHistory> SalesDetail {
            get {
                return this._salesDetail;
            }
            set {
                this._salesDetail = value;
                OnPropertyChanged("SalesDetail");
            }
        }
        public decimal SumTotal {
            get {
                return this._sumTotal;
            }
            set {
                this._sumTotal = value;
                OnPropertyChanged("SumTotal");
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
            return this.Code;
        }

        #endregion

        #region Equality

        public bool Equals(SalesTransaction other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return ((string.Equals(this.Id, other.Id)) && (string.Equals(this.Code, other.Code)));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as SalesTransaction);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                int hash = hashingBase;

                hash = (hash * hashingMultiplier) ^ (!object.ReferenceEquals(null, this.Id) ? this.Id.GetHashCode() : 0);
                hash = (hash * hashingMultiplier) ^ (!object.ReferenceEquals(null, this.Code) ? this.Code.GetHashCode() : 0);

                return hash;
            }
        }

        public static bool operator ==(SalesTransaction x, SalesTransaction y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(SalesTransaction x, SalesTransaction y)
        {
            return !(x == y);
        }

        #endregion
    }
}
