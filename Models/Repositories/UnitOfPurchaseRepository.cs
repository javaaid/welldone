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
    public class UnitOfPurchaseRepository
    {
        #region Private Fields

        private readonly string _connString;
        private List<UnitOfPurchase> _uopStore;

        #endregion

        #region ctor

        public UnitOfPurchaseRepository()
        {
            this._connString = SqlHelper.GetConnectionString();
            this._uopStore = new List<UnitOfPurchase>();

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

                string selectSql = @"SELECT * FROM dbo.posVwUnitOfPurchase ORDER BY IdUnitOfPurchase;";

                SqlCommand command = new SqlCommand(selectSql, connection);

                connection.Open();

                this._uopStore.Clear();

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    this._uopStore.Add(new UnitOfPurchase()
                    {
                        Id = (string)dataReader["IdUnitOfPurchase"],
                        ClusterId = (string)dataReader["IdItem"],
                        Barcode = (string)dataReader["Barcode"],
                        Code = (string)dataReader["Code"],
                        Name = (string)dataReader["Name"],
                        QtyPerUOP = (int)dataReader["QtyPerUOP"],
                        Status = (bool)dataReader["Status"]
                    });
                }
            }
        }

        private void InsertData(UnitOfPurchase unitOfPurchase)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string insertIntoSql = @"INSERT INTO dbo.posTbUnitOfPurchase (IdUnitOfPurchase, IdItem, Barcode, Code, Name, QtyPerUOP, Status) VALUES (@UOPIdUnitOfPurchase, @UOPClusterId, @UOPBarcode, @UOPCode, @UOPName, @UOPQtyPerUOP, @UOPStatus);";

                SqlCommand command = new SqlCommand(insertIntoSql, connection);

                command.Parameters.Add("@UOPIdUnitOfPurchase", SqlDbType.Char).Value = unitOfPurchase.Id;
                command.Parameters.Add("@UOPClusterId", SqlDbType.Char).Value = unitOfPurchase.ClusterId;
                command.Parameters.Add("@UOPBarcode", SqlDbType.VarChar).Value = unitOfPurchase.Barcode;
                command.Parameters.Add("@UOPCode", SqlDbType.VarChar).Value = unitOfPurchase.Code;
                command.Parameters.Add("@UOPName", SqlDbType.VarChar).Value = unitOfPurchase.Name;
                command.Parameters.Add("@UOPQtyPerUOP", SqlDbType.Int).Value = unitOfPurchase.QtyPerUOP;
                command.Parameters.Add("@UOPStatus", SqlDbType.Bit).Value = unitOfPurchase.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void UpdateData(UnitOfPurchase unitOfPurchase)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string updateSql = @"UPDATE dbo.posTbUnitOfPurchase SET Barcode = @UOPBarcode, Code = @UOPCode, Name = @UOPName, QtyPerUOP = @UOPQtyPerUOP, Status = @UOPStatus WHERE IdUnitOfPurchase = @UOPIdUnitOfPurchase;";

                SqlCommand command = new SqlCommand(updateSql, connection);

                command.Parameters.Add("@UOPIdUnitOfPurchase", SqlDbType.Char).Value = unitOfPurchase.Id;
                command.Parameters.Add("@UOPBarcode", SqlDbType.VarChar).Value = unitOfPurchase.Barcode;
                command.Parameters.Add("@UOPCode", SqlDbType.VarChar).Value = unitOfPurchase.Code;
                command.Parameters.Add("@UOPName", SqlDbType.VarChar).Value = unitOfPurchase.Name;
                command.Parameters.Add("@UOPQtyPerUOP", SqlDbType.Int).Value = unitOfPurchase.QtyPerUOP;
                command.Parameters.Add("@UOPStatus", SqlDbType.Bit).Value = unitOfPurchase.Status;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        private void DeleteData(UnitOfPurchase unitOfPurchase)
        {
            using (SqlConnection connection = new SqlConnection(this._connString))
            {
                string deleteSql = @"DELETE FROM dbo.posTbUnitOfPurchase WHERE IdUnitOfPurchase = @UOPIdUnitOfPurchase;";

                SqlCommand command = new SqlCommand(deleteSql, connection);

                command.Parameters.Add("@UOPIdUnitOfPurchase", SqlDbType.Char).Value = unitOfPurchase.Id;

                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #endregion

        #region Public Methods

        #region Actions

        public bool Save(UnitOfPurchase unitOfPurchase, ObservableCollection<UnitOfPurchase> clusterMembers)
        {
            ValidationHelper validation = new ValidationHelper();

            bool isSaved = false;
            int retry = 0;

            if (validation.HasNoEquals(unitOfPurchase, clusterMembers, this._uopStore))
            {
                if (string.IsNullOrEmpty(unitOfPurchase.Id))
                {
                    while ((!isSaved) && (retry < 3))
                    {
                        unitOfPurchase.Id = "000000";

                        try
                        {
                            this.InsertData(unitOfPurchase);

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
                            this.UpdateData(unitOfPurchase);

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
                MessageBox.Show(string.Format("Satuan beli dengan nama [{0}] dan kode barang [{1}] telah terdaftar.\n\nPastikan Anda menggunakan nama dan kode barang yang belum terdaftar sebelum melanjutkan proses.", unitOfPurchase.Name, unitOfPurchase.Code), "Proses Gagal", MessageBoxButton.OK);

            this.PopulateData();

            return isSaved;
        }

        public void Delete(UnitOfPurchase unitOfPurchase)
        {
            this.DeleteData(unitOfPurchase);

            this.PopulateData();
        }

        #endregion

        #region Filters

        public List<UnitOfPurchase> FindByClusterId(string clusterId)
        {
            IEnumerable<UnitOfPurchase> found1 = from uop in this._uopStore
                                                 where (uop.ClusterId == clusterId)
                                                 select uop;

            return found1.ToList();
        }

        public List<UnitOfPurchase> FindByStatus(bool status)
        {
            IEnumerable<UnitOfPurchase> found1 = from uop in this._uopStore
                                                 where (uop.Status == status)
                                                 select uop;

            return found1.ToList();
        }

        public List<UnitOfPurchase> FindAll()
        {
            return new List<UnitOfPurchase>(this._uopStore);
        }

        #endregion

        #endregion
    }
}
