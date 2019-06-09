using System.Windows.Input;

namespace WelldonePOS.Models.Misc
{
    public static class UICommands
    {
        public static readonly RoutedUICommand AddReceiptHistory = new RoutedUICommand("AddReceiptHistory", "AddReceiptHistory", typeof(UICommands));
        public static readonly RoutedUICommand AddSalesHistory = new RoutedUICommand("AddSalesHistory", "AddSalesHistory", typeof(UICommands));

        public static readonly RoutedUICommand ReplaceCategory = new RoutedUICommand("ReplaceCategory", "ReplaceCategory", typeof(UICommands));
        public static readonly RoutedUICommand ReplacePassword = new RoutedUICommand("ReplacePassword", "ReplacePassword", typeof(UICommands));

        public static readonly RoutedUICommand SaveCategory = new RoutedUICommand("SaveCategory", "SaveCategory", typeof(UICommands));
        public static readonly RoutedUICommand SaveCompanyProfile = new RoutedUICommand("SaveCompanyProfile", "SaveCompanyProfile", typeof(UICommands));
        public static readonly RoutedUICommand SaveCustomer = new RoutedUICommand("SaveCustomer", "SaveCustomer", typeof(UICommands));
        public static readonly RoutedUICommand SaveItem = new RoutedUICommand("SaveItem", "SaveItem", typeof(UICommands));
        public static readonly RoutedUICommand SavePurchaseTransaction = new RoutedUICommand("SavePurchaseTransaction", "SavePurchaseTransaction", typeof(UICommands));
        public static readonly RoutedUICommand SaveReceiptTransaction = new RoutedUICommand("SaveReceiptTransaction", "SaveReceiptTransaction", typeof(UICommands));
        public static readonly RoutedUICommand SaveSalesTransaction = new RoutedUICommand("SaveSalesTransaction", "SaveSalesTransaction", typeof(UICommands));
        public static readonly RoutedUICommand SaveStock = new RoutedUICommand("SaveStock", "SaveStock", typeof(UICommands));
        public static readonly RoutedUICommand SaveSupplier = new RoutedUICommand("SaveSupplier", "SaveSupplier", typeof(UICommands));
        public static readonly RoutedUICommand SaveUnitOfPurchase = new RoutedUICommand("SaveUnitOfPurchase", "SaveUnitOfPurchase", typeof(UICommands));
        public static readonly RoutedUICommand SaveUnitOfSales = new RoutedUICommand("SaveUnitOfSales", "SaveUnitOfSales", typeof(UICommands));
        public static readonly RoutedUICommand SaveUser = new RoutedUICommand("SaveUser", "SaveUser", typeof(UICommands));
        public static readonly RoutedUICommand SaveUserRight = new RoutedUICommand("SaveUserRight", "SaveUserRight", typeof(UICommands));
    }
}
