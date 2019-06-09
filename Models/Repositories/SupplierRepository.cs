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
    public class SupplierRepository
    {
        #region Private Fields

        private readonly string _connString;
        private List<Supplier> _splStore;

        #endregion

        #region ctor

        public SupplierRepository()
        {
            this._connString = SqlHelper.GetConnectionString();
            this._splStore = new List<Supplier>();

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

                string selectSql = @"SELECT * FROM dbo.posVwSupplier ORDER BY IdSupplier;";

                SqlCommand command = new SqlCommand(selectSql, connection);

                connection.Open();

                this._splStore.Clear();

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    this._splStore.Add(new Supplier()
                        {
                            Id = (string)dataReader["IdSupplier"],
                            Name = (string)dataReader["Name"],
                            Phone1 = (string)dataReader["Phone1"],
                            Phone2 = (string)dataReader["Phone2"],
                            Email = (string)dataReader["Email"],
                            Address1 = (string)dataReader["Address1"],
                            Address2 = (string)dataReader["Address2"],
                            City = (string)dataReader["City"],
                            Status = (bool)dataReader["Status"]
                        });
                }
            }
        }

        private void InsertData(Supplier supplier)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string insertIntoSql = @"INSERT INTO dbo.posTbSupplier (IdSupplier, Name, Phone1, Phone2, Email, Address1, Address2, City, Status) VALUES (@SPLIdSupplier, @SPLName, @SPLPhone1, @SPLPhone2, @SPLEmail, @SPLAddress1, @SPLAddress2, @SPLCity, @SPLStatus);";

                SqlCommand command = new SqlCommand(insertIntoSql, connection);

                command.Parameters.Add("@SPLIdSupplier", SqlDbType.Char).Value = supplier.Id;
                command.Parameters.Add("@SPLName", SqlDbType.VarChar).Value = supplier.Name;
                command.Parameters.Add("@SPLPhone1", SqlDbType.VarChar).Value = supplier.Phone1;
                command.Parameters.Add("@SPLPhone2", SqlDbType.VarChar).Value = supplier.Phone2;
                command.Parameters.Add("@SPLEmail", SqlDbType.VarChar).Value = supplier.Email;
                command.Parameters.Add("@SPLAddress1", SqlDbType.VarChar).Value = supplier.Address1;
                command.Parameters.Add("@SPLAddress2", SqlDbType.VarChar).Value = supplier.Address2;
                command.Parameters.Add("@SPLCity", SqlDbType.VarChar).Value = supplier.City;
                command.Parameters.Add("@SPLStatus", SqlDbType.Bit).Value = supplier.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void UpdateData(Supplier supplier) 
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string updateSql = @"UPDATE dbo.posTbSupplier SET Name = @SPLName, Phone1 = @SPLPhone1, Phone2 = @SPLPhone2, Email = @SPLEmail, Address1 = @SPLAddress1, Address2 = @SPLAddress2, City = @SPLCity, Status = @SPLStatus WHERE IdSupplier = @SPLIdSupplier;";

                SqlCommand command = new SqlCommand(updateSql, connection);

                command.Parameters.Add("@SPLIdSupplier", SqlDbType.Char).Value = supplier.Id;
                command.Parameters.Add("@SPLName", SqlDbType.VarChar).Value = supplier.Name;
                command.Parameters.Add("@SPLPhone1", SqlDbType.VarChar).Value = supplier.Phone1;
                command.Parameters.Add("@SPLPhone2", SqlDbType.VarChar).Value = supplier.Phone2;
                command.Parameters.Add("@SPLEmail", SqlDbType.VarChar).Value = supplier.Email;
                command.Parameters.Add("@SPLAddress1", SqlDbType.VarChar).Value = supplier.Address1;
                command.Parameters.Add("@SPLAddress2", SqlDbType.VarChar).Value = supplier.Address2;
                command.Parameters.Add("@SPLCity", SqlDbType.VarChar).Value = supplier.City;
                command.Parameters.Add("@SPLStatus", SqlDbType.Bit).Value = supplier.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void DeleteData(Supplier supplier)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string deleteSql = @"DELETE FROM dbo.posTbSupplier WHERE IdSupplier = @SPLIdSupplier;";

                SqlCommand command = new SqlCommand(deleteSql, connection);

                command.Parameters.Add("@SPLIdSupplier", SqlDbType.Char).Value = supplier.Id;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #endregion

        #region Public Methods

        #region Actions

        public bool Save(Supplier supplier)
        {
            ValidationHelper validation = new ValidationHelper();
            
            bool isSaved = false;
            int retry = 0;

            if (validation.HasNoEquals(supplier, this._splStore))
            {
                if (string.IsNullOrEmpty(supplier.Id))
                {
                    while ((!isSaved) && (retry < 3))
                    {
                        supplier.Id = "000000";

                        try
                        {
                            this.InsertData(supplier);

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
                            this.UpdateData(supplier);

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
                MessageBox.Show(string.Format("Supplier dengan nama [{0}] telah terdaftar.\n\nPastikan Anda menggunakan nama yang belum terdaftar sebelum melanjutkan proses.", supplier.Name), "Proses Gagal", MessageBoxButton.OK);

            this.PopulateData();

            return isSaved;
        }

        public void Delete(Supplier supplier)
        {
            this.DeleteData(supplier);

            this.PopulateData();
        }

        #endregion

        #region Filters

        public List<Supplier> FindByName(string name)
        {
            IEnumerable<Supplier> found1 = from spl in this._splStore
                                           where (spl.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                                           select spl;

            return found1.ToList();
        }

        public List<Supplier> FindByNameByStatus(string name, bool status)
        {
            IEnumerable<Supplier> found1 = from spl in this._splStore
                                           where (spl.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                                           select spl;

            IEnumerable<Supplier> found2 = from spl in found1
                                           where (spl.Status == status)
                                           select spl;

            return found2.ToList();
        }

        public List<Supplier> FindByCity(string city)
        {
            IEnumerable<Supplier> found1 = from spl in this._splStore
                                           where (spl.City.StartsWith(city, StringComparison.OrdinalIgnoreCase))
                                           select spl;

            return found1.ToList();
        }

        public List<Supplier> FindByCityByStatus(string city, bool status) 
        {
            IEnumerable<Supplier> found1 = from spl in this._splStore
                                           where (spl.City.StartsWith(city, StringComparison.OrdinalIgnoreCase))
                                           select spl;

            IEnumerable<Supplier> found2 = from spl in found1
                                           where (spl.Status == status)
                                           select spl;

            return found2.ToList();
        }

        public List<Supplier> FindByStatus(bool status)
        {
            IEnumerable<Supplier> found1 = from spl in this._splStore
                                           where (spl.Status == status)
                                           select spl;

            return found1.ToList();
        }

        public List<Supplier> FindAll()
        {
            return new List<Supplier>(this._splStore);
        }

        #endregion

        #endregion
    }
}
