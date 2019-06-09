using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;

namespace WelldonePOS.Models.Repositories
{
    public class SalesHistoryRepository
    {
        #region Private Fields

        private readonly string _connString;
        private List<SalesHistory> _slhStore;

        private readonly ItemRepository _itmRepository;
        private readonly UnitOfSalesRepository _untRepository;

        #endregion

        #region ctor

        public SalesHistoryRepository()
        {
            this._connString = SqlHelper.GetConnectionString();
            this._slhStore = new List<SalesHistory>();

            this._itmRepository = new ItemRepository();
            this._untRepository = new UnitOfSalesRepository();

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

                string selectSql = @"SELECT * FROM dbo.posVwSalesHistory ORDER BY IdSalesHistory;";

                SqlCommand command = new SqlCommand(selectSql, connection);

                connection.Open();

                this._slhStore.Clear();

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    this._slhStore.Add(new SalesHistory()
                        {
                            Id = (string)dataReader["IdSalesHistory"],
                            ClusterId = (string)dataReader["IdSalesTransaction"],
                            SoldItem = this.FindSoldItem((string)dataReader["IdItem"]),
                            SoldUnit = this.FindSoldUnit((string)dataReader["IdUnit"]),
                            SoldPrice = (decimal)dataReader["SoldPrice"],
                            SoldQty = (int)dataReader["SoldQty"],
                            SubTotal = (decimal)dataReader["SubTotal"],
                            Status = (bool)dataReader["Status"]
                        });
                }
            }
        }

        private void InsertData(SalesHistory salesHistory)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string insertIntoSql = @"INSERT INTO dbo.posTbSalesHistory (IdSalesHistory, IdSalesTransaction, IdItem, IdUnit, SoldPrice, SoldQty, SubTotal, Status) VALUES (@SLHIdSalesHistory, @SLHIdSalesTransaction, @SLHIdItem, @SLHIdUnit, @SLHSoldPrice, @SLHSoldQty, @SLHSubTotal, @SLHStatus);";

                SqlCommand command = new SqlCommand(insertIntoSql, connection);

                command.Parameters.Add("@SLHIdSalesHistory", SqlDbType.Char).Value = salesHistory.Id;
                command.Parameters.Add("@SLHIdSalesTransaction", SqlDbType.Char).Value = salesHistory.ClusterId;
                command.Parameters.Add("@SLHIdItem", SqlDbType.Char).Value = salesHistory.SoldItem.Id;
                command.Parameters.Add("@SLHIdUnit", SqlDbType.Char).Value = salesHistory.SoldUnit.Id;
                command.Parameters.Add("@SLHSoldPrice", SqlDbType.Decimal).Value = salesHistory.SoldPrice;
                command.Parameters.Add("@SLHSoldQty", SqlDbType.Int).Value = salesHistory.SoldQty;
                command.Parameters.Add("@SLHSubTotal", SqlDbType.Decimal).Value = salesHistory.SubTotal;
                command.Parameters.Add("@SLHStatus", SqlDbType.Bit).Value = salesHistory.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void UpdateData(SalesHistory salesHistory) 
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string insertIntoSql = @"UPDATE dbo.posTbSalesHistory SET IdItem = @SLHIdItem, IdUnit = @SLHIdUnit, SoldPrice = @SLHSoldPrice, SoldQty = @SLHSoldQty, SubTotal = @SLHSubTotal, Status = @SLHStatus WHERE IdSalesHistory = @SLHIdSalesHistory;";

                SqlCommand command = new SqlCommand(insertIntoSql, connection);

                command.Parameters.Add("@SLHIdSalesHistory", SqlDbType.Char).Value = salesHistory.Id;
                command.Parameters.Add("@SLHIdItem", SqlDbType.Char).Value = salesHistory.SoldItem.Id;
                command.Parameters.Add("@SLHIdUnit", SqlDbType.Char).Value = salesHistory.SoldUnit.Id;
                command.Parameters.Add("@SLHSoldPrice", SqlDbType.Decimal).Value = salesHistory.SoldPrice;
                command.Parameters.Add("@SLHSoldQty", SqlDbType.Int).Value = salesHistory.SoldQty;
                command.Parameters.Add("@SLHSubTotal", SqlDbType.Decimal).Value = salesHistory.SubTotal;
                command.Parameters.Add("@SLHStatus", SqlDbType.Bit).Value = salesHistory.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void DeleteData(SalesHistory salesHistory)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string deleteSql = @"DELETE FROM dbo.posTbSalesHistory WHERE IdSalesHistory = @SLHIdSalesHistory;";

                SqlCommand command = new SqlCommand(deleteSql, connection);

                command.Parameters.Add("@SLHIdSalesHistory", SqlDbType.Char).Value = salesHistory.Id;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region etc

        private Item FindSoldItem(string id)
        {
            List<Item> itemStore = new List<Item>(this._itmRepository.FindAllItem());

            for (int i = 0; i < itemStore.Count; i++)
            {
                if (itemStore[i].Id == id)
                    return itemStore[i];
            }

            return new Item();
        }

        private UnitOfSales FindSoldUnit(string id)
        {
            List<UnitOfSales> unitStore = new List<UnitOfSales>(this._untRepository.FindAll());

            for (int i = 0; i < unitStore.Count; i++)
            {
                if (unitStore[i].Id == id)
                    return unitStore[i];
            }

            return new UnitOfSales();
        }

        #endregion

        #endregion

        #region Public Methods

        #region Actions

        public bool Save(SalesHistory salesHistory)
        {
            ValidationHelper validation = new ValidationHelper();

            bool isSaved = false;
            int retry = 0;

            if (string.IsNullOrEmpty(salesHistory.Id))
            {
                while ((!isSaved) && (retry < 3))
                {
                    salesHistory.Id = "000000";

                    try
                    {
                        this.InsertData(salesHistory);

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
                        this.UpdateData(salesHistory);

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

            this.PopulateData();

            return isSaved;
        }

        public void Delete(SalesHistory salesHistory)
        {
            this.DeleteData(salesHistory);

            this.PopulateData();
        }

        #endregion

        #region Filters

        public List<SalesHistory> FindByClusterId(string clusterId)
        {
            IEnumerable<SalesHistory> found1 = from slh in this._slhStore
                                               where (slh.ClusterId == clusterId)
                                               select slh;

            return found1.ToList();
        }

        public List<SalesHistory> FindByStatus(bool status)
        {
            IEnumerable<SalesHistory> found1 = from slh in this._slhStore
                                               where (slh.Status == status)
                                               select slh;

            return found1.ToList();
        }

        public List<SalesHistory> FindAll()
        {
            return new List<SalesHistory>(this._slhStore);
        }

        #endregion

        #endregion
    }
}
