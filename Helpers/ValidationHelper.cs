using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using WelldonePOS.Models.DataModels;
using WelldonePOS.Models.Enums;
using WelldonePOS.Models.Misc;
using WelldonePOS.Presenters.Views;

namespace WelldonePOS.Helpers
{
    public class ValidationHelper : ValidationRule
    {
        #region Private Fields

        private string _type;
        private string _property;
        private object _reference;

        #endregion

        #region Public Properties

        public string Type {
            get {
                return this._type;
            }
            set {
                this._type = value;
            }
        }
        public string Property {
            get {
                return this._property;
            }
            set {
                this._property = value;
            }
        }
        public object Reference {
            get {
                return this._reference;
            }
            set {
                this._reference = value;
            }
        }

        #endregion

        #region Private Methods

        #region Implicit

        //inibuat Password
        private ValidationResult Result(object property, string errorMessage)
        {
            ValidationResult result = new ValidationResult(true, null);
            string input = (property ?? string.Empty).ToString();

            if (input != (this.Reference) as string)
                result = new ValidationResult(false, errorMessage);

            return result;
        }

        private ValidationResult Result(object property, string errorMessage, IEnumerable<int> validLength)
        {
            ValidationResult result = new ValidationResult(true, null);
            string input = (property ?? string.Empty).ToString();

            if (!validLength.Contains(input.Length))
                result = new ValidationResult(false, errorMessage);

            return result;
        }

        private ValidationResult Result(object property, string errorMessage, IEnumerable<int> validLength, int minimumValue)
        {
            ValidationResult result = new ValidationResult(true, null);
            string input = (property ?? string.Empty).ToString();

            if ((!validLength.Contains(input.Length)) || (int.Parse(input) < minimumValue))
                result = new ValidationResult(false, errorMessage);

            return result;
        }

        private ValidationResult Result(object property, string errorMessage, IEnumerable<int> validLength, decimal minimumValue)
        {
            ValidationResult result = new ValidationResult(true, null);
            string input = (property ?? string.Empty).ToString();

            if ((!validLength.Contains(input.Length)) || (decimal.Parse(input) < minimumValue))
                result = new ValidationResult(false, errorMessage);

            return result;
        }

        private ValidationResult Result(Dictionary<int, bool> property, string errorMessage, int minimumValue)
        {
            ValidationResult result = new ValidationResult(true, null);

            IEnumerable<int> found = from pair in property
                                     where (pair.Value == true)
                                     select pair.Key;

            if (found.ToList().Count < minimumValue)
                result = new ValidationResult(false, errorMessage);

            return result;
        }

        #endregion

        #region Explicit

        private ValidationResult Result(Category property, string errorMessage)
        {
            ValidationResult result = new ValidationResult(true, null);

            if (this.FindInvalid(property).Count > 0)
                result = new ValidationResult(false, errorMessage);

            return result;
        }

        private ValidationResult Result(Customer property, string errorMessage)
        {
            ValidationResult result = new ValidationResult(true, null);

            if (this.FindInvalid(property).Count > 0)
                result = new ValidationResult(false, errorMessage);

            return result;
        }

        private ValidationResult Result(Item property, string errorMessage)
        {
            ValidationResult result = new ValidationResult(true, null);

            if (this.FindInvalid(property).Count > 0)
                result = new ValidationResult(false, errorMessage);

            return result;
        }

        private ValidationResult Result(UnitOfPurchase property, string errorMessage)
        {
            ValidationResult result = new ValidationResult(true, null);

            if (this.FindInvalid(property).Count > 0)
                result = new ValidationResult(false, errorMessage);

            return result;
        }

        private ValidationResult Result(UnitOfSales property, string errorMessage)
        {
            ValidationResult result = new ValidationResult(true, null);

            if (this.FindInvalid(property).Count > 0)
                result = new ValidationResult(false, errorMessage);

            return result;
        }

        private ValidationResult Result(User property, string errorMessage)
        {
            ValidationResult result = new ValidationResult(true, null);

            if (this.FindInvalid(property).Count > 0)
                result = new ValidationResult(false, errorMessage);

            return result;
        }

        private ValidationResult Result(UserRight property, string errorMessage)
        {
            ValidationResult result = new ValidationResult(true, null);

            if (this.FindInvalid(property).Count > 0)
                result = new ValidationResult(false, errorMessage);

            return result;
        }

        private ValidationResult Result(ObservableCollection<SalesHistory> property, string errorMessage)
        {
            ValidationResult result = new ValidationResult(true, null);

            //if (property.Count > 0)
            //{
            //    for (int i = 0; i < property.Count; i++)
            //    {
            //        if (this.FindInvalid(property[i]).Count > 0)
            //        {
            //            result = new ValidationResult(false, errorMessage);
            //            break;
            //        }
            //    }
            //}
            //else
            //    result = new ValidationResult(false, errorMessage);

            if (property.Count < 1)
                result = new ValidationResult(false, errorMessage);

            return result;
        }

        private ValidationResult Result(Category category, string errorMessage, ObservableCollection<Item> itemCollection)
        {
            ValidationResult result = new ValidationResult(false, null);

            for (int i = 0; i < itemCollection.Count; i++)
            {
                if (itemCollection[i].Group.Id == category.Id)
                    result = new ValidationResult(true, errorMessage);
            }

            return result;
        }

        private ValidationResult Result(UserRight userRight, string errorMessage, ObservableCollection<User> userCollection)
        {
            ValidationResult result = new ValidationResult(false, null);

            for (int i = 0; i < userCollection.Count; i++)
            {
                if (userCollection[i].Accessibility.Id == userRight.Id)
                    result = new ValidationResult(true, errorMessage);
            }
            
            return result;
        }

        #endregion

        #endregion

        #region Public Methods

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            string input = (value ?? string.Empty).ToString();

            switch (this.Type)
            {
                case "Category":
                    switch (this.Property)
                    {
                        case "Name":
                            return this.Result(value, " * [Nama kategori] tidak dapat dikosongkan.", Enumerable.Range(1, 20));
                        default:
                            return ValidationResult.ValidResult;
                    }
                case "CompanyProfile":
                    switch (this.Property)
                    {
                        case "Name":
                            return this.Result(value, " * [Nama toko] tidak dapat dikosongkan.", Enumerable.Range(1, 25));
                        case "Owner":
                            return this.Result(value, " * [Nama pemilik] tidak dapat dikosongkan.", Enumerable.Range(1, 25));
                        case "Phone1":
                            return this.Result(value, " * [Telepon 1] tidak dapat dikosongkan.", Enumerable.Range(1, 15));
                        case "Address1":
                            return this.Result(value, " * [Alamat 1] tidak dapat dikosongkan.", Enumerable.Range(1, 75));
                        default:
                            return ValidationResult.ValidResult;
                    }
                case "Customer":
                    switch (this.Property)
                    {
                        case "Name":
                            return this.Result(value, " * [Nama customer] tidak dapat dikosongkan.", Enumerable.Range(1, 50));
                        case "Phone1":
                            return this.Result(value, " * [Telepon 1] tidak dapat dikosongkan.", Enumerable.Range(1, 15));
                        case "Address1":
                            return this.Result(value, " * [Alamat 1] tidak dapat dikosongkan.", Enumerable.Range(1, 75));
                        default:
                            return ValidationResult.ValidResult;
                    }
                case "Item":
                    switch (this.Property)
                    {
                        case "Name":
                            return this.Result(value, " * [Nama barang] tidak dapat dikosongkan.", Enumerable.Range(1, 75));
                        case "RefUnit":
                            return this.Result(value, " * [Nama satuan] referensi tidak dapat dikosongkan.", Enumerable.Range(1, 15));
                        case "MinimumProfit":
                            return this.Result(value, " * Nilai minimum [Batas minimum laba] adalah 0.", Enumerable.Range(1, 14), (decimal)0);
                        case "SafetyStockPortion":
                            return this.Result(value, " * Nilai minimum [Batas minimum persediaan] adalah 0.", Enumerable.Range(1, 9), (int)0);
                        default:
                            return ValidationResult.ValidResult;
                    }
                case "PasswordTemp":
                    switch (this.Property)
                    {
                        case "OldPassword":
                            return this.Result(value, " * Isian password harus sesuai dengan password lama.");
                        case "NewPassword":
                            return this.Result(value, " * [Password baru] terdiri dari minimum 8 digit.", Enumerable.Range(8, 18));
                        default:
                            return ValidationResult.ValidResult;
                    }
                case "SalesHistory":
                    switch (this.Property)
                    {
                        case "SoldQty":
                            return this.Result(value, " * Nilai minimum [Jumlah dijual] adalah 1.", Enumerable.Range(1, 9), (int)1);
                        default:
                            return ValidationResult.ValidResult;
                    }
                case "Stock":
                    switch (this.Property)
                    {
                        case "InitialQty":
                            return this.Result(value, " * Nilai minimum [Saldo awal] adalah 1.", Enumerable.Range(1, 9), (int)1);
                        case "PriceOfPurchase":
                            return this.Result(value, " * Nilai minimum [Harga beli] adalah 1.", Enumerable.Range(1, 14), (decimal)1);
                        default:
                            return ValidationResult.ValidResult;
                    }
                case "Supplier":
                    switch (this.Property)
                    {
                        case "Name":
                            return this.Result(value, " * [Nama supplier] tidak dapat dikosongkan.", Enumerable.Range(1, 50));
                        case "Phone1":
                            return this.Result(value, " * [Telepon 1] tidak dapat dikosongkan.", Enumerable.Range(1, 15));
                        case "Address1":
                            return this.Result(value, " * [Alamat 1] tidak dapat dikosongkan.", Enumerable.Range(1, 75));
                        default:
                            return ValidationResult.ValidResult;
                    }
                case "TFormSalesPresenter":
                    switch (this.Property)
                    {
                        case "CurrentPaid":
                            return this.Result(value, " * Nilai minimum [Bayar tunai] adalah 1.", Enumerable.Range(1, 14), (decimal)1);
                        default:
                            return ValidationResult.ValidResult;
                    }
                case "UnitOfPurchase":
                    switch (this.Property)
                    {
                        case "Name":
                            return this.Result(value, " * [Nama satuan beli] tidak dapat dikosongkan.", Enumerable.Range(1, 15));
                        case "QtyPerUOP":
                            return this.Result(value, " * Nilai minimum [Jumlah per satuan beli] adalah 1.", Enumerable.Range(1, 9), (int)1);
                        default:
                            return ValidationResult.ValidResult;
                    }
                case "UnitOfSales":
                    switch (this.Property)
                    {
                        case "Name":
                            return this.Result(value, " * [Nama satuan jual] tidak dapat dikosongkan.", Enumerable.Range(1, 15));
                        case "QtyPerUOS":
                            return this.Result(value, " * Nilai minimum [Jumlah per satuan jual] adalah 1.", Enumerable.Range(1, 9), (int)1);
                        case "DefaultPriceOfSales":
                            return this.Result(value, " * Nilai minimum [Harga jual baku] adalah 1.", Enumerable.Range(1, 14), (decimal)1);
                        default:
                            return ValidationResult.ValidResult;
                    }
                case "User":
                    switch (this.Property)
                    {
                        case "Name":
                            return this.Result(value, " * [Nama user] tidak dapat dikosongkan.", Enumerable.Range(1, 25));
                        case "Password":
                            return this.Result(value, " * [Password] terdiri dari minimum 8 digit.", Enumerable.Range(8, 18));
                        default:
                            return ValidationResult.ValidResult;
                    }
                case "UserRight":
                    switch (this.Property)
                    {
                        case "Name":
                            return this.Result(value, " * [Nama hak user] tidak dapat dikosongkan.", Enumerable.Range(1, 25));
                        default:
                            return ValidationResult.ValidResult;
                    }
                default:
                    return ValidationResult.ValidResult;
            }
        }

        #region Find Equals

        public List<string> FindReference(ObservableCollection<Category> categoryCollection, ObservableCollection<Item> itemCollection)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (itemCollection.Count != 0)
            {
                for (int i = 0; i < categoryCollection.Count; i++)
                {
                    results.Add(this.Result(categoryCollection[i], string.Format(" * Kategori [{0}] masih terkait dengan satu atau lebih data barang.", categoryCollection[i].Name), itemCollection));
                }

                IEnumerable<string> found = from result in results
                                            where (result.IsValid == true)
                                            select result.ErrorContent as string;

                List<string> referenceList = found.ToList();

                return referenceList;
            }
            else
                return new List<string>();
        }

        public List<string> FindReference(ObservableCollection<UserRight> userRightCollection, ObservableCollection<User> userCollection)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if (userCollection.Count != 0)
            {
                for (int i = 0; i < userRightCollection.Count; i++)
                {
                    results.Add(this.Result(userRightCollection[i], string.Format(" * Hak user [{0}] masih terkait dengan satu atau lebih akun user.", userRightCollection[i].Name), userCollection));
                }

                IEnumerable<string> found = from result in results
                                            where (result.IsValid == true)
                                            select result.ErrorContent as string;

                List<string> referenceList = found.ToList();

                return referenceList;
            }
            else
                return new List<string>();
        }

        #endregion

        #region Find Invalid

        public List<string> FindInvalid(Category category)
        {
            List<ValidationResult> results = new List<ValidationResult>()
            {
                this.Result(category.Name, " * [Nama kategori] tidak dapat dikosongkan.", Enumerable.Range(1, 20)),
            };

            IEnumerable<string> found = from result in results
                                        where (result.IsValid == false)
                                        select result.ErrorContent as string;

            List<string> errorList = found.ToList();

            return errorList;
        }

        public List<string> FindInvalid(CompanyProfile companyProfile)
        {
            List<ValidationResult> results = new List<ValidationResult>()
            {
                this.Result(companyProfile.Name, " * [Nama toko] tidak dapat dikosongkan.", Enumerable.Range(1, 25)),
                this.Result(companyProfile.Owner, " * [Nama pemilik] tidak dapat dikosongkan.", Enumerable.Range(1, 25)),
                this.Result(companyProfile.Phone1, " * [Telepon 1] tidak dapat dikosongkan.", Enumerable.Range(1, 15)),
                this.Result(companyProfile.Address1, " * [Alamat 1] tidak dapat dikosongkan.", Enumerable.Range(1, 75))
            };

            IEnumerable<string> found = from result in results
                                        where (result.IsValid == false)
                                        select result.ErrorContent as string;

            List<string> errorList = found.ToList();

            return errorList;
        }

        public List<string> FindInvalid(Customer customer)
        {
            List<ValidationResult> results = new List<ValidationResult>()
            {
                this.Result(customer.Name, " * [Nama customer] tidak dapat dikosongkan.", Enumerable.Range(1, 50)),
                this.Result(customer.Phone1, " * [Telepon 1] tidak dapat dikosongkan.", Enumerable.Range(1, 15)),
                this.Result(customer.Address1, " * [Alamat 1] tidak dapat dikosongkan.", Enumerable.Range(1, 75))
            };

            IEnumerable<string> found = from result in results
                                        where (result.IsValid == false)
                                        select result.ErrorContent as string;

            List<string> errorList = found.ToList();

            return errorList;
        }

        public List<string> FindInvalid(Item item)
        {
            List<ValidationResult> results = new List<ValidationResult>()
            {
                this.Result(item.Name, " * [Nama barang] tidak dapat dikosongkan.", Enumerable.Range(1, 75)),
                this.Result(item.Group, " * [Kategori] tidak dapat dikosongkan."),
                this.Result(item.RefUnit, " * [Nama satuan referensi] tidak dapat dikosongkan.", Enumerable.Range(1, 15)),
                this.Result(item.MinimumProfit, " * Nilai minimum [Batas minimum laba] adalah 0.", Enumerable.Range(1, 14), (decimal)0),
                this.Result(item.SafetyStockPortion, " * Nilai minimum [Batas Minimum Laba] adalah 0.", Enumerable.Range(1, 9), (int)0)
            };

            IEnumerable<string> found = from result in results
                                        where (result.IsValid == false)
                                        select result.ErrorContent as string;

            List<string> errorList = found.ToList();

            return errorList;
        }

        public List<string> FindInvalid(PasswordTemp passwordTemp)
        {
            List<ValidationResult> results = new List<ValidationResult>()
            {
                this.Result(passwordTemp.OldPassword, " * Isian password harus sesuai dengan password lama."),
                this.Result(passwordTemp.NewPassword, " * [Password] terdiri dari minimum 8 digit.", Enumerable.Range(8, 18))
            };

            IEnumerable<string> found = from result in results
                                        where (result.IsValid == false)
                                        select result.ErrorContent as string;

            List<string> errorList = found.ToList();

            return errorList;
        }

        public List<string> FindInvalid(SalesHistory salesHistory)
        {
            List<ValidationResult> results = new List<ValidationResult>()
            {
                this.Result(salesHistory.SoldItem, " * [Barang dijual] tidak dapat dikosongkan."),
                this.Result(salesHistory.SoldUnit, " * [Satuan dijual] tidak dapat dikosongkan."),
                this.Result(salesHistory.SoldQty, " * Nilai minimum [Jumlah dijual] adalah 1.", Enumerable.Range(1, 9), (int)1)
            };

            IEnumerable<string> found = from result in results
                                        where (result.IsValid == false)
                                        select result.ErrorContent as string;

            List<string> errorList = found.ToList();

            return errorList;
        }

        public List<string> FindInvalid(SalesTransaction salesTransaction)
        {
            List<ValidationResult> results = new List<ValidationResult>()
            {
                this.Result(salesTransaction.Seller, " * [Kasir] tidak dapat dikosongkan."),
                this.Result(salesTransaction.Consumer, " * [Customer] tidak dapat dikosongkan."),
                this.Result(salesTransaction.SumTotal, " * Nilai minimum [Nominal penjualan] adalah 1.", Enumerable.Range(1, 14), (decimal)1)
            };

            IEnumerable<string> found = from result in results
                                        where (result.IsValid == false)
                                        select result.ErrorContent as string;

            List<string> errorList = found.ToList();

            return errorList;
        }

        public List<string> FindInvalid(Stock stock)
        {
            List<ValidationResult> results = new List<ValidationResult>()
            {
                this.Result(stock.InitialQty, " * Nilai minimum [Saldo awal] adalah 1.", Enumerable.Range(1, 9), (int)1),
                this.Result(stock.PriceOfPurchase, " * Nilai minimum [Harga beli] adalah 1.", Enumerable.Range(1, 14), (decimal)1)
            };

            IEnumerable<string> found = from result in results
                                        where (result.IsValid == false)
                                        select result.ErrorContent as string;

            List<string> errorList = found.ToList();

            return errorList;
        }

        public List<string> FindInvalid(Supplier supplier)
        {
            List<ValidationResult> results = new List<ValidationResult>()
            {
                this.Result(supplier.Name, " * [Nama supplier] tidak dapat dikosongkan.", Enumerable.Range(1, 50)),
                this.Result(supplier.Phone1, " * [Telepon 1] tidak dapat dikosongkan.", Enumerable.Range(1, 15)),
                this.Result(supplier.Address1, " * [Alamat 1] tidak dapat dikosongkan.", Enumerable.Range(1, 75))
            };

            IEnumerable<string> found = from result in results
                                        where (result.IsValid == false)
                                        select result.ErrorContent as string;

            List<string> errorList = found.ToList();

            return errorList;
        }

        public List<string> FindInvalid(TFormSalesPresenter tFormSalesPresenter)
        {
            List<ValidationResult> results = new List<ValidationResult>()
            {
                this.Result(tFormSalesPresenter.CurrentSalesHistoryCollection, " * [Histori penjualan] tidak dapat dikosongkan."),
                this.Result(tFormSalesPresenter.CurrentPaid, " * Nilai minimum [Bayar tunai] adalah 1.", Enumerable.Range(1, 14), (decimal)1),
                this.Result(tFormSalesPresenter.CurrentChange, " * Nilai minimum [Kembalian] adalah 0.", Enumerable.Range(1, 14), (decimal)0)
            };

            IEnumerable<string> found = from result in results
                                        where (result.IsValid == false)
                                        select result.ErrorContent as string;

            List<string> errorList = found.ToList();

            return errorList;
        }

        public List<string> FindInvalid(UnitOfPurchase unitOfPurchase)
        {
            List<ValidationResult> results = new List<ValidationResult>()
            {
                this.Result(unitOfPurchase.Name, " * [Nama satuan beli] tidak dapat dikosongkan.", Enumerable.Range(1, 15)),
                this.Result(unitOfPurchase.QtyPerUOP, " * Nilai minimum [Jumlah per satuan beli] adalah 1.", Enumerable.Range(1, 9), (int)1)
            };

            IEnumerable<string> found = from result in results
                                        where (result.IsValid == false)
                                        select result.ErrorContent as string;

            List<string> errorList = found.ToList();

            return errorList;
        }

        public List<string> FindInvalid(UnitOfSales unitOfSales)
        {
            List<ValidationResult> results = new List<ValidationResult>()
            {
                this.Result(unitOfSales.Name, " * [Nama satuan jual] tidak dapat dikosongkan.", Enumerable.Range(1, 15)),
                this.Result(unitOfSales.QtyPerUOS, " * Nilai minimum [Jumlah per satuan jual] adalah 1.", Enumerable.Range(1, 9), (int)1),
                this.Result(unitOfSales.DefaultPriceOfSales, " * Nilai minimum [Harga jual baku] adalah 1.", Enumerable.Range(1, 14), (decimal)1)
            };

            IEnumerable<string> found = from result in results
                                        where (result.IsValid == false)
                                        select result.ErrorContent as string;

            List<string> errorList = found.ToList();

            return errorList;
        }

        public List<string> FindInvalid(User user)
        {
            List<ValidationResult> results = new List<ValidationResult>()
            {
                this.Result(user.Name, " * [Nama user] tidak dapat dikosongkan.", Enumerable.Range(1, 25)),
                this.Result(user.Password, " * [Password] terdiri dari minimum 8 digit.", Enumerable.Range(8, 18)),
                this.Result(user.Accessibility, " * [Aksesibilitas] tidak dapat dikosongkan."),
                this.Result(user.PhotoPath, " * [Foto] tidak dapat dikosongkan.", Enumerable.Range(1, 300))
            };

            IEnumerable<string> found = from result in results
                                        where (result.IsValid == false)
                                        select result.ErrorContent as string;

            List<string> errorList = found.ToList();

            return errorList;
        }

        public List<string> FindInvalid(UserRight userRight)
        {
            List<ValidationResult> results = new List<ValidationResult>()
            {
                this.Result(userRight.Name, " * [Nama hak user] tidak dapat dikosongkan.", Enumerable.Range(1, 25)),
                this.Result(userRight.Rights, " * [Hak akses user] tidak dapat dikosongkan.", 1)
            };

            IEnumerable<string> found = from result in results
                                        where (result.IsValid == false)
                                        select result.ErrorContent as string;

            List<string> errorList = found.ToList();

            return errorList;
        }

        #endregion

        #region Has No Equals

        public bool HasNoEquals(Category category, List<Category> store)
        {
            for (int i = 0; i < store.Count; i++)
            {
                if (store[i].Name == category.Name)
                {
                    if (!string.IsNullOrEmpty(category.Id))
                    {
                        if (store[i].Id != category.Id)
                            return false;
                    }
                    else
                        return false;
                }
            }

            return true;
        }

        public bool HasNoEquals(Customer customer, List<Customer> store)
        {
            for (int i = 0; i < store.Count; i++)
            {
                if (store[i].Name == customer.Name)
                {
                    if (!string.IsNullOrEmpty(customer.Id))
                    {
                        if (store[i].Id != customer.Id)
                            return false;
                    }
                    else
                        return false;
                }
            }

            return true;
        }

        public bool HasNoEquals(Item item, List<Item> store)
        {
            for (int i = 0; i < store.Count; i++)
            {
                if (store[i].Name == item.Name)
                {
                    if (!string.IsNullOrEmpty(item.Id))
                    {
                        if (store[i].Id != item.Id)
                            return false;
                    }
                    else
                        return false;
                }
            }

            return true;
        }

        public bool HasNoEquals(SalesTransaction salesTransaction, List<SalesTransaction> store)
        {
            for (int i = 0; i < store.Count; i++)
            {
                if (store[i].Code == salesTransaction.Code)
                {
                    if (!string.IsNullOrEmpty(salesTransaction.Id))
                    {
                        if (store[i].Id != salesTransaction.Id)
                            return false;
                    }
                    else
                        return false;
                }
            }

            return true;
        }

        public bool HasNoEquals(Supplier supplier, List<Supplier> store)
        {
            for (int i = 0; i < store.Count; i++)
            {
                if (store[i].Name == supplier.Name)
                {
                    if (!string.IsNullOrEmpty(supplier.Id))
                    {
                        if (store[i].Id != supplier.Id)
                            return false;
                    }
                    else
                        return false;
                }
            }

            return true;
        }

        public bool HasNoEquals(UnitOfPurchase unitOfPurchase, ObservableCollection<UnitOfPurchase> clusterMembers, List<UnitOfPurchase> store)
        {
            for (int i = 0; i < clusterMembers.Count; i++)
            {
                if (clusterMembers[i].Name == unitOfPurchase.Name)
                {
                    if (!string.IsNullOrEmpty(unitOfPurchase.Id))
                    {
                        if (clusterMembers[i].Id != unitOfPurchase.Id)
                            return false;
                    }
                    else
                        return false;
                }
            }

            if (!string.IsNullOrEmpty(unitOfPurchase.Code))
            {
                for (int i = 0; i < store.Count; i++)
                {
                    if (store[i].Code == unitOfPurchase.Code)
                    {
                        if (!string.IsNullOrEmpty(unitOfPurchase.Id))
                        {
                            if (store[i].Id != unitOfPurchase.Id)
                                return false;
                        }
                        else
                            return false;
                    }
                }
            }

            return true;
        }

        public bool HasNoEquals(UnitOfSales unitOfSales, ObservableCollection<UnitOfSales> clusterMembers, List<UnitOfSales> store)
        {
            for (int i = 0; i < clusterMembers.Count; i++)
            {
                if (clusterMembers[i].Name == unitOfSales.Name)
                {
                    if (!string.IsNullOrEmpty(unitOfSales.Id))
                    {
                        if (clusterMembers[i].Id != unitOfSales.Id)
                            return false;
                    }
                    else
                        return false;
                }
            }

            if (!string.IsNullOrEmpty(unitOfSales.Code))
            {
                for (int i = 0; i < store.Count; i++)
                {
                    if (store[i].Code == unitOfSales.Code)
                    {
                        if (!string.IsNullOrEmpty(unitOfSales.Id))
                        {
                            if (store[i].Id != unitOfSales.Id)
                                return false;
                        }
                        else
                            return false;
                    }
                }
            }

            return true;
        }

        public bool HasNoEquals(User user, List<User> store)
        {
            for (int i = 0; i < store.Count; i++)
            {
                if (store[i].Name == user.Name)
                {
                    if (!string.IsNullOrEmpty(user.Id))
                    {
                        if (store[i].Id != user.Id)
                            return false;
                    }
                    else
                        return false;
                }
            }
            
            return true;
        }

        public bool HasNoEquals(UserRight userRight, List<UserRight> store)
        {
            for (int i = 0; i < store.Count; i++)
            {
                if (store[i].Name == userRight.Name)
                {
                    if (!string.IsNullOrEmpty(userRight.Id))
                    {
                        if (store[i].Id != userRight.Id)
                            return false;
                    }
                    else
                        return false;
                }
            }

            return true;
        }

        #endregion

        #region Has No Errors

        public bool HasNoErrors(DependencyObject obj)
        {
            return ((!Validation.GetHasError(obj)) && (LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>().All(HasNoErrors)));
        }

        public bool HasNoErrors(Key key)
        {
            switch (key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.Left:
                case Key.Up:
                case Key.Right:
                case Key.Down:
                case Key.Back:
                case Key.Delete:
                case Key.Tab:
                    return true;
                default:
                    return false;
            }
        }

        #endregion

        #endregion
    }
}
