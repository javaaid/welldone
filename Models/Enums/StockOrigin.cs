using System.ComponentModel;

using WelldonePOS.ValueConverters;

namespace WelldonePOS.Models.Enums
{
    [TypeConverter(typeof(enum_description))]
    public enum StockOrigin
    {
        [Description("Belum dipilih")]
        None = 0,
        [Description("Saldo Awal")]
        InitialStock = 1,
        [Description("Pembelian")]
        PurchaseTransaction = 2
    }
}
