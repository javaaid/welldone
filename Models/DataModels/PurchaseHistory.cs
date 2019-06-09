using WelldonePOS.Helpers;

namespace WelldonePOS.Models.DataModels
{
    public class PurchaseHistory : Notifier
    {
        #region Private Fields

        private string _id = string.Empty;
        private string _clusterId = string.Empty;

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

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return this.ClusterId;
        }

        #endregion

        #region Equality

        public bool Equals(PurchaseHistory other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (string.Equals(this.Id, other.Id));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PurchaseHistory);
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

        public static bool operator ==(PurchaseHistory x, PurchaseHistory y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(PurchaseHistory x, PurchaseHistory y)
        {
            return !(x == y);
        }

        #endregion
    }
}
