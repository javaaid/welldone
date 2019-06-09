using System.Collections.Generic;

using WelldonePOS.Helpers;

namespace WelldonePOS.Models.DataModels
{
    public class UserRight : Notifier
    {
        #region Private Fields

        private string _id = string.Empty;
        private string _name = string.Empty;
        private Dictionary<int, bool> _rights = new Dictionary<int, bool>()
        {
            // [V] TForm
            {0, false}, 
            // [V, A] Sales
            {1, false}, {2, false}, 
            // [V, A] Sales Return
            {3, false}, {4, false}, 
            // [V, A] Receipt
            {5, false}, {6, false}, 
            // [V, A] Receipt Return
            {7, false}, {8, false}, 
            // [V, A] Purchase
            {9, false}, {10, false}, 

            // [V] Report
            {11, false}, 
            // [V, E, D] Sales
            {12, false}, {13, false}, {14, false}, 
            // [V, E, D] Sales Return
            {15, false}, {16, false}, {17, false}, 
            // [V, E, D] Receipt
            {18, false}, {19, false}, {20, false},
            // [V, E, D] Receipt Return
            {21, false}, {22, false}, {23, false},
            // [V, E, D] Purchase
            {24, false}, {25, false}, {26, false},

            // [V] Setup Data
            {27, false}, 
            // [V, A, E, D] Item
            {28, false}, {29, false}, {30, false}, {31, false}, 
            // [V, A, E, D] Customer
            {32, false}, {33, false}, {34, false}, {35, false}, 
            // [V, A, E, D] Supplier
            {36, false}, {37, false}, {38, false}, {39, false}, 
            // [V, A, E, D] Category
            {40, false}, {41, false}, {42, false}, {43, false}, 

            // [V] Settings
            {44, false}, 
            // [V, E] General Settings
            {45, false}, {46, false}, 
            // [V, A, E, D] User
            {47, false}, {48, false}, {49, false}, {50, false}, 
            // [V, A, E, D] User Right
            {51, false}, {52, false}, {53, false}, {54, false},
            // [V, E] Theme
            {55, false}, {56, false}
        };
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
        public Dictionary<int, bool> Rights {
            get {
                return this._rights;
            }
            set {
                this._rights = value;
                OnPropertyChanged("Rights");
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

        public bool Equals(UserRight other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return ((string.Equals(this.Id, other.Id)) && (string.Equals(this.Name, other.Name)));
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as UserRight);
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

        public static bool operator ==(UserRight x, UserRight y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(UserRight x, UserRight y)
        {
            return !(x == y);
        }

        #endregion
    }
}
