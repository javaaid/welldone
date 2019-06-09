using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;

namespace WelldonePOS.Models.Repositories
{
    public class GeneralSettingsRepository
    {
        #region Private Fields

        private readonly string _connString;
        private GeneralSettings _gstStore;

        private readonly CompanyProfileRepository _cpfRepository;
        private readonly CompanyPoliciesRepository _cplRepository;

        #endregion

        #region ctor

        public GeneralSettingsRepository()
        {
            this._connString = SqlHelper.GetConnectionString();
            this._gstStore = new GeneralSettings();

            this._cpfRepository = new CompanyProfileRepository();
            this._cplRepository = new CompanyPoliciesRepository();

            this.PopulateData();
        }

        #endregion

        #region Private Methods

        private void PopulateData()
        {
            this._gstStore = new GeneralSettings()
            {
                Profile = this._cpfRepository.FindProfile(),
                Policies = this._cplRepository.FindPolicies()
            };
        }

        #endregion

        #region Public Methods

        #region Actions

        public bool Save(CompanyProfile companyProfile)
        {
            bool isSaved = false;

            if (this._cpfRepository.Save(companyProfile))
                isSaved = true;

            this.PopulateData();

            return isSaved;
        }

        public bool Save(CompanyPolicies companyPolicies)
        {
            bool isSaved = false;

            if (this._cplRepository.Save(companyPolicies))
                isSaved = true;

            this.PopulateData();

            return isSaved;
        }

        #endregion

        #region Filters

        public GeneralSettings FindGeneralSettings()
        {
            return this._gstStore;
        }

        #endregion

        #endregion
    }
}
