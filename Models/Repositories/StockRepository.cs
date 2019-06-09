using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;
using WelldonePOS.Models.Enums;

namespace WelldonePOS.Models.Repositories
{
    public class StockRepository
    {
        #region Private Fields

        private readonly string _connString;
        private List<Stock> _stkStore;

        #endregion

        #region ctor

        public StockRepository()
        {
            this._connString = SqlHelper.GetConnectionString();
            this._stkStore = new List<Stock>();

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

                string selectSql = @"SELECT * FROM dbo.posVwStock ORDER BY IdStock;";

                SqlCommand command = new SqlCommand(selectSql, connection);

                connection.Open();

                this._stkStore.Clear();

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    this._stkStore.Add(new Stock()
                        {
                            Id = (string)dataReader["IdStock"],
                            ClusterId = (string)dataReader["IdItem"],
                            ReceptionDate = (DateTime)dataReader["ReceptionDate"],
                            Origin = (StockOrigin)dataReader["Origin"],
                            InitialQty = (int)dataReader["InitialQty"],
                            CurrentQty = (int)dataReader["CurrentQty"],
                            PriceOfPurchase = (decimal)dataReader["PriceOfPurchase"],
                            Status = (bool)dataReader["Status"]
                        });
                }
            }
        }

        private void InsertData(Stock stock)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string insertIntoSql = @"INSERT INTO dbo.posTbStock (IdStock, IdItem, ReceptionDate, Origin, InitialQty, CurrentQty, PriceOfPurchase, Status) VALUES (@STKIdStock, @STKClusterId, @STKReceptionDate, @STKOrigin, @STKInitialQty, @STKCurrentQty, @STKPriceOfPurchase, @STKStatus);";

                SqlCommand command = new SqlCommand(insertIntoSql, connection);

                command.Parameters.Add("@STKIdStock", SqlDbType.Char).Value = stock.Id;
                command.Parameters.Add("@STKClusterId", SqlDbType.Char).Value = stock.ClusterId;
                command.Parameters.Add("@STKReceptionDate", SqlDbType.DateTime).Value = stock.ReceptionDate;
                command.Parameters.Add("@STKOrigin", SqlDbType.Int).Value = (int)stock.Origin;
                command.Parameters.Add("@STKInitialQty", SqlDbType.Int).Value = stock.InitialQty;
                command.Parameters.Add("@STKCurrentQty", SqlDbType.Int).Value = stock.CurrentQty;
                command.Parameters.Add("@STKPriceOfPurchase", SqlDbType.Decimal).Value = stock.PriceOfPurchase;
                command.Parameters.Add("@STKStatus", SqlDbType.Bit).Value = stock.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void UpdateData(Stock stock)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string updateSql = @"UPDATE dbo.posTbStock SET ReceptionDate = @STKReceptionDate, Origin = @STKOrigin, InitialQty = @STKInitialQty, CurrentQty = @STKCurrentQty, PriceOfPurchase = @STKPriceOfPurchase, Status = @STKStatus WHERE IdStock = @STKIdStock;";

                SqlCommand command = new SqlCommand(updateSql, connection);

                command.Parameters.Add("@STKIdStock", SqlDbType.Char).Value = stock.Id;
                command.Parameters.Add("@STKReceptionDate", SqlDbType.DateTime).Value = stock.ReceptionDate;
                command.Parameters.Add("@STKOrigin", SqlDbType.Int).Value = (int)stock.Origin;
                command.Parameters.Add("@STKInitialQty", SqlDbType.Int).Value = stock.InitialQty;
                command.Parameters.Add("@STKCurrentQty", SqlDbType.Int).Value = stock.CurrentQty;
                command.Parameters.Add("@STKPriceOfPurchase", SqlDbType.Decimal).Value = stock.PriceOfPurchase;
                command.Parameters.Add("@STKStatus", SqlDbType.Bit).Value = stock.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void DeleteData(Stock stock)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string deleteSql = @"DELETE FROM dbo.posTbStock WHERE IdStock = @STKIdStock;";

                SqlCommand command = new SqlCommand(deleteSql, connection);

                command.Parameters.Add("@STKIdStock", SqlDbType.Char).Value = stock.Id;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #endregion

        #region Public Methods

        #region Actions

        public bool Save(Stock stock)
        {
            bool isSaved = false;
            int retry = 0;

            if (string.IsNullOrEmpty(stock.Id))
            {
                while ((!isSaved) && (retry < 3))
                {
                    stock.Id = "000000";

                    try
                    {
                        this.InsertData(stock);

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
                this.UpdateData(stock);

                isSaved = true;
            }

            if (!isSaved)
                MessageBox.Show("Terjadi kesalahan program. Silahkan coba beberapa saat lagi.", "Proses Gagal", MessageBoxButton.OK);

            this.PopulateData();

            return isSaved;
        }

        public void Delete(Stock stock)
        {
            this.DeleteData(stock);

            this.PopulateData();
        }

        #endregion

        #region Filters

        public List<Stock> FindByClusterId(string clusterId)
        {
            IEnumerable<Stock> found1 = from stk in this._stkStore
                                        where (stk.ClusterId == clusterId)
                                        select stk;

            return found1.ToList();
        }

        public List<Stock> FindByStatus(bool status)
        {
            IEnumerable<Stock> found1 = from stk in this._stkStore
                                        where (stk.Status == status)
                                        select stk;

            return found1.ToList();
        }

        public List<Stock> FindAll()
        {
            return new List<Stock>(this._stkStore);
        }

        #endregion

        #endregion
    }
}
