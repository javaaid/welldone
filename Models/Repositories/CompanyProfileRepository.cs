using System.Data;
using System.Data.SqlClient;
using System.Windows;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;

namespace WelldonePOS.Models.Repositories
{
    public class CompanyProfileRepository
    {
        #region Private Fields

        private readonly string _connString;
        private CompanyProfile _cpfStore;

        #endregion

        #region ctor

        public CompanyProfileRepository()
        {
            this._connString = SqlHelper.GetConnectionString();
            this._cpfStore = new CompanyProfile();

            this.PopulateData();
        }

        #endregion

        #region Private Methods

        private void PopulateData()
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                SqlDataReader dataReader = null;

                string selectSql = @"SELECT * FROM dbo.posVwCompanyProfile;";

                SqlCommand command = new SqlCommand(selectSql, connection);

                connection.Open();

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    this._cpfStore = new CompanyProfile()
                    {
                        Name = (string)dataReader["Name"],
                        Owner = (string)dataReader["Owner"],
                        LogoPath = (string)dataReader["LogoPath"],
                        Phone1 = (string)dataReader["Phone1"],
                        Phone2 = (string)dataReader["Phone2"],
                        Fax = (string)dataReader["Fax"],
                        Email = (string)dataReader["Email"],
                        Website = (string)dataReader["Website"],
                        Address1 = (string)dataReader["Address1"],
                        Address2 = (string)dataReader["Address2"],
                        City = (string)dataReader["City"],
                        Zip = (string)dataReader["Zip"]
                    };
                }
            }
        }

        private void UpdateData(CompanyProfile companyProfile)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string updateSql = @"UPDATE dbo.posTbCompanyProfile SET Name = @CPFName, Owner = @CPFOwner, LogoPath = @CPFLogoPath, Phone1 = @CPFPhone1, Phone2 = @CPFPhone2, Fax = @CPFFax, Email = @CPFEmail, Website = @CPFWebsite, Address1 = @CPFAddress1, Address2 = @CPFAddress2, City = @CPFCity, Zip = @CPFZip;";

                SqlCommand command = new SqlCommand(updateSql, connection);

                command.Parameters.Add("@CPFName", SqlDbType.VarChar).Value = companyProfile.Name;
                command.Parameters.Add("@CPFOwner", SqlDbType.VarChar).Value = companyProfile.Owner;
                command.Parameters.Add("@CPFLogoPath", SqlDbType.VarChar).Value = companyProfile.LogoPath;
                command.Parameters.Add("@CPFPhone1", SqlDbType.VarChar).Value = companyProfile.Phone1;
                command.Parameters.Add("@CPFPhone2", SqlDbType.VarChar).Value = companyProfile.Phone2;
                command.Parameters.Add("@CPFFax", SqlDbType.VarChar).Value = companyProfile.Fax;
                command.Parameters.Add("@CPFEmail", SqlDbType.VarChar).Value = companyProfile.Email;
                command.Parameters.Add("@CPFWebsite", SqlDbType.VarChar).Value = companyProfile.Website;
                command.Parameters.Add("@CPFAddress1", SqlDbType.VarChar).Value = companyProfile.Address1;
                command.Parameters.Add("@CPFAddress2", SqlDbType.VarChar).Value = companyProfile.Address2;
                command.Parameters.Add("@CPFCity", SqlDbType.VarChar).Value = companyProfile.City;
                command.Parameters.Add("@CPFZip", SqlDbType.VarChar).Value = companyProfile.Zip;


                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Public Methods

        #region Actions

        public bool Save(CompanyProfile companyProfile)
        {
            bool isSaved = false;
            int retry = 0;

            while ((!isSaved) && (retry < 3))
            {
                try
                {
                    this.UpdateData(companyProfile);

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

        public CompanyProfile FindProfile()
        {
            return this._cpfStore;
        }

        #endregion

        #endregion
    }
}
