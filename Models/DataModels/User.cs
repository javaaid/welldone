using System.Windows;
using WelldonePOS.Helpers;

namespace WelldonePOS.Models.DataModels
{
    public class User : Notifier
    {
        #region Private Fields

        private string _id = string.Empty;
        private string _name = string.Empty;
        private string _password = string.Empty;
        private UserRight _accessibility = new UserRight();
        private string _photoPath = "/Resources/DefaultUserPhoto.png";
        private bool _loggingIn = false;
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
        public string Password {
            get {
                return this._password;
            }
            set {
                this._password = value;
                OnPropertyChanged("Password");
            }
        }
        public UserRight Accessibility {
            get {
                return this._accessibility;
            }
            set {
                this._accessibility = value;
                OnPropertyChanged("Accessibility");
            }
        }
        public string PhotoPath {
            get {
                return this._photoPath;
            }
            set {
                this._photoPath = value;
                OnPropertyChanged("PhotoPath");
            }
        }
        public bool LoggingIn {
            get {
                return this._loggingIn;
            }
            set {
                this._loggingIn = value;
                OnPropertyChanged("LoggingIn");
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
            return string.Format("[{0}] {1}", this.Accessibility.Name, this.Name);
        }

        #endregion

        #region Equality

        public bool Equals(User other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return ((string.Equals(this.Id, other.Id)) && (string.Equals(this.Name, other.Name)));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as User);
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

        public static bool operator ==(User x, User y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(User x, User y)
        {
            return !(x == y);
        }

        #endregion
    }
}
