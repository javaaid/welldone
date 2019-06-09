using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;

namespace WelldonePOS.Models.Repositories
{
    public class UserRepository
    {
        #region Private Fields

        private readonly string _connString;
        private List<User> _usrStore;

        private readonly UserRightRepository _urgRepository;

        #endregion

        #region ctor

        public UserRepository()
        {
            this._connString = SqlHelper.GetConnectionString();
            this._usrStore = new List<User>();

            this._urgRepository = new UserRightRepository();

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

                string selectSql = @"SELECT * FROM dbo.posVwUser ORDER BY IdUser;";

                SqlCommand command = new SqlCommand(selectSql, connection);

                connection.Open();

                this._usrStore.Clear();

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    this._usrStore.Add(new User()
                        {
                            Id = (string)dataReader["IdUser"],
                            Name = (string)dataReader["Name"],
                            Password = (string)dataReader["Password"],
                            Accessibility = this.FindUserRight((string)dataReader["IdUserRight"]),
                            PhotoPath = (string)dataReader["PhotoPath"],
                            LoggingIn = (bool)dataReader["LoggingIn"],
                            Status = (bool)dataReader["Status"]
                        });
                }
            }
        }

        private void InsertData(User user)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string insertIntoSql = @"INSERT INTO dbo.posTbUser (IdUser, Name, Password, IdUserRight, PhotoPath, LoggingIn, Status) VALUES (@USRIdUser, @USRName, @USRPassword, @USRIdUserRight, @USRPhotoPath, @USRLoggingIn, @USRStatus);";

                SqlCommand command = new SqlCommand(insertIntoSql, connection);

                command.Parameters.Add("@USRIdUser", SqlDbType.Char).Value = user.Id;
                command.Parameters.Add("@USRName", SqlDbType.VarChar).Value = user.Name;
                command.Parameters.Add("@USRPassword", SqlDbType.VarChar).Value = user.Password;
                command.Parameters.Add("@USRIdUserRight", SqlDbType.Char).Value = user.Accessibility.Id;
                command.Parameters.Add("@USRPhotoPath", SqlDbType.VarChar).Value = user.PhotoPath;
                command.Parameters.Add("@USRLoggingIn", SqlDbType.Bit).Value = user.LoggingIn;
                command.Parameters.Add("@USRStatus", SqlDbType.Bit).Value = user.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void UpdateData(User user)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string updateSql = @"UPDATE dbo.posTbUser SET Name = @USRName, Password = @USRPassword, IdUserRight = @USRIdUserRight, PhotoPath = @USRPhotoPath, LoggingIn = @USRLoggingIn, Status = @USRStatus WHERE IdUser = @USRIdUser;";

                SqlCommand command = new SqlCommand(updateSql, connection);

                command.Parameters.Add("@USRIdUser", SqlDbType.Char).Value = user.Id;
                command.Parameters.Add("@USRName", SqlDbType.VarChar).Value = user.Name;
                command.Parameters.Add("@USRPassword", SqlDbType.VarChar).Value = user.Password;
                command.Parameters.Add("@USRIdUserRight", SqlDbType.Char).Value = user.Accessibility.Id;
                command.Parameters.Add("@USRPhotoPath", SqlDbType.VarChar).Value = user.PhotoPath;
                command.Parameters.Add("@USRLoggingIn", SqlDbType.Bit).Value = user.LoggingIn;
                command.Parameters.Add("@USRStatus", SqlDbType.Bit).Value = user.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void DeleteData(User user)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string deleteSql = @"DELETE FROM dbo.posTbUser WHERE IdUser = @USRIdUser;";

                SqlCommand command = new SqlCommand(deleteSql, connection);

                command.Parameters.Add("@USRIdUser", SqlDbType.Char).Value = user.Id;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region etc

        private UserRight FindUserRight(string id)
        {
            List<UserRight> urgStore = new List<UserRight>(this._urgRepository.FindAll());

            for (int i = 0; i < urgStore.Count; i++)
            {
                if (urgStore[i].Id == id)
                    return urgStore[i];
            }

            return new UserRight();
        }

        #endregion

        #endregion

        #region Public Methods

        #region Actions

        public bool Save(User user)
        {
            ValidationHelper validation = new ValidationHelper();
            
            bool isSaved = false;
            int retry = 0;

            if (validation.HasNoEquals(user, this._usrStore))
            {
                if (string.IsNullOrEmpty(user.Id))
                {
                    while ((!isSaved) && (retry < 3))
                    {
                        user.Id = "000000";

                        try
                        {
                            this.InsertData(user);

                            isSaved = true;
                        }
                        catch (SqlException)
                        {
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
                            this.UpdateData(user);

                            isSaved = true;
                        }
                        catch (SqlException)
                        {
                            retry++;
                        }
                    }
                }

                if (!isSaved)
                    MessageBox.Show("Terjadi kesalahan program. Silahkan coba beberapa saat lagi.", "Proses Gagal", MessageBoxButton.OK);
            }
            else
                MessageBox.Show(string.Format("User dengan nama [{0}] telah terdaftar.\n\nPastikan Anda menggunakan nama yang belum terdaftar sebelum melanjutkan proses.", user.Name), "Proses Gagal", MessageBoxButton.OK);

            this.PopulateData();

            return isSaved;
        }

        public void Delete(User user)
        {
            this.DeleteData(user);

            this.PopulateData();
        }

        #endregion

        #region Filters

        public List<User> FindByStatus(bool status)
        {
            IEnumerable<User> found1 = from usr in this._usrStore
                                       where (usr.Status == status)
                                       select usr;

            return found1.ToList();
        }

        public List<User> FindAll()
        {
            return new List<User>(this._usrStore);
        }

        #endregion

        #endregion
    }
}
