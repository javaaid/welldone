using WelldonePOS.Helpers;

namespace WelldonePOS.Models.DataModels
{
    public class GeneralSettings : Notifier
    {
        #region Private Fields

        private CompanyProfile _profile = new CompanyProfile();
        private CompanyPolicies _policies = new CompanyPolicies();

        #endregion

        #region Public Properties

        public CompanyProfile Profile {
            get {
                return this._profile;
            }
            set {
                this._profile = value;
                OnPropertyChanged("Profile");
            }
        }
        public CompanyPolicies Policies {
            get {
                return this._policies;
            }
            set {
                this._policies = value;
                OnPropertyChanged("Policies");
            }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return "General Settings";
        }

        #endregion

        #region Equality

        public bool Equals(GeneralSettings other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return true;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as GeneralSettings);
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

        public static bool operator ==(GeneralSettings x, GeneralSettings y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(GeneralSettings x, GeneralSettings y)
        {
            return !(x == y);
        }

        #endregion
    }
}
