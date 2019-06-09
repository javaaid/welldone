using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelldonePOS.Models.Misc
{
    static class OptionsProvider
    {
        #region Private Fields

        private static readonly List<string> _statusFilterOptions;

        private static readonly List<string> _customerFilterOptions;
        private static readonly List<string> _itemFilterOptions;
        private static readonly List<string> _salesTransactionFilterOptions;
        private static readonly List<string> _supplierFilterOptions;

        private static readonly List<string> _tItemFilterOptions;

        #endregion

        #region ctor

        static OptionsProvider()
        {
            _statusFilterOptions = new List<string>(3)
            {
                "Semua",
                "Aktif",
                "Non Aktif"
            };

            _customerFilterOptions = new List<string>(2)
            {
                "Nama",
                "Kota"
            };

            _itemFilterOptions = new List<string>(3)
            {
                "Nama", 
                "Kategori", 
                "Satuan Referensi"
            };

            _salesTransactionFilterOptions = new List<string>(3)
            {
                "Nomor Transaksi",
                "Karyawan",
                "Konsumen"
            };

            _supplierFilterOptions = new List<string>(2)
            {
                "Nama",
                "Kota"
            };

            _tItemFilterOptions = new List<string>(2)
            {
                "Nama", 
                "Kategori", 
            };
        }

        #endregion

        #region Public Methods

        public static IList<string> GetStatusFilterOptions()
        {
            return _statusFilterOptions;
        }

        public static IList<string> GetCustomerFilterOptions()
        {
            return _customerFilterOptions;
        }

        public static IList<string> GetItemFilterOptions()
        {
            return _itemFilterOptions;
        }

        public static IList<string> GetSalesTransactionFilterOptions()
        {
            return _salesTransactionFilterOptions;
        }

        public static IList<string> GetSupplierFilterOptions()
        {
            return _supplierFilterOptions;
        }

        public static IList<string> GetTItemFilterOptions()
        {
            return _tItemFilterOptions;
        }

        #endregion
    }
}
