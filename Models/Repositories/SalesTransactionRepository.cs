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
    public class SalesTransactionRepository
    {
        #region Private Fields

        private readonly string _connString;
        private List<SalesTransaction> _sltStore;

        private readonly UserRepository _usrRepository;
        private readonly CustomerRepository _cstRepository;
        private readonly SalesHistoryRepository _slhRepository;

        #endregion

        #region ctor

        public SalesTransactionRepository()
        {
            this._connString = SqlHelper.GetConnectionString();
            this._sltStore = new List<SalesTransaction>();

            this._usrRepository = new UserRepository();
            this._cstRepository = new CustomerRepository();
            this._slhRepository = new SalesHistoryRepository();

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

                string selectSql = @"SELECT * FROM dbo.posVwSalesTransaction ORDER BY IdSalesTransaction;";

                SqlCommand command = new SqlCommand(selectSql, connection);

                connection.Open();

                this._sltStore.Clear();

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    this._sltStore.Add(new SalesTransaction()
                        {
                            Id = (string)dataReader["IdSalesTransaction"],
                            Code = (string)dataReader["Code"],
                            TransactionDate = (DateTime)dataReader["TransactionDate"],
                            Seller = this.FindSeller((string)dataReader["IdUser"]),
                            Consumer = this.FindConsumer((string)dataReader["IdCustomer"]),
                            SalesDetail = this.FindSalesDetail((string)dataReader["IdSalesTransaction"]),
                            SumTotal = (decimal)dataReader["SumTotal"],
                            Status = (bool)dataReader["Status"]
                        });
                }
            }
        }

        private void InsertData(SalesTransaction salesTransaction)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string insertIntoSql = @"INSERT INTO dbo.posTbSalesTransaction (IdSalesTransaction, Code, TransactionDate, IdUser, IdCustomer, SumTotal, Status) VALUES (@SLTIdSalesTransaction, @SLTCode, @SLTTransactionDate, @SLTIdUser, @SLTIdCustomer, @SLTSumTotal, @SLTStatus);";

                SqlCommand command = new SqlCommand(insertIntoSql, connection);

                command.Parameters.Add("@SLTIdSalesTransaction", SqlDbType.Char).Value = salesTransaction.Id;
                command.Parameters.Add("@SLTCode", SqlDbType.VarChar).Value = salesTransaction.Code;
                command.Parameters.Add("@SLTTransactionDate", SqlDbType.DateTime).Value = salesTransaction.TransactionDate;
                command.Parameters.Add("@SLTIdUser", SqlDbType.Char).Value = salesTransaction.Seller.Id;
                command.Parameters.Add("@SLTIdCustomer", SqlDbType.Char).Value = salesTransaction.Consumer.Id;
                command.Parameters.Add("@SLTSumTotal", SqlDbType.Decimal).Value = salesTransaction.SumTotal;
                command.Parameters.Add("@SLTStatus", SqlDbType.Bit).Value = salesTransaction.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void UpdateData(SalesTransaction salesTransaction)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string insertIntoSql = @"UPDATE dbo.posTbSalesTransaction SET Code = @SLTCode, TransactionDate = @SLTTransactionDate, IdUser = @SLTIdUser, IdCustomer = @SLTIdCustomer, SumTotal = @SLTSumTotal, Status = @SLTStatus WHERE IdSalesTransaction = @SLTIdSalesTransaction;";

                SqlCommand command = new SqlCommand(insertIntoSql, connection);

                command.Parameters.Add("@SLTIdSalesTransaction", SqlDbType.Char).Value = salesTransaction.Id;
                command.Parameters.Add("@SLTCode", SqlDbType.VarChar).Value = salesTransaction.Code;
                command.Parameters.Add("@SLTTransactionDate", SqlDbType.DateTime).Value = salesTransaction.TransactionDate;
                command.Parameters.Add("@SLTIdUser", SqlDbType.Char).Value = salesTransaction.Seller.Id;
                command.Parameters.Add("@SLTIdCustomer", SqlDbType.Char).Value = salesTransaction.Consumer.Id;
                command.Parameters.Add("@SLTSumTotal", SqlDbType.Decimal).Value = salesTransaction.SumTotal;
                command.Parameters.Add("@SLTStatus", SqlDbType.Bit).Value = salesTransaction.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void DeleteData(SalesTransaction salesTransaction)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string deleteSql = @"DELETE FROM dbo.posTbSalesTransaction WHERE IdSalesTransaction = @SLTIdSalesTransaction;";

                SqlCommand command = new SqlCommand(deleteSql, connection);

                command.Parameters.Add("@SLTIdSalesTransaction", SqlDbType.Char).Value = salesTransaction.Id;

                connection.Open();

                command.ExecuteNonQuery();
            }

            for (int i = 0; i < salesTransaction.SalesDetail.Count; i++)
            {
                this._slhRepository.Delete(salesTransaction.SalesDetail[i]);
            }
        }

        #endregion

        #region etc

        private User FindSeller(string id)
        {
            List<User> usrStore = new List<User>(this._usrRepository.FindAll());

            for (int i = 0; i < usrStore.Count; i++)
            {
                if (usrStore[i].Id == id)
                    return usrStore[i];
            }

            return new User();
        }

        private Customer FindConsumer(string id)
        {
            List<Customer> cstStore = new List<Customer>(this._cstRepository.FindAll());

            for (int i = 0; i < cstStore.Count; i++)
            {
                if (cstStore[i].Id == id)
                    return cstStore[i];
            }

            return new Customer();
        }

        private List<SalesHistory> FindSalesDetail(string clusterId)
        {
            return this._slhRepository.FindByClusterId(clusterId);
        }

        #endregion

        #endregion

        #region Public Methods

        #region Actions

        public bool Save(SalesHistory salesHistory)
        {
            bool isSaved = false;

            if (this._slhRepository.Save(salesHistory))
                isSaved = true;

            this.PopulateData();

            return isSaved;
        }

        public bool Save(SalesTransaction salesTransaction)
        {
            ValidationHelper validation = new ValidationHelper();

            bool isSaved = false;
            int retry = 0;

            if (validation.HasNoEquals(salesTransaction, this._sltStore))
            {
                if (string.IsNullOrEmpty(salesTransaction.Id))
                {
                    while ((!isSaved) && (retry < 3))
                    {
                        salesTransaction.Id = "000000";

                        try
                        {
                            this.InsertData(salesTransaction);

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
                            this.UpdateData(salesTransaction);

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
                MessageBox.Show(string.Format("Transaksi penjualan dengan nomor [{0}] telah terdaftar.\n\nPastikan Anda menggunakan nama yang belum terdaftar sebelum melanjutkan proses.", salesTransaction.Code), "Proses Gagal", MessageBoxButton.OK);

            this.PopulateData();

            return isSaved;
        }

        public void Delete(SalesHistory salesHistory)
        {
            this._slhRepository.Delete(salesHistory);

            this.PopulateData();
        }

        public void Delete(SalesTransaction salesTransaction)
        {
            this.DeleteData(salesTransaction);

            this.PopulateData();
        }

        #endregion

        #region Filters

        public List<SalesTransaction> FindByStatus(bool status)
        {
            IEnumerable<SalesTransaction> found1 = from slt in this._sltStore
                                                   where (slt.Status == status)
                                                   select slt;

            return found1.ToList();
        }

        public List<SalesHistory> FindAllHistory()
        {
            return this._slhRepository.FindAll();
        }

        public List<SalesTransaction> FindAllTransaction()
        {
            return new List<SalesTransaction>(this._sltStore);
        }

        #endregion

        #endregion
    }
}
