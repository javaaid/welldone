using WelldonePOS.Helpers;

namespace WelldonePOS.Models.DataModels
{
    public class Customer : Notifier
    {
        #region Private Fields

        private string _id = string.Empty;
        private string _name = string.Empty;
        private string _phone1 = string.Empty;
        private string _phone2 = string.Empty;
        private string _email = string.Empty;
        private string _address1 = string.Empty;
        private string _address2 = string.Empty;
        private string _city = string.Empty;
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
        public string Email {
            get {
                return this._email;
            }
            set {
                this._email = value;
                OnPropertyChanged("Email");
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

        public bool Equals(Customer other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return ((string.Equals(this.Id, other.Id)) && (string.Equals(this.Name, other.Name)));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Customer);
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

        public static bool operator ==(Customer x, Customer y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(Customer x, Customer y)
        {
            return !(x == y);
        }

        #endregion
    }
}
