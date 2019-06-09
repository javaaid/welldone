using WelldonePOS.Helpers;

namespace WelldonePOS.Models.DataModels
{
    public class CompanyProfile : Notifier
    {
        #region Private Fields

        private string _name = string.Empty;
        private string _owner = string.Empty;
        private string _logoPath = "/Resources/DefaultCompanyLogo.png";
        private string _phone1 = string.Empty;
        private string _phone2 = string.Empty;
        private string _fax = string.Empty;
        private string _email = string.Empty;
        private string _website = string.Empty;
        private string _address1 = string.Empty;
        private string _address2 = string.Empty;
        private string _city = string.Empty;
        private string _zip = string.Empty;

        #endregion

        #region Public Properties

        public string Name {
            get {
                return this._name;
            }
            set {
                this._name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Owner {
            get {
                return this._owner;
            }
            set {
                this._owner = value;
                OnPropertyChanged("Owner");
            }
        }
        public string LogoPath {
            get {
                return this._logoPath;
            }
            set {
                this._logoPath = value;
                OnPropertyChanged("LogoPath");
            }
        }
        public string Phone1 {
            get {
                return this._phone1;
            }
            set {
                this._phone1 = value;
                OnPropertyChanged("Phone1");
            }
        }
        public string Phone2 {
            get {
                return this._phone2;
            }
            set {
                this._phone2 = value;
                OnPropertyChanged("Phone2");
            }
        }
        public string Fax {
            get {
                return this._fax;
            }
            set {
                this._fax = value;
                OnPropertyChanged("Fax");
            }
        }
        public string Email {
            get {
                return this._email;
            }
            set {
                this._email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Website {
            get {
                return this._website;
            }
            set {
                this._website = value;
                OnPropertyChanged("Website");
            }
        }
        public string Address1 {
            get {
                return this._address1;
            }
            set {
                this._address1 = value;
                OnPropertyChanged("Address1");
            }
        }
        public string Address2 {
            get {
                return this._address2;
            }
            set {
                this._address2 = value;
                OnPropertyChanged("Address2");
            }
        }
        public string City {
            get {
                return this._city;
            }
            set {
                this._city = value;
                OnPropertyChanged("City");
            }
        }
        public string Zip {
            get {
                return this._zip;
            }
            set {
                this._zip = value;
                OnPropertyChanged("Zip");
            }
        }
        

        #endregion

        #region Public Method

        public override string ToString()
        {
            return "General Settings - Company Profile";
        }

        #endregion

        #region Equality

        public bool Equals(CompanyProfile other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return true;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CompanyProfile);
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

        public static bool operator ==(CompanyProfile x, CompanyProfile y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(CompanyProfile x, CompanyProfile y)
        {
            return !(x == y);
        }

        #endregion
    }
}
