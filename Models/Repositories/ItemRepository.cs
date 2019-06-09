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
    public class ItemRepository
    {
        #region Private Fields

        private readonly string _connString;
        private List<Item> _itmStore;

        private readonly CategoryRepository _ctgRepository;
        private readonly StockRepository _stkRepository;
        private readonly UnitOfPurchaseRepository _uopRepository;
        private readonly UnitOfSalesRepository _uosRepository;

        #endregion

        #region ctor

        public ItemRepository()
        {
            this._connString = SqlHelper.GetConnectionString();
            this._itmStore = new List<Item>();

            this._ctgRepository = new CategoryRepository();
            this._stkRepository = new StockRepository();
            this._uopRepository = new UnitOfPurchaseRepository();
            this._uosRepository = new UnitOfSalesRepository();

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

                string selectSql = @"SELECT * FROM dbo.posVwItem ORDER BY IdItem;";

                SqlCommand command = new SqlCommand(selectSql, connection);

                connection.Open();

                this._itmStore.Clear();

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    this._itmStore.Add(new Item()
                        {
                            Id = (string)dataReader["IdItem"],
                            Name = (string)dataReader["Name"],
                            Group = this.FindGroup((string)dataReader["IdCategory"]),
                            RefUnit = (string)dataReader["RefUnit"],
                            MinimumProfit = (decimal)dataReader["MinimumProfit"],
                            SafetyStockPortion = (int)dataReader["SafetyStockPortion"],
                            Stocks = this.FindStocks((string)dataReader["IdItem"]),
                            UnitsOfPurchase = this.FindUnitsOfPurchase((string)dataReader["IdItem"]),
                            UnitsOfSales = this.FindUnitsOfSales((string)dataReader["IdItem"]),
                            Status = (bool)dataReader["Status"]
                        });
                }
            }
        }

        private void InsertData(Item item)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string insertIntoSql = @"INSERT INTO dbo.posTbItem (IdItem, Name, IdCategory, RefUnit, MinimumProfit, SafetyStockPortion, Status) VALUES (@ITMIdItem, @ITMName, @ITMIdCategory, @ITMRefUnit, @ITMMinimumProfit, @ITMSafetyStockPortion, @ITMStatus);";

                SqlCommand command = new SqlCommand(insertIntoSql, connection);

                command.Parameters.Add("@ITMIdItem", SqlDbType.Char).Value = item.Id;
                command.Parameters.Add("@ITMName", SqlDbType.VarChar).Value = item.Name;
                command.Parameters.Add("@ITMIdCategory", SqlDbType.Char).Value = item.Group.Id;
                command.Parameters.Add("@ITMRefUnit", SqlDbType.VarChar).Value = item.RefUnit;
                command.Parameters.Add("@ITMMinimumProfit", SqlDbType.Decimal).Value = item.MinimumProfit;
                command.Parameters.Add("@ITMSafetyStockPortion", SqlDbType.Int).Value = item.SafetyStockPortion;
                command.Parameters.Add("@ITMStatus", SqlDbType.Bit).Value = item.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void UpdateData(Item item)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string updateSql = @"UPDATE dbo.posTbItem SET Name = @ITMName, IdCategory = @ITMIdCategory, RefUnit = @ITMRefUnit, MinimumProfit = @ITMMinimumProfit, SafetyStockPortion = @ITMSafetyStockPortion, Status = @ITMStatus WHERE IdItem = @ITMIdItem;";

                SqlCommand command = new SqlCommand(updateSql, connection);

                command.Parameters.Add("@ITMIdItem", SqlDbType.Char).Value = item.Id;
                command.Parameters.Add("@ITMName", SqlDbType.VarChar).Value = item.Name;
                command.Parameters.Add("@ITMIdCategory", SqlDbType.Char).Value = item.Group.Id;
                command.Parameters.Add("@ITMRefUnit", SqlDbType.VarChar).Value = item.RefUnit;
                command.Parameters.Add("@ITMMinimumProfit", SqlDbType.Decimal).Value = item.MinimumProfit;
                command.Parameters.Add("@ITMSafetyStockPortion", SqlDbType.Int).Value = item.SafetyStockPortion;
                command.Parameters.Add("@ITMStatus", SqlDbType.Bit).Value = item.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void DeleteData(Item item)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string deleteSql = @"DELETE FROM dbo.posTbItem WHERE IdItem = @ITMIdItem;";

                SqlCommand command = new SqlCommand(deleteSql, connection);

                command.Parameters.Add("@ITMIdItem", SqlDbType.Char).Value = item.Id;

                connection.Open();

                command.ExecuteNonQuery();
            }

            for (int i = 0; i < item.Stocks.Count; i++)
            {
                this._stkRepository.Delete(item.Stocks[i]);
            }

            for (int i = 0; i < item.UnitsOfPurchase.Count; i++)
            {
                this._uopRepository.Delete(item.UnitsOfPurchase[i]);
            }

            for (int i = 0; i < item.UnitsOfSales.Count; i++)
            {
                this._uosRepository.Delete(item.UnitsOfSales[i]);
            }
        }

        #endregion

        #region etc

        private Category FindGroup(string id)
        {
            List<Category> ctgStore = new List<Category>(this._ctgRepository.FindAll());

            for (int i = 0; i < ctgStore.Count; i++)
            {
                if (ctgStore[i].Id == id)
                    return ctgStore[i];
            }

            return new Category();
        }

        private List<Stock> FindStocks(string clusterId)
        {
            return this._stkRepository.FindByClusterId(clusterId);
        }

        private List<UnitOfPurchase> FindUnitsOfPurchase(string clusterId)
        {
            return this._uopRepository.FindByClusterId(clusterId);
        }

        private List<UnitOfSales> FindUnitsOfSales(string clusterId)
        {
            return this._uosRepository.FindByClusterId(clusterId);
        }

        #endregion

        #endregion

        #region Public Methods

        #region Actions

        public bool Save(Stock stock)
        {
            bool isSaved = false;

            if (this._stkRepository.Save(stock))
                isSaved = true;

            this.PopulateData();

            return isSaved;
        }

        public bool Save(UnitOfPurchase unitOfPurchase, ObservableCollection<UnitOfPurchase> clusterMembers)
        {
            bool isSaved = false;

            if (this._uopRepository.Save(unitOfPurchase, clusterMembers))
                isSaved = true;

            this.PopulateData();

            return isSaved;
        }

        public bool Save(UnitOfSales unitOfSales, ObservableCollection<UnitOfSales> clusterMembers)
        {
            bool isSaved = false;

            if (this._uosRepository.Save(unitOfSales, clusterMembers))
                isSaved = true;

            this.PopulateData();

            return isSaved;
        }

        public bool Save(Item item)
        {
            ValidationHelper validation = new ValidationHelper();
            
            bool isSaved = false;
            int retry = 0;

            if (validation.HasNoEquals(item, this._itmStore))
            {
                if (string.IsNullOrEmpty(item.Id))
                {
                    while ((!isSaved) && (retry < 3))
                    {
                        item.Id = "000000";

                        try
                        {
                            this.InsertData(item);

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
                            this.UpdateData(item);

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
                MessageBox.Show(string.Format("Barang dengan nama [{0}] telah terdaftar.\n\nPastikan Anda menggunakan nama yang belum terdaftar sebelum melanjutkan proses.", item.Name), "Proses Gagal", MessageBoxButton.OK);

            this.PopulateData();

            return isSaved;
        }

        public void Delete(Stock stock)
        {
            this._stkRepository.Delete(stock);

            this.PopulateData();
        }

        public void Delete(UnitOfPurchase unitOfPurchase)
        {
            this._uopRepository.Delete(unitOfPurchase);

            this.PopulateData();
        }

        public void Delete(UnitOfSales unitOfSales)
        {
            this._uosRepository.Delete(unitOfSales);

            this.PopulateData();
        }

        public void Delete(Item item)
        {
            this.DeleteData(item);

            this.PopulateData();
        }

        #endregion

        #region Filters

        public List<Item> FindByName(string name)
        {
            IEnumerable<Item> found1 = from itm in this._itmStore
                                       where (itm.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                                       select itm;

            return found1.ToList();
        }

        public List<Item> FindByNameByStatus(string name, bool status)
        {
            IEnumerable<Item> found1 = from itm in _itmStore
                                       where (itm.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                                       select itm;

            IEnumerable<Item> found2 = from itm in found1
                                       where (itm.Status == status)
                                       select itm;

            return found2.ToList();
        }

        public List<Item> FindByNameByStatusByObjective(string name, bool status, string objective)
        {
            IEnumerable<Item> found1 = from itm in _itmStore
                                       where (itm.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                                       select itm;

            IEnumerable<Item> found2 = from itm in found1
                                       where (itm.Status == status)
                                       select itm;

            IEnumerable<Item> found3 = found2;

            switch (objective)
            {
                case "Sales":
                    found3 = from itm in found2
                                               where (itm.UnitsOfSales.Count > 0)
                                               select itm;
                    break;
                case "Purchase":
                    found3 = from itm in found2
                                               where (itm.UnitsOfPurchase.Count > 0)
                                               select itm;
                    break;
            }

            return found3.ToList();
        }

        public List<Item> FindByGroup(string groupName)
        {
            IEnumerable<Item> found1 = from itm in this._itmStore
                                       where (itm.Group.Name.StartsWith(groupName, StringComparison.OrdinalIgnoreCase))
                                       select itm;

            return found1.ToList();
        }

        public List<Item> FindByGroupByStatus(string groupName, bool status)
        {
            IEnumerable<Item> found1 = from itm in this._itmStore
                                       where (itm.Group.Name.StartsWith(groupName, StringComparison.OrdinalIgnoreCase))
                                       select itm;

            IEnumerable<Item> found2 = from itm in found1
                                       where (itm.Status == status)
                                       select itm;

            return found2.ToList();
        }

        public List<Item> FindByRefUnit(string refUnit)
        {
            IEnumerable<Item> found1 = from itm in this._itmStore
                                       where (itm.RefUnit.StartsWith(refUnit, StringComparison.OrdinalIgnoreCase))
                                       select itm;

            return found1.ToList();
        }

        public List<Item> FindByRefUnitByStatus(string refUnit, bool status)
        {
            IEnumerable<Item> found1 = from itm in this._itmStore
                                       where (itm.RefUnit.StartsWith(refUnit, StringComparison.OrdinalIgnoreCase))
                                       select itm;

            IEnumerable<Item> found2 = from itm in found1
                                       where (itm.Status == status)
                                       select itm;

            return found2.ToList();
        }

        public List<Item> FindByStatus(bool status)
        {
            IEnumerable<Item> found1 = from itm in this._itmStore
                                       where (itm.Status == status)
                                       select itm;

            return found1.ToList();
        }

        public List<UnitOfSales> FindAllUnit()
        {
            return this._uosRepository.FindAll();
        }

        public List<Item> FindAllItem()
        {
            return new List<Item>(this._itmStore);
        }

        #endregion

        #endregion
    }
}
