using System.ComponentModel;

using WelldonePOS.ValueConverters;

namespace WelldonePOS.Models.Enums
{
    [TypeConverter(typeof(enum_description))]
    public enum InventoryCosting
    {
        [Description("First In First Out (FIFO)")]
        FIFO = 1,
        [Description("Weighted Average")]
        Average = 2,
    }
}
