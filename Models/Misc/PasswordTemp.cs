using WelldonePOS.Helpers;

namespace WelldonePOS.Models.Misc
{
    public class PasswordTemp : Notifier
    {
        #region Private Fields

        private string _oldPassword = string.Empty;
        private string _newPassword = string.Empty;

        #endregion

        #region Public Properties

        public string OldPassword {
            get {
                return this._oldPassword;
            }
            set {
                this._oldPassword = value;
                OnPropertyChanged("OldPassword");
            }
        }
        public string NewPassword {
            get {
                return this._newPassword;
            }
            set {
                this._newPassword = value;
                OnPropertyChanged("NewPassword");
            }
        }

        #endregion
    }
}
