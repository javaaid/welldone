using WelldonePOS.Helpers;
using WelldonePOS.Models.Enums;

namespace WelldonePOS.Models.DataModels
{
    public class CompanyPolicies : Notifier
    {
        #region Private Fields

        private InventoryCosting _inventoryCostingPolicy = InventoryCosting.FIFO;

        #endregion

        #region Public Properties

        public InventoryCosting InventoryCostingPolicy {
            get {
                return this._inventoryCostingPolicy;
            }
            set {
                this._inventoryCostingPolicy = value;
                OnPropertyChanged("InventoryCostingPolicy");
            }
        }

        #endregion

        #region Public Method

        public override string ToString()
        {
            return "General Settings - Company Policies";
        }

        #endregion

        #region Equality

        public bool Equals(CompanyPolicies other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return true;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CompanyPolicies);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int hashingBase = (int)2166136261;

                int hash = hashingBase;

                return hash;
            }
        }

        public static bool operator ==(CompanyPolicies x, CompanyPolicies y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(CompanyPolicies x, CompanyPolicies y)
        {
            return !(x == y);
        }

        #endregion
    }
}
