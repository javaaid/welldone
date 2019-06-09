using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;

namespace WelldonePOS.Models.Repositories
{
    public class UnitOfSalesRepository
    {
        #region Private Fields

        private readonly string _connString;
        private List<UnitOfSales> _uosStore;

        #endregion

        #region ctor

        public UnitOfSalesRepository()
        {
            this._connString = SqlHelper.GetConnectionString();
            this._uosStore = new List<UnitOfSales>();

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

                string selectSql = @"SELECT * FROM dbo.posVwUnitOfSales ORDER BY IdUnitOfSales;";

                SqlCommand command = new SqlCommand(selectSql, connection);

                connection.Open();

                this._uosStore.Clear();
                
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    this._uosStore.Add(new UnitOfSales()
                        {
                            Id = (string)dataReader["IdUnitOfSales"],
                            ClusterId = (string)dataReader["IdItem"],
                            Barcode = (string)dataReader["Barcode"],
                            Code = (string)dataReader["Code"],
                            Name = (string)dataReader["Name"],
                            QtyPerUOS = (int)dataReader["QtyPerUOS"],
                            DefaultPriceOfSales = (decimal)dataReader["DefaultPriceOfSales"],
                            Status = (bool)dataReader["Status"]
                        });
                }
            }
        }

        private void InsertData(UnitOfSales unitOfSales)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string insertIntoSql = @"INSERT INTO dbo.posTbUnitOfSales (IdUnitOfSales, IdItem, Barcode, Code, Name, QtyPerUOS, DefaultPriceOfSales, Status) VALUES (@UOSIdUnitOfSales, @UOSClusterId, @UOSBarcode, @UOSCode, @UOSName, @UOSQtyPerUOS, @UOSDefaultPriceOfSales, @UOSStatus);";

                SqlCommand command = new SqlCommand(insertIntoSql, connection);

                command.Parameters.Add("@UOSIdUnitOfSales", SqlDbType.Char).Value = unitOfSales.Id;
                command.Parameters.Add("@UOSClusterId", SqlDbType.Char).Value = unitOfSales.ClusterId;
                command.Parameters.Add("@UOSBarcode", SqlDbType.VarChar).Value = unitOfSales.Barcode;
                command.Parameters.Add("@UOSCode", SqlDbType.VarChar).Value = unitOfSales.Code;
                command.Parameters.Add("@UOSName", SqlDbType.VarChar).Value = unitOfSales.Name;
                command.Parameters.Add("@UOSQtyPerUOS", SqlDbType.Int).Value = unitOfSales.QtyPerUOS;
                command.Parameters.Add("@UOSDefaultPriceOfSales", SqlDbType.Decimal).Value = unitOfSales.DefaultPriceOfSales;
                command.Parameters.Add("@UOSStatus", SqlDbType.Bit).Value = unitOfSales.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void UpdateData(UnitOfSales unitOfSales)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string updateSql = @"UPDATE dbo.posTbUnitOfSales SET Barcode = @UOSBarcode, Code = @UOSCode, Name = @UOSName, QtyPerUOS = @UOSQtyPerUOS, DefaultPriceOfSales = @UOSDefaultPriceOfSales, Status = @UOSStatus WHERE IdUnitOfSales = @UOSIdUnitOfSales;";

                SqlCommand command = new SqlCommand(updateSql, connection);

                command.Parameters.Add("@UOSIdUnitOfSales", SqlDbType.Char).Value = unitOfSales.Id;
                command.Parameters.Add("@UOSBarcode", SqlDbType.VarChar).Value = unitOfSales.Barcode;
                command.Parameters.Add("@UOSCode", SqlDbType.VarChar).Value = unitOfSales.Code;
                command.Parameters.Add("@UOSName", SqlDbType.VarChar).Value = unitOfSales.Name;
                command.Parameters.Add("@UOSQtyPerUOS", SqlDbType.Int).Value = unitOfSales.QtyPerUOS;
                command.Parameters.Add("@UOSDefaultPriceOfSales", SqlDbType.Decimal).Value = unitOfSales.DefaultPriceOfSales;
                command.Parameters.Add("@UOSStatus", SqlDbType.Bit).Value = unitOfSales.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void DeleteData(UnitOfSales unitOfSales)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string deleteSql = @"DELETE FROM dbo.posTbUnitOfSales WHERE IdUnitOfSales = @UOSIdUnitOfSales;";

                SqlCommand command = new SqlCommand(deleteSql, connection);

                command.Parameters.Add("@UOSIdUnitOfSales", SqlDbType.Char).Value = unitOfSales.Id;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #endregion

        #region Public Methods

        #region Actions

        public bool Save(UnitOfSales unitOfSales, ObservableCollection<UnitOfSales> clusterMembers)
        {
            ValidationHelper validation = new ValidationHelper();
            
            bool isSaved = false;
            int retry = 0;

            if (validation.HasNoEquals(unitOfSales, clusterMembers, this._uosStore))
            {
                if (string.IsNullOrEmpty(unitOfSales.Id))
                {
                    while ((!isSaved) && (retry < 3))
                    {
                        unitOfSales.Id = "000000";

                        try
                        {
                            this.InsertData(unitOfSales);

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
                            this.UpdateData(unitOfSales);

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
                MessageBox.Show(string.Format("Satuan jual dengan nama [{0}] dan kode barang [{1}] telah terdaftar.\n\nPastikan Anda menggunakan nama dan kode barang yang belum terdaftar sebelum melanjutkan proses.", unitOfSales.Name, unitOfSales.Code), "Proses Gagal", MessageBoxButton.OK);

            this.PopulateData();

            return isSaved;
        }

        public void Delete(UnitOfSales unitOfSales)
        {
            this.DeleteData(unitOfSales);

            this.PopulateData();
        }

        #endregion

        #region Filters

        public List<UnitOfSales> FindByClusterId(string clusterId)
        {
            IEnumerable<UnitOfSales> found1 = from uos in this._uosStore
                                              where (uos.ClusterId == clusterId)
                                              select uos;

            return found1.ToList();
        }

        public List<UnitOfSales> FindByStatus(bool status)
        {
            IEnumerable<UnitOfSales> found1 = from uos in this._uosStore
                                              where (uos.Status == status)
                                              select uos;

            return found1.ToList();
        }

        public List<UnitOfSales> FindAll()
        {
            return new List<UnitOfSales>(this._uosStore);
        }

        #endregion

        #endregion
    }
}
