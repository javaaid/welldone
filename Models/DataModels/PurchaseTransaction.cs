using System;
using System.Collections.Generic;

using WelldonePOS.Helpers;

namespace WelldonePOS.Models.DataModels
{
    public class PurchaseTransaction : Notifier
    {
        #region Private Fields

        private string _id = string.Empty;
        private string _code = string.Empty;

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

        #endregion

        #region Methods

        public override string ToString()
        {
            return this.Code;
        }

        #endregion

        #region Equality

        public bool Equals(PurchaseTransaction other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return ((string.Equals(this.Id, other.Id)) && (string.Equals(this.Code, other.Code)));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PurchaseTransaction);
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

        public static bool operator ==(PurchaseTransaction x, PurchaseTransaction y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(PurchaseTransaction x, PurchaseTransaction y)
        {
            return !(x == y);
        }

        #endregion
    }
}
