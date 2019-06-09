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
    public class CustomerRepository
    {
        #region Private Fields

        private readonly string _connString;
        private List<Customer> _cstStore;

        #endregion

        #region ctor

        public CustomerRepository()
        {
            this._connString = SqlHelper.GetConnectionString();
            this._cstStore = new List<Customer>();

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

                string selectSql = @"SELECT * FROM dbo.posVwCustomer ORDER BY IdCustomer;";

                SqlCommand command = new SqlCommand(selectSql, connection);

                connection.Open();

                this._cstStore.Clear();

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    this._cstStore.Add(new Customer()
                        {
                            Id = (string)dataReader["IdCustomer"],
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

        private void InsertData(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string insertIntoSql = @"INSERT INTO dbo.posTbCustomer (IdCustomer, Name, Phone1, Phone2, Email, Address1, Address2, City, Status) VALUES (@CSTIdCustomer, @CSTName, @CSTPhone1, @CSTPhone2, @CSTEmail, @CSTAddress1, @CSTAddress2, @CSTCity, @CSTStatus);";

                SqlCommand command = new SqlCommand(insertIntoSql, connection);

                command.Parameters.Add("@CSTIdCustomer", SqlDbType.Char).Value = customer.Id;
                command.Parameters.Add("@CSTName", SqlDbType.VarChar).Value = customer.Name;
                command.Parameters.Add("@CSTPhone1", SqlDbType.VarChar).Value = customer.Phone1;
                command.Parameters.Add("@CSTPhone2", SqlDbType.VarChar).Value = customer.Phone2;
                command.Parameters.Add("@CSTEmail", SqlDbType.VarChar).Value = customer.Email;
                command.Parameters.Add("@CSTAddress1", SqlDbType.VarChar).Value = customer.Address1;
                command.Parameters.Add("@CSTAddress2", SqlDbType.VarChar).Value = customer.Address2;
                command.Parameters.Add("@CSTCity", SqlDbType.VarChar).Value = customer.City;
                command.Parameters.Add("@CSTStatus", SqlDbType.Bit).Value = customer.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void UpdateData(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string updateSql = @"UPDATE dbo.posTbCustomer SET Name = @CSTName, Phone1 = @CSTPhone1, Phone2 = @CSTPhone2, Email = @CSTEmail, Address1 = @CSTAddress1, Address2 = @CSTAddress2, City = @CSTCity, Status = @CSTStatus WHERE IdCustomer = @CSTIdCustomer;";

                SqlCommand command = new SqlCommand(updateSql, connection);

                command.Parameters.Add("@CSTIdCustomer", SqlDbType.Char).Value = customer.Id;
                command.Parameters.Add("@CSTName", SqlDbType.VarChar).Value = customer.Name;
                command.Parameters.Add("@CSTPhone1", SqlDbType.VarChar).Value = customer.Phone1;
                command.Parameters.Add("@CSTPhone2", SqlDbType.VarChar).Value = customer.Phone2;
                command.Parameters.Add("@CSTEmail", SqlDbType.VarChar).Value = customer.Email;
                command.Parameters.Add("@CSTAddress1", SqlDbType.VarChar).Value = customer.Address1;
                command.Parameters.Add("@CSTAddress2", SqlDbType.VarChar).Value = customer.Address2;
                command.Parameters.Add("@CSTCity", SqlDbType.VarChar).Value = customer.City;
                command.Parameters.Add("@CSTStatus", SqlDbType.Bit).Value = customer.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void DeleteData(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string deleteSql = @"DELETE FROM dbo.posTbCustomer WHERE IdCustomer = @CSTIdCustomer;";

                SqlCommand command = new SqlCommand(deleteSql, connection);

                command.Parameters.Add("@CSTIdCustomer", SqlDbType.Char).Value = customer.Id;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #endregion

        #region Public Methods

        #region Actions

        public bool Save(Customer customer)
        {
            ValidationHelper validation = new ValidationHelper();

            bool isSaved = false;
            int retry = 0;

            if (validation.HasNoEquals(customer, this._cstStore))
            {
                if (string.IsNullOrEmpty(customer.Id))
                {
                    while ((!isSaved) && (retry < 3))
                    {
                        customer.Id = "000000";

                        try
                        {
                            this.InsertData(customer);

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
                            this.UpdateData(customer);

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
                MessageBox.Show(string.Format("Customer dengan nama [{0}] telah terdaftar.\n\nPastikan Anda menggunakan nama yang belum terdaftar sebelum melanjutkan proses.", customer.Name), "Proses Gagal", MessageBoxButton.OK);

            this.PopulateData();

            return isSaved;
        }

        public void Delete(Customer customer)
        {
            this.DeleteData(customer);

            this.PopulateData();
        }

        #endregion

        #region Filters

        public List<Customer> FindByName(string name)
        {
            IEnumerable<Customer> found1 = from cst in this._cstStore
                                           where (cst.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                                           select cst;

            return found1.ToList();
        }

        public List<Customer> FindByNameByStatus(string name, bool status)
        {
            IEnumerable<Customer> found1 = from cst in this._cstStore
                                           where (cst.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                                           select cst;

            IEnumerable<Customer> found2 = from cst in found1
                                           where (cst.Status == status)
                                           select cst;

            return found2.ToList();
        }

        public List<Customer> FindByCity(string city)
        {
            IEnumerable<Customer> found1 = from cst in this._cstStore
                                           where (cst.City.StartsWith(city, StringComparison.OrdinalIgnoreCase))
                                           select cst;

            return found1.ToList();
        }

        public List<Customer> FindByCityByStatus(string city, bool status)
        {
            IEnumerable<Customer> found1 = from cst in this._cstStore
                                           where (cst.City.StartsWith(city, StringComparison.OrdinalIgnoreCase))
                                           select cst;

            IEnumerable<Customer> found2 = from cst in found1
                                           where (cst.Status == status)
                                           select cst;

            return found2.ToList();
        }

        public List<Customer> FindByStatus(bool status)
        {
            IEnumerable<Customer> found1 = from cst in this._cstStore
                                           where (cst.Status == status)
                                           select cst;

            return found1.ToList();
        }

        public List<Customer> FindAll()
        {
            return new List<Customer>(this._cstStore);
        }

        #endregion

        #endregion
    }
}
