using System.Collections.ObjectModel;

using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Shells;
using WelldonePOS.UserControls.Views;

namespace WelldonePOS.Presenters.Views
{
    public class SetupGeneralSettingsPresenter : PresenterBase<SetupGeneralSettingsView>
    {
        #region Private Fields

        private readonly AppPresenter _appPresenter;

        private bool _isIdle;

        private ObservableCollection<GeneralSettings> _currentGeneralSettings;

        private CompanyProfile _currentCompanyProfile;
        private CompanyProfile _currentCompanyProfileTemp;

        private CompanyPolicies _currentCompanyPolicies;
        private CompanyPolicies _currentCompanyPoliciesTemp;

        #endregion

        #region ctor

        public SetupGeneralSettingsPresenter(AppPresenter appPresenter, SetupGeneralSettingsView view)
            : base(view, "TabHeader")
        {
            this._appPresenter = appPresenter;

            this._isIdle = true;

            this._currentGeneralSettings = appPresenter.CurrentGeneralSettings;

            this._currentCompanyProfile = new CompanyProfile();
            this._currentCompanyProfileTemp = null;

            this._currentCompanyPolicies = new CompanyPolicies();
            this._currentCompanyPoliciesTemp = null;
        }

        #endregion

        #region Public Properties

        public string TabHeader {
            get {
                return "Pengaturan Toko";
            }
        }

        public AppPresenter Presenter {
            get {
                return this._appPresenter;
            }
        }

        public bool IsIdle {
            get {
                return this._isIdle;
            }
            set {
                this._isIdle = value;
                OnPropertyChanged("IsIdle");
            }
        }

        public ObservableCollection<GeneralSettings> CurrentGeneralSettings {
            get {
                return this._currentGeneralSettings;
            }
            set {
                this._currentGeneralSettings = value;
                OnPropertyChanged("CurrentGeneralSettings");
            }
        }

        public CompanyProfile CurrentCompanyProfile {
            get {
                return this._currentCompanyProfile;
            }
            set {
                this._currentCompanyProfile = value;
                OnPropertyChanged("CurrentCompanyProfile");
            }
        }
        public CompanyProfile CurrentCompanyProfileTemp {
            get {
                return this._currentCompanyProfileTemp;
            }
            set {
                this._currentCompanyProfileTemp = value;
                OnPropertyChanged("CurrentCompanyProfileTemp");
            }
        }

        public CompanyPolicies CurrentCompanyPolicies {
            get {
                return this._currentCompanyPolicies;
            }
            set {
                this._currentCompanyPolicies = value;
                OnPropertyChanged("CurrentCompanyPolicies");
            }
        }
        public CompanyPolicies CurrentCompanyPoliciesTemp {
            get {
                return this._currentCompanyPoliciesTemp;
            }
            set {
                this._currentCompanyPoliciesTemp = value;
                OnPropertyChanged("CurrentCompanyPoliciesTemp");
            }
        }

        #endregion

        #region Private Methods

        #region FillTemp

        private void FillTemp(CompanyProfile companyProfile)
        {
            this.CurrentCompanyProfileTemp = new CompanyProfile()
            {
                Name = companyProfile.Name,
                Owner = companyProfile.Owner,
                LogoPath = companyProfile.LogoPath,
                Phone1 = companyProfile.Phone1,
                Phone2 = companyProfile.Phone2,
                Fax = companyProfile.Fax,
                Email = companyProfile.Email,
                Website = companyProfile.Website,
                Address1 = companyProfile.Address1,
                Address2 = companyProfile.Address2,
                City = companyProfile.City,
                Zip = companyProfile.Zip
            };
        }

        private void FillTemp(CompanyPolicies companyPolicies)
        {
            this.CurrentCompanyPoliciesTemp = new CompanyPolicies()
            {
                InventoryCostingPolicy = companyPolicies.InventoryCostingPolicy
            };
        }

        #endregion

        #region ReturnTemp

        private void ReturnCompanyProfileTemp()
        {
            this.CurrentCompanyProfile = new CompanyProfile()
            {
                Name = this.CurrentCompanyProfileTemp.Name,
                Owner = this.CurrentCompanyProfileTemp.Owner,
                LogoPath = this._currentCompanyProfileTemp.LogoPath,
                Phone1 = this.CurrentCompanyProfileTemp.Phone1,
                Phone2 = this.CurrentCompanyProfileTemp.Phone2,
                Fax = this.CurrentCompanyProfileTemp.Fax,
                Email = this.CurrentCompanyProfileTemp.Email,
                Website = this.CurrentCompanyProfileTemp.Website,
                Address1 = this.CurrentCompanyProfileTemp.Address1,
                Address2 = this.CurrentCompanyProfileTemp.Address2,
                City = this.CurrentCompanyProfileTemp.City,
                Zip = this.CurrentCompanyProfileTemp.Zip
            };
        }

        private void ReturnCompanyPoliciesTemp()
        {
            this.CurrentCompanyPolicies = new CompanyPolicies()
            {
                InventoryCostingPolicy = this.CurrentCompanyPoliciesTemp.InventoryCostingPolicy
            };
        }

        #endregion

        #endregion

        #region Public Methods

        #region Save

        public bool SaveCompanyProfile()
        {
            bool isSaved = false;

            this.ReturnCompanyProfileTemp();

            if (this.Presenter.Save(this.CurrentCompanyProfile))
                isSaved = true;

            return isSaved;
        }

        public bool SaveCompanyPolicies()
        {
            bool isSaved = false;

            this.ReturnCompanyPoliciesTemp();

            if (this.Presenter.Save(this.CurrentCompanyPolicies))
                isSaved = true;

            return isSaved;
        }

        #endregion

        #region Detail

        public void CompanyProfileDetail()
        {
            this.CurrentCompanyProfile = this.CurrentGeneralSettings[0].Profile;
            this.FillTemp(this.CurrentGeneralSettings[0].Profile);
        }

        public void CompanyPoliciesDetail()
        {
            this.CurrentCompanyPolicies = this.CurrentGeneralSettings[0].Policies;
            this.FillTemp(this.CurrentGeneralSettings[0].Policies);
        }

        #endregion

        #region Clear Detail

        public void ClearCompanyProfileDetail()
        {
            this.CurrentCompanyProfile = new CompanyProfile();
            this.CurrentCompanyProfileTemp = new CompanyProfile();
        }

        public void ClearCompanyPoliciesDetail()
        {
            this.CurrentCompanyPolicies = new CompanyPolicies();
            this.CurrentCompanyPoliciesTemp = new CompanyPolicies();
        }

        #endregion

        #region etc

        public void SelectLogo()
        {
            string logoPath = View.BrowseLogo();

            if (!string.IsNullOrEmpty(logoPath))
                this.CurrentCompanyProfileTemp.LogoPath = logoPath;
        }

        public void Close()
        {
            this.Presenter.CloseTab(this);
            this.Presenter.Reload("GeneralSettings");
        }

        #endregion

        #endregion

        #region Equality

        public bool Equals(SetupGeneralSettingsPresenter other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (this.GetType() == other.GetType());
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as SetupGeneralSettingsPresenter);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                int hash = hashingBase;

                hash = (hash * hashingMultiplier) ^ (!object.ReferenceEquals(null, this.GetType()) ? this.GetType().GetHashCode() : 0);

                return hash;
            }
        }

        public static bool operator ==(SetupGeneralSettingsPresenter x, SetupGeneralSettingsPresenter y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(SetupGeneralSettingsPresenter x, SetupGeneralSettingsPresenter y)
        {
            return !(x == y);
        }

        #endregion
    }
}
