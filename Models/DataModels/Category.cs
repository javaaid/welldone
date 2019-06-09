using WelldonePOS.Helpers;

namespace WelldonePOS.Models.DataModels
{
    public class Category : Notifier
    {
        #region Private Fields

        private string _id = string.Empty;
        private string _name = string.Empty;
        private string _description = string.Empty;
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
        public string Description {
            get {
                return this._description;
            }
            set {
                this._description = value;
                OnPropertyChanged("Description");
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

        public bool Equals(Category other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return ((string.Equals(this.Id, other.Id)) && (string.Equals(this.Name, other.Name)));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Category);
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

        public static bool operator ==(Category x, Category y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(Category x, Category y)
        {
            return !(x == y);
        }

        #endregion
    }
}
