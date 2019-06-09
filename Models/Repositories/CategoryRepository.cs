using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;

namespace WelldonePOS.Models.Repositories
{
    public class CategoryRepository
    {
        #region Private Fields

        private readonly string _connString;
        private List<Category> _ctgStore;

        #endregion

        #region ctor

        public CategoryRepository()
        {
            this._connString = SqlHelper.GetConnectionString();
            this._ctgStore = new List<Category>();

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

                string selectSql = @"SELECT * FROM dbo.posVwCategory ORDER BY IdCategory;";

                SqlCommand command = new SqlCommand(selectSql, connection);

                connection.Open();

                this._ctgStore.Clear();

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    this._ctgStore.Add(new Category()
                        {
                            Id = (string)dataReader["IdCategory"],
                            Name = (string)dataReader["Name"],
                            Description = (string)dataReader["Description"],
                            Status = (bool)dataReader["Status"]
                        });
                }
            }
        }

        private void InsertData(Category category)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string insertIntoSql = @"INSERT INTO dbo.posTbCategory (IdCategory, Name, Description, Status) VALUES (@CTGIdCategory, @CTGName, @CTGDescription, @CTGStatus);";

                SqlCommand command = new SqlCommand(insertIntoSql, connection);

                command.Parameters.Add("@CTGIdCategory", SqlDbType.Char).Value = category.Id;
                command.Parameters.Add("@CTGName", SqlDbType.VarChar).Value = category.Name;
                command.Parameters.Add("@CTGDescription", SqlDbType.VarChar).Value = category.Description;
                command.Parameters.Add("@CTGStatus", SqlDbType.Bit).Value = category.Status;

                connection.Open();
                
                command.ExecuteNonQuery();
            }
        }

        private void UpdateData(Category category)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string updateSql = @"UPDATE dbo.posTbCategory SET Name = @CTGName, Description = @CTGDescription, Status = @CTGStatus WHERE IdCategory = @CTGIdCategory;";

                SqlCommand command = new SqlCommand(updateSql, connection);

                command.Parameters.Add("@CTGIdCategory", SqlDbType.Char).Value = category.Id;
                command.Parameters.Add("@CTGName", SqlDbType.VarChar).Value = category.Name;
                command.Parameters.Add("@CTGDescription", SqlDbType.VarChar).Value = category.Description;
                command.Parameters.Add("@CTGStatus", SqlDbType.Bit).Value = category.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void DeleteData(Category category)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string deleteSql = @"DELETE FROM dbo.posTbCategory WHERE IdCategory = @CTGIdCategory;";

                SqlCommand command = new SqlCommand(deleteSql, connection);

                command.Parameters.Add("@CTGIdCategory", SqlDbType.Char).Value = category.Id;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #endregion

        #region Public Methods

        #region Actions

        public bool Save(Category category)
        {
            ValidationHelper validation = new ValidationHelper();

            bool isSaved = false;
            int retry = 0;

            if (validation.HasNoEquals(category, this._ctgStore))
            {
                if (string.IsNullOrEmpty(category.Id))
                {
                    while ((!isSaved) && (retry < 3))
                    {
                        category.Id = "000000";

                        try
                        {
                            this.InsertData(category);

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
                            this.UpdateData(category);

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
                MessageBox.Show(string.Format("Kategori dengan nama [{0}] telah terdaftar.\n\nPastikan Anda menggunakan nama yang belum terdaftar sebelum melanjutkan proses.", category.Name), "Proses Gagal", MessageBoxButton.OK);

            this.PopulateData();

            return isSaved;
        }

        public void Delete(Category category)
        {
            this.DeleteData(category);

            this.PopulateData();
        }

        #endregion

        #region Filters

        public List<Category> FindByStatus(bool status)
        {
            IEnumerable<Category> found1 = from ctg in this._ctgStore
                                           where (ctg.Status == status)
                                           select ctg;

            return found1.ToList();
        }

        public List<Category> FindAll()
        {
            return new List<Category>(this._ctgStore);
        }

        #endregion

        #endregion
    }
}
