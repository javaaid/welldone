using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;
using WelldonePOS.Models.Misc;

namespace WelldonePOS.Models.Repositories
{
    public class UserRightRepository
    {
        #region Private Fields

        private readonly string _connString;
        private List<UserRight> _urgStore;

        #endregion

        #region ctor

        public UserRightRepository()
        {
            this._connString = SqlHelper.GetConnectionString();
            this._urgStore = new List<UserRight>();

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

                string selectSql = @"SELECT * FROM dbo.posVwUserRight ORDER BY IdUserRight;";

                SqlCommand command = new SqlCommand(selectSql, connection);

                connection.Open();

                this._urgStore.Clear();

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    this._urgStore.Add(new UserRight()
                    {
                        Id = (string)dataReader["IdUserRight"],
                        Name = (string)dataReader["Name"],
                        Rights = RightsHelper.ToRights((string)dataReader["Rights"]),
                        Status = (bool)dataReader["Status"]
                    });
                }
            }
        }

        private void InsertData(UserRight userRight)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string insertIntoSql = @"INSERT INTO dbo.posTbUserRight (IdUserRight, Name, Rights, Status) VALUES (@URGIdUserRight, @URGName, @URGRights, @URGStatus);";

                SqlCommand command = new SqlCommand(insertIntoSql, connection);

                command.Parameters.Add("@URGIdUserRight", SqlDbType.Char).Value = userRight.Id;
                command.Parameters.Add("@URGName", SqlDbType.VarChar).Value = userRight.Name;
                command.Parameters.Add("@URGRights", SqlDbType.VarChar).Value = RightsHelper.ToVarchar(userRight.Rights);
                command.Parameters.Add("@URGStatus", SqlDbType.Bit).Value = userRight.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void UpdateData(UserRight userRight)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string updateSql = @"UPDATE dbo.posTbUserRight SET Name = @URGName, Rights = @URGRights, Status = @URGStatus WHERE IdUserRight = @URGIdUserRight;";

                SqlCommand command = new SqlCommand(updateSql, connection);

                command.Parameters.Add("@URGIdUserRight", SqlDbType.Char).Value = userRight.Id;
                command.Parameters.Add("@URGName", SqlDbType.VarChar).Value = userRight.Name;
                command.Parameters.Add("@URGRights", SqlDbType.VarChar).Value = RightsHelper.ToVarchar(userRight.Rights);
                command.Parameters.Add("@URGStatus", SqlDbType.Bit).Value = userRight.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void DeleteData(UserRight userRight)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string deleteSql = @"DELETE FROM dbo.posTbUserRight WHERE IdUserRight = @URGIdUserRight;";

                SqlCommand command = new SqlCommand(deleteSql, connection);

                command.Parameters.Add("@URGIdUserRight", SqlDbType.Char).Value = userRight.Id;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #endregion

        #region Public Methods

        #region Actions

        public bool Save(UserRight userRight)
        {
            ValidationHelper validation = new ValidationHelper();

            bool isSaved = false;
            int retry = 0;

            if (validation.HasNoEquals(userRight, this._urgStore))
            {
                if (string.IsNullOrEmpty(userRight.Id))
                {
                    while ((!isSaved) && (retry < 3))
                    {
                        userRight.Id = "000000";

                        try
                        {
                            this.InsertData(userRight);

                            isSaved = true;
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);

                            retry++;
                        }
                    }
                }
                else
                {
                    while ((!isSaved) && (retry < 3))
                    {
                        try
                        {
                            this.UpdateData(userRight);

                            isSaved = true;
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show(ex.Message);

                            retry++;
                        }
                    }
                }

                if (!isSaved)
                    MessageBox.Show("Terjadi kesalahan program. Silahkan coba beberapa saat lagi.", "Proses Gagal", MessageBoxButton.OK);
            }
            else
                MessageBox.Show(string.Format("Hak user dengan nama [{0}] telah terdaftar.\n\nPastikan Anda menggunakan nama yang belum terdaftar sebelum melanjutkan proses.", userRight.Name), "Proses Gagal", MessageBoxButton.OK);

            this.PopulateData();

            return isSaved;
        }

        public void Delete(UserRight userRight)
        {
            this.DeleteData(userRight);

            this.PopulateData();
        }

        #endregion

        #region Filters

        public List<UserRight> FindByStatus(bool status)
        {
            IEnumerable<UserRight> found1 = from urg in this._urgStore
                                            where (urg.Status == status)
                                            select urg;

            return found1.ToList();
        }

        public List<UserRight> FindAll()
        {
            return new List<UserRight>(this._urgStore);
        }

        #endregion

        #endregion
    }
}
