using System.Data;
using System.Data.SqlClient;
using System.Windows;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;
using WelldonePOS.Models.Enums;

namespace WelldonePOS.Models.Repositories
{
    public class CompanyPoliciesRepository
    {
        #region Private Fields

        private readonly string _connString;
        private CompanyPolicies _cplStore;

        #endregion

        #region ctor

        public CompanyPoliciesRepository()
        {
            this._connString = SqlHelper.GetConnectionString();
            this._cplStore = new CompanyPolicies();

            this.PopulateData();
        }

        #endregion

        #region Private Methods

        #region Database Access

        private void PopulateData()
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                SqlDataReader dataReader = null;

                string selectSql = @"SELECT * FROM dbo.posVwCompanyPolicies;";

                SqlCommand command = new SqlCommand(selectSql, connection);

                connection.Open();

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    this._cplStore = new CompanyPolicies()
                    {
                        InventoryCostingPolicy = (InventoryCosting)dataReader["InventoryCostingPolicy"]
                    };
                }
            }
        }

        private void UpdateData(CompanyPolicies companyPolicies) 
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string updateSql = @"UPDATE dbo.posTbCompanyPolicies SET InventoryCostingPolicy = @CPLInventoryCostingPolicy;";

                SqlCommand command = new SqlCommand(updateSql, connection);

                command.Parameters.Add("@CPLInventoryCostingPolicy", SqlDbType.Int).Value = (int)companyPolicies.InventoryCostingPolicy;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #endregion

        #region Public Methods

        #region Actions

        public bool Save(CompanyPolicies companyPolicies)
        {
            bool isSaved = false;
            int retry = 0;

            while ((!isSaved) && (retry < 3))
            {
                try
                {
                    this.UpdateData(companyPolicies);

                    isSaved = true;
                }
                catch (SqlException)
                {
                    retry++;
                }
            }

            if (!isSaved)
                MessageBox.Show("Terjadi kesalahan program. Silahkan coba beberapa saat lagi.", "Proses Gagal", MessageBoxButton.OK);

            this.PopulateData();

            return isSaved;
        }

        #endregion

        #region Filters

        public CompanyPolicies FindPolicies()
        {
            return this._cplStore;
        }

        #endregion

        #endregion
    }
}
