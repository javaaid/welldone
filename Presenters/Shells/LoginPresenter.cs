using System.Collections.ObjectModel;
using System.Windows;

using WelldonePOS.Models.DataModels;
using WelldonePOS.Models.Repositories;
using WelldonePOS.UserControls.Shells;

using WelldonePOS.Helpers;

namespace WelldonePOS.Presenters.Shells
{
    public class LoginPresenter : PresenterBase<LoginShell>
    {
        #region Private Fields

        private readonly UserRepository _userRepository;

        private ObservableCollection<User> _currentUserCollection;
        private User _currentUser;
        private User _currentUserTemp;

        #endregion

        #region ctor

        public LoginPresenter(LoginShell view, UserRepository userRepository)
            : base(view)
        {
            this._userRepository = userRepository;

            this._currentUserCollection = new ObservableCollection<User>(this._userRepository.FindByStatus(true));
            this._currentUser = new User();
            this._currentUserTemp = null;
        }

        #endregion

        #region Public Properties

        public ObservableCollection<User> CurrentUserCollection {
            get {
                return this._currentUserCollection;
            }
            set {
                this._currentUserCollection = value;
                OnPropertyChanged("CurrentUserCollection");
            }
        }
        public User CurrentUser {
            get {
                return this._currentUser;
            }
            set {
                this._currentUser = value;
                OnPropertyChanged("CurrentUser");
            }
        }
        public User CurrentUserTemp {
            get {
                return this._currentUserTemp;
            }
            set {
                this._currentUserTemp = value;
                OnPropertyChanged("CurrentUserTemp");
            }
        }

        #endregion

        #region Private Methods

        private void FillTemp(User user)
        {
            this.CurrentUserTemp = new User()
            {
                Id = user.Id,
                Name = user.Name,
                Password = string.Empty,
                Accessibility = user.Accessibility,
                PhotoPath = user.PhotoPath,
                LoggingIn = user.LoggingIn,
                Status = user.Status
            };
        }

        private bool IsVerified(User user, User userTemp)
        {
            bool verified = false;

            if ((user.Id == userTemp.Id) && (user.Password == userTemp.Password))
                verified = true;

            return verified;
        }

        #endregion

        #region Public Methods

        public void SelectThis(User user)
        {
            this.CurrentUser = user;
            this.FillTemp(user);
        }

        public void Login()
        {
            if (!this.CurrentUser.LoggingIn)
            {
                if (this.IsVerified(this.CurrentUser, this.CurrentUserTemp))
                {
                    this.CurrentUser.LoggingIn = true;
                    this._userRepository.Save(this.CurrentUser);

                    (Application.Current.MainWindow.DataContext as MainPresenter).CurrentLogin = this.CurrentUser;

                    //Ngetes Connection String, jangan lupa hapus using reference HELPER di atas!
                    string x = SqlHelper.GetConnectionString();

                    //Pesan buat Elda!
                    MessageBox.Show(x);

                    View.NavigateToApp();
                }
                else
                    MessageBox.Show("Pastikan Anda mengisi password dengan benar sebelum melanjutkan proses.", "Login Gagal", MessageBoxButton.OK);
            }
            else
                MessageBox.Show("User sedang digunakan. Anda tidak dapat melanjutkan proses.", "Login Gagal", MessageBoxButton.OK);
        }

        public void Shutdown()
        {
            Application.Current.Shutdown();
        }

        #endregion
    }
}
