using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

using WelldonePOS.Models.DataModels;
using WelldonePOS.Models.Enums;
using WelldonePOS.Models.Repositories;
using WelldonePOS.Presenters.Views;
using WelldonePOS.UserControls.Shells;
using WelldonePOS.UserControls.Views;

namespace WelldonePOS.Presenters.Shells
{
    public class AppPresenter : PresenterBase<AppShell>
    {
        #region Private Fields

        private readonly CategoryRepository _categoryRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly GeneralSettingsRepository _generalSettingsRepository;
        private readonly ItemRepository _itemRepository;
        private readonly SalesTransactionRepository _salesTransactionRepository;
        private readonly SupplierRepository _supplierRepository;
        private readonly UserRepository _userRepository;
        private readonly UserRightRepository _userRightRepository;

        private ObservableCollection<Category> _currentCategoryCollection;
        private ObservableCollection<Customer> _currentCustomerCollection;
        private ObservableCollection<Item> _currentItemCollection;
        private ObservableCollection<SalesTransaction> _currentSalesTransactionCollection;
        private ObservableCollection<Supplier> _currentSupplierCollection;
        private ObservableCollection<User> _currentUserCollection;
        private ObservableCollection<UserRight> _currentUserRightCollection;

        private ObservableCollection<Item> _currentTItemCollection;

        private ObservableCollection<GeneralSettings> _currentGeneralSettings;

        private readonly User _currentLogin;
        private DateTime _currentDateTime;
        private string _statusText;

        private bool _profileBoxState;

        #endregion

        #region ctor

        public AppPresenter(AppShell view, CategoryRepository categoryRepository, CustomerRepository customerRepository, GeneralSettingsRepository generalSettingsRepository, ItemRepository itemRepository, SalesTransactionRepository salesTransactionRepository, SupplierRepository supplierRepository, UserRepository userRepository, UserRightRepository userRightRepository)
            : base(view)
        {
            this._categoryRepository = categoryRepository;
            this._customerRepository = customerRepository;
            this._generalSettingsRepository = generalSettingsRepository;
            this._itemRepository = itemRepository;
            this._salesTransactionRepository = salesTransactionRepository;
            this._supplierRepository = supplierRepository;
            this._userRepository = userRepository;
            this._userRightRepository = userRightRepository;

            this._currentCategoryCollection = new ObservableCollection<Category>(categoryRepository.FindAll());
            this._currentCustomerCollection = new ObservableCollection<Customer>(customerRepository.FindAll());
            this._currentItemCollection = new ObservableCollection<Item>(itemRepository.FindAllItem());
            this._currentSalesTransactionCollection = new ObservableCollection<SalesTransaction>(salesTransactionRepository.FindAllTransaction());
            this._currentSupplierCollection = new ObservableCollection<Supplier>(supplierRepository.FindAll());
            this._currentUserCollection = new ObservableCollection<User>(userRepository.FindAll());
            this._currentUserRightCollection = new ObservableCollection<UserRight>(userRightRepository.FindAll());

            this._currentTItemCollection = new ObservableCollection<Item>(); ;

            this._currentGeneralSettings = new ObservableCollection<GeneralSettings>();
            this._currentGeneralSettings.Add(generalSettingsRepository.FindGeneralSettings());

            this._currentLogin = (Application.Current.MainWindow.DataContext as MainPresenter).CurrentLogin;

            this._profileBoxState = false;
        }

        #endregion

        #region Public Properties

        public ObservableCollection<Category> CurrentCategoryCollection {
            get {
                return this._currentCategoryCollection;
            }
            set {
                this._currentCategoryCollection = value;
                OnPropertyChanged("CurrentCategoryCollection");
            }
        }
        public ObservableCollection<Customer> CurrentCustomerCollection {
            get {
                return this._currentCustomerCollection;
            }
            set {
                this._currentCustomerCollection = value;
                OnPropertyChanged("CurrentCustomerCollection");
            }
        }
        public ObservableCollection<Item> CurrentItemCollection {
            get {
                return this._currentItemCollection;
            }
            set {
                this._currentItemCollection = value;
                OnPropertyChanged("CurrentItemCollection");
            }
        }
        public ObservableCollection<SalesTransaction> CurrentSalesTransactionCollection {
            get {
                return this._currentSalesTransactionCollection;
            }
            set {
                this._currentSalesTransactionCollection = value;
                OnPropertyChanged("CurrentSalesTransactionCollection");
            }
        }
        public ObservableCollection<Supplier> CurrentSupplierCollection {
            get {
                return this._currentSupplierCollection;
            }
            set {
                this._currentSupplierCollection = value;
                OnPropertyChanged("CurrentSupplierCollection");
            }
        }
        public ObservableCollection<User> CurrentUserCollection {
            get {
                return this._currentUserCollection;
            }
            set {
                this._currentUserCollection = value;
                OnPropertyChanged("CurrentUserCollection");
            }
        }
        public ObservableCollection<UserRight> CurrentUserRightCollection {
            get {
                return this._currentUserRightCollection;
            }
            set {
                this._currentUserRightCollection = value;
                OnPropertyChanged("CurrentUserRightCollection");
            }
        }

        public ObservableCollection<Item> CurrentTItemCollection {
            get {
                return this._currentTItemCollection;
            }
            set {
                this._currentTItemCollection = value;
                OnPropertyChanged("CurrentTItemCollection");
            }
        }

        public ObservableCollection<GeneralSettings> CurrentGeneralSettings {
            get {
                return this._currentGeneralSettings;
            }
            set {
                this._currentGeneralSettings = value;
                OnPropertyChanged("CurrentGeneralSettings");
            }
        }

        public User CurrentLogin {
            get {
                return this._currentLogin;
            }
        }
        public DateTime CurrentDateTime {
            get {
                return this._currentDateTime;
            }
            set {
                this._currentDateTime = value;
                OnPropertyChanged("CurrentDateTime");
            }
        }
        public string StatusText {
            get {
                return this._statusText;
            }
            set {
                this._statusText = value;
                OnPropertyChanged("StatusText");
            }
        }

        public bool ProfileBoxState {
            get {
                return this._profileBoxState;
            }
            set {
                this._profileBoxState = value;
                OnPropertyChanged("ProfileBoxState");
            }
        }

        #endregion

        #region Public Methods

        #region Add Tab

        public void DisplayManageProfile()
        {
            View.AddTab(new ManageProfilePresenter(this, new ManageProfileView()));
        }

        public void DisplayTFormSales()
        {
            View.AddTab(new TFormSalesPresenter(this, new TFormSalesView()));
        }

        public void DisplayTFormReceipt()
        {
            View.AddTab(new TFormReceiptPresenter(this, new TFormReceiptView()));
        }

        public void DisplayTFormPurchase()
        {
            View.AddTab(new TFormPurchasePresenter(this, new TFormPurchaseView()));
        }

        public void DisplaySetupDataItem()
        {
            View.AddTab(new SetupDataItemPresenter(this, new SetupDataItemView()));
        }

        public void DisplaySetupDataCustomer()
        {
            View.AddTab(new SetupDataCustomerPresenter(this, new SetupDataCustomerView()));
        }

        public void DisplaySetupDataSupplier()
        {
            View.AddTab(new SetupDataSupplierPresenter(this, new SetupDataSupplierView()));
        }

        public void DisplaySetupDataCategory()
        {
            View.AddTab(new SetupDataCategoryPresenter(this, new SetupDataCategoryView()));
        }

        public void DisplaySetupGeneralSettings()
        {
            View.AddTab(new SetupGeneralSettingsPresenter(this, new SetupGeneralSettingsView()));
        }

        public void DisplaySetupUser()
        {
            View.AddTab(new SetupUserPresenter(this, new SetupUserView()));
        }

        public void DisplaySetupUserRight()
        {
            View.AddTab(new SetupUserRightPresenter(this, new SetupUserRightView()));
        }

        #endregion

        #region Save

        public bool Save(Category category)
        {
            bool isSaved = false;

            if (this._categoryRepository.Save(category))
            {
                this.StatusText = string.Format("Kategori [{0}] telah disimpan.", category.Name);

                isSaved = true;
            }
            else
                this.StatusText = "Terjadi kesalahan program. Silahkan coba beberapa saat lagi.";

            this.Reload("Category");

            return isSaved;
        }

        public bool Save(CompanyPolicies companyPolicies)
        {
            bool isSaved = false;

            if (this._generalSettingsRepository.Save(companyPolicies))
            {
                this.StatusText = "Kebijakan toko telah diperbarui.";

                isSaved = true;
            }
            else
                this.StatusText = "Terjadi kesalahan program. Silahkan coba beberapa saat lagi.";

            this.Reload("GeneralSettings");

            return isSaved;
        }

        public bool Save(CompanyProfile companyProfile)
        {
            bool isSaved = false;

            if (this._generalSettingsRepository.Save(companyProfile))
            {
                this.StatusText = "Profil toko telah diperbarui.";

                isSaved = true;
            }
            else
                this.StatusText = "Terjadi kesalahan program. Silahkan coba beberapa saat lagi.";

            this.Reload("GeneralSettings");

            return isSaved;
        }

        public bool Save(Customer customer)
        {
            bool isSaved = false;

            if (this._customerRepository.Save(customer))
            {
                this.StatusText = string.Format("Customer [{0}] telah disimpan.", customer.Name);

                isSaved = true;
            }
            else
                this.StatusText = "Terjadi kesalahan program. Silahkan coba beberapa saat lagi.";

            this.Reload("Customer");

            return isSaved;
        }

        public bool Save(Item item)
        {
            bool isSaved = false;

            if (this._itemRepository.Save(item))
            {
                this.StatusText = string.Format("Barang [{0}] dengan kategori [{1}] telah disimpan.", item.Name, item.Group.Name);

                isSaved = true;
            }
            else
                this.StatusText = "Terjadi kesalahan program. Silahkan coba beberapa saat lagi.";

            this.Reload("Item");

            return isSaved;
        }

        public bool Save(SalesHistory salesHistory)
        {
            bool isSaved = false;

            if (this._salesTransactionRepository.Save(salesHistory))
            {
                this.StatusText = string.Format("Histori transaksi untuk transaksi penjualan dengan nomor [{0}] telah disimpan.", salesHistory.ClusterId);

                isSaved = true;
            }
            else
                this.StatusText = "Terjadi kesalahan program. Silahkan coba beberapa saat lagi.";

            this.Reload("SalesTransaction");

            return isSaved;
        }

        public bool Save(SalesTransaction salesTransaction)
        {
            bool isSaved = false;

            if (this._salesTransactionRepository.Save(salesTransaction))
            {
                this.StatusText = string.Format("Transaksi penjualan [{0}] telah disimpan.", salesTransaction.Code);

                isSaved = true;
            }
            else
                this.StatusText = "Terjadi kesalahan program. Silahkan coba beberapa saat lagi.";

            this.Reload("SalesTransaction");

            return isSaved;
        }

        public bool Save(Stock stock)
        {
            bool isSaved = false;

            if (this._itemRepository.Save(stock))
            {
                this.StatusText = string.Format("Persediaan dengan sumber [{0}] sejumlah [{1}] telah disimpan.", stock.Origin.ToString(), stock.InitialQty.ToString());

                isSaved = true;
            }
            else
                this.StatusText = "Terjadi kesalahan program. Silahkan coba beberapa saat lagi.";

            this.Reload("Item");

            return isSaved;
        }

        public bool Save(Supplier supplier)
        {
            bool isSaved = false;

            if (this._supplierRepository.Save(supplier))
            {
                this.StatusText = string.Format("Supplier [{0}] telah disimpan.", supplier.Name);

                isSaved = true;
            }
            else
                this.StatusText = "Terjadi kesalahan program. Silahkan coba beberapa saat lagi.";

            this.Reload("Supplier");

            return isSaved;
        }

        public bool Save(UnitOfPurchase unitOfPurchase, ObservableCollection<UnitOfPurchase> clusterMembers)
        {
            bool isSaved = false;

            if (this._itemRepository.Save(unitOfPurchase, clusterMembers))
            {
                this.StatusText = string.Format("Satuan beli [{0}] telah disimpan.", unitOfPurchase.Name);

                isSaved = true;
            }
            else
                this.StatusText = "Terjadi kesalahan program. Silahkan coba beberapa saat lagi.";

            this.Reload("Item");

            return isSaved;
        }

        public bool Save(UnitOfSales unitOfSales, ObservableCollection<UnitOfSales> clusterMembers)
        {
            bool isSaved = false;

            if (this._itemRepository.Save(unitOfSales, clusterMembers))
            {
                this.StatusText = string.Format("Satuan jual [{0}] telah disimpan.", unitOfSales.Name);

                isSaved = true;
            }
            else
                this.StatusText = "Terjadi kesalahan program. Silahkan coba beberapa saat lagi.";

            this.Reload("Item");

            return isSaved;
        }

        public bool Save(User user)
        {
            bool isSaved = false;

            if (this._userRepository.Save(user))
            {
                this.StatusText = string.Format("User [{0}] dengan Level akses [{1}] telah disimpan.", user.Name, user.Accessibility.Name);

                isSaved = true;
            }
            else
                this.StatusText = "Terjadi kesalahan program. Silahkan coba beberapa saat lagi.";

            this.Reload("User");

            return isSaved;
        }

        public bool Save(UserRight userRight)
        {
            bool isSaved = false;

            if (this._userRightRepository.Save(userRight))
            {
                this.StatusText = string.Format("Hak user [{0}] telah disimpan.", userRight.Name);

                isSaved = true;
            }
            else
                this.StatusText = "Terjadi kesalahan program. Silahkan coba beberapa saat lagi.";

            this.Reload("UserRight");

            return isSaved;
        }

        #endregion

        #region Delete

        public void Delete(Category category)
        {
            this._categoryRepository.Delete(category);

            this.Reload("Category");

            this.StatusText = string.Format("Kategori [{0}] telah dihapus.", category.Name);
        }

        public void Delete(Customer customer)
        {
            this._customerRepository.Delete(customer);

            this.Reload("Customer");

            this.StatusText = string.Format("Customer [{0}] telah dihapus.", customer.Name);
        }

        public void Delete(Item item)
        {
            this._itemRepository.Delete(item);

            this.Reload("Item");

            this.StatusText = string.Format("Barang [{0}] dengan kategori [{1}] telah dihapus.", item.Name, item.Group.Name);
        }

        public void Delete(Stock stock)
        {
            this._itemRepository.Delete(stock);

            this.Reload("Item");

            this.StatusText = string.Format("Persediaan dengan sumber [{0}] sejumlah [{1}] telah dihapus.", stock.Origin.ToString(), stock.InitialQty.ToString());
        }

        public void Delete(Supplier supplier)
        {
            this._supplierRepository.Delete(supplier);

            this.Reload("Supplier");

            this.StatusText = string.Format("Supplier [{0}] telah dihapus.", supplier.Name);
        }

        public void Delete(UnitOfPurchase unitOfPurchase)
        {
            this._itemRepository.Delete(unitOfPurchase);

            this.Reload("Item");

            this.StatusText = string.Format("Satuan beli [{0}] telah dihapus.", unitOfPurchase.Name);
        }

        public void Delete(UnitOfSales unitOfSales)
        {
            this._itemRepository.Delete(unitOfSales);

            this.Reload("Item");

            this.StatusText = string.Format("Satuan jual [{0}] telah dihapus.", unitOfSales.Name);
        }

        public void Delete(User user)
        {
            this._userRepository.Delete(user);

            this.Reload("User");

            this.StatusText = string.Format("User [{0}] dengan Level akses [{1}] telah dihapus.", user.Name, user.Accessibility.Name);
        }

        public void Delete(UserRight userRight)
        {
            this._userRightRepository.Delete(userRight);

            this.Reload("UserRight");

            this.StatusText = string.Format("Hak user [{0}] telah dihapus.", userRight.Name);
        }

        #endregion

        #region Filter

        #region Filter Data

        public void FilterItem(string prop, string status, string keywords)
        {
            if ((!string.IsNullOrEmpty(prop)) && (!string.IsNullOrEmpty(keywords)) && (!string.IsNullOrEmpty(status)) && (keywords.Length > 1))
            {
                switch (status)
                {
                    case "Semua":
                        switch (prop)
                        {
                            case "Nama":
                                this.CurrentItemCollection.Clear();

                                foreach (Item itm in this._itemRepository.FindByName(keywords))
                                    this.CurrentItemCollection.Add(itm);

                                break;
                            case "Kategori":
                                this.CurrentItemCollection.Clear();

                                foreach (Item itm in this._itemRepository.FindByGroup(keywords))
                                    this.CurrentItemCollection.Add(itm);

                                break;
                            case "Satuan Referensi":
                                this.CurrentItemCollection.Clear();

                                foreach (Item itm in this._itemRepository.FindByRefUnit(keywords))
                                    this.CurrentItemCollection.Add(itm);

                                break;
                        }

                        break;
                    case "Aktif":
                        switch (prop)
                        {
                            case "Nama":
                                this.CurrentItemCollection.Clear();

                                foreach (Item itm in this._itemRepository.FindByNameByStatus(keywords, true))
                                    this.CurrentItemCollection.Add(itm);

                                break;
                            case "Kategori":
                                this.CurrentItemCollection.Clear();

                                foreach (Item itm in this._itemRepository.FindByGroupByStatus(keywords, true))
                                    this.CurrentItemCollection.Add(itm);

                                break;
                            case "Satuan Referensi":
                                this.CurrentItemCollection.Clear();

                                foreach (Item itm in this._itemRepository.FindByRefUnitByStatus(keywords, true))
                                    this.CurrentItemCollection.Add(itm);

                                break;
                        }

                        break;
                    case "Non Aktif":
                        switch (prop)
                        {
                            case "Nama":
                                this.CurrentItemCollection.Clear();

                                foreach (Item itm in this._itemRepository.FindByNameByStatus(keywords, false))
                                    this.CurrentItemCollection.Add(itm);

                                break;
                            case "Kategori":
                                this.CurrentItemCollection.Clear();

                                foreach (Item itm in this._itemRepository.FindByGroupByStatus(keywords, false))
                                    this.CurrentItemCollection.Add(itm);

                                break;
                            case "Satuan Referensi":
                                this.CurrentItemCollection.Clear();

                                foreach (Item itm in this._itemRepository.FindByRefUnitByStatus(keywords, false))
                                    this.CurrentItemCollection.Add(itm);

                                break;
                        }

                        break;
                }

                this.StatusText = string.Format("{0} barang ditemukan.", this.CurrentItemCollection.Count);
            }
            else
            {
                this.Reload("Item");

                this.StatusText = "Menampilkan semua barang.";
            }
        }

        public void FilterCustomer(string prop, string status, string keywords)
        {
            if ((!string.IsNullOrEmpty(prop)) && (!string.IsNullOrEmpty(keywords)) && (!string.IsNullOrEmpty(status)) && (keywords.Length > 1))
            {
                switch (status)
                {
                    case "Semua":
                        switch (prop)
                        {
                            case "Nama":
                                this.CurrentCustomerCollection.Clear();

                                foreach (Customer cst in this._customerRepository.FindByName(keywords))
                                    this.CurrentCustomerCollection.Add(cst);

                                break;
                            case "Kota":
                                this.CurrentCustomerCollection.Clear();

                                foreach (Customer cst in this._customerRepository.FindByCity(keywords))
                                    this.CurrentCustomerCollection.Add(cst);

                                break;
                        }

                        break;
                    case "Aktif":
                        switch (prop)
                        {
                            case "Nama":
                                this.CurrentCustomerCollection.Clear();

                                foreach (Customer cst in this._customerRepository.FindByNameByStatus(keywords, true))
                                    this.CurrentCustomerCollection.Add(cst);

                                break;
                            case "Kota":
                                this.CurrentCustomerCollection.Clear();

                                foreach (Customer cst in this._customerRepository.FindByCityByStatus(keywords, true))
                                    this.CurrentCustomerCollection.Add(cst);

                                break;
                        }

                        break;
                    case "Non Aktif":
                        switch (prop)
                        {
                            case "Nama":
                                this.CurrentCustomerCollection.Clear();

                                foreach (Customer cst in this._customerRepository.FindByNameByStatus(keywords, false))
                                    this.CurrentCustomerCollection.Add(cst);

                                break;
                            case "Kota":
                                this.CurrentCustomerCollection.Clear();

                                foreach (Customer cst in this._customerRepository.FindByCityByStatus(keywords, false))
                                    this.CurrentCustomerCollection.Add(cst);

                                break;
                        }

                        break;
                }
            }
            else
            {
                this.Reload("Customer");

                this.StatusText = "Menampilkan semua customer.";
            }
        }

        public void FilterSupplier(string prop, string status, string keywords)
        {
            if ((!string.IsNullOrEmpty(prop)) && (!string.IsNullOrEmpty(keywords)) && (!string.IsNullOrEmpty(status)) && (keywords.Length > 1))
            {
                switch (status)
                {
                    case "Semua":
                        switch (prop)
                        {
                            case "Nama":
                                this.CurrentSupplierCollection.Clear();

                                foreach (Supplier spl in this._supplierRepository.FindByName(keywords))
                                    this.CurrentSupplierCollection.Add(spl);

                                break;
                            case "Kota":
                                this.CurrentSupplierCollection.Clear();

                                foreach (Supplier spl in this._supplierRepository.FindByCity(keywords))
                                    this.CurrentSupplierCollection.Add(spl);

                                break;
                        }

                        break;
                    case "Aktif":
                        switch (prop)
                        {
                            case "Nama":
                                this.CurrentSupplierCollection.Clear();

                                foreach (Supplier spl in this._supplierRepository.FindByNameByStatus(keywords, true))
                                    this.CurrentSupplierCollection.Add(spl);

                                break;
                            case "Kota":
                                this.CurrentSupplierCollection.Clear();

                                foreach (Supplier spl in this._supplierRepository.FindByCityByStatus(keywords, true))
                                    this.CurrentSupplierCollection.Add(spl);

                                break;
                        }

                        break;
                    case "Non Aktif":
                        switch (prop)
                        {
                            case "Nama":
                                this.CurrentSupplierCollection.Clear();

                                foreach (Supplier spl in this._supplierRepository.FindByNameByStatus(keywords, false))
                                    this.CurrentSupplierCollection.Add(spl);

                                break;
                            case "Kota":
                                this.CurrentSupplierCollection.Clear();

                                foreach (Supplier spl in this._supplierRepository.FindByCityByStatus(keywords, false))
                                    this.CurrentSupplierCollection.Add(spl);

                                break;
                        }

                        break;
                }
            }
            else
            {
                this.Reload("Supplier");

                this.StatusText = "Menampilkan semua supplier.";
            }

        }

        public void FilterTItem(string prop, string keywords)
        {
            if ((!string.IsNullOrEmpty(prop)) && (!string.IsNullOrEmpty(keywords)) && (keywords.Length > 0))
            {
                //List<UnitNature> natures = new List<UnitNature>(new UnitNature[] { UnitNature.SalesOnly, UnitNature.Both });

                switch (prop)
                {
                    case "Nama":
                        this.CurrentTItemCollection.Clear();

                        foreach (Item itm in this._itemRepository.FindByNameByStatusByObjective(keywords, true, "Sales"))
                            this.CurrentTItemCollection.Add(itm);

                        ////foreach (Item itm in this._itemRepository.FindByNameByStatusByUnitNature(keywords, true, natures))
                        ////    this.CurrentTItemCollection.Add(itm);

                        break;
                    case "Kategori":
                        this.CurrentTItemCollection.Clear();

                        //foreach (Item itm in this._itemRepository.FindByGroupByStatusByUnitNature(keywords, true, natures))
                        //    this.CurrentTItemCollection.Add(itm);

                        break;
                }
            }
            else
            {
                this.Reload("TItem");
            }
        }

        #endregion

        #region Combobox Items Source

        public List<Category> FindCategory(bool status)
        {
            return this._categoryRepository.FindByStatus(status);
        }

        public List<Customer> FindCustomer(bool status)
        {
            return this._customerRepository.FindByStatus(status);
        }

        public List<UserRight> FindUserRight(bool status)
        {
            return this._userRightRepository.FindByStatus(status);
        }

        #endregion

        #region Cluster Member

        public List<SalesHistory> FindAllSalesHistory()
        {
            return this._salesTransactionRepository.FindAllHistory();
        }

        #endregion

        #endregion

        #region Reload Collection

        public void Reload(string type)
        {
            switch (type)
            {
                case "Category":
                    this.CurrentCategoryCollection.Clear();
                    
                    foreach (Category ctg in this._categoryRepository.FindAll())
                        this.CurrentCategoryCollection.Add(ctg);
                    
                    break;
                case "Customer":
                    this.CurrentCustomerCollection.Clear();

                    foreach (Customer cst in this._customerRepository.FindAll())
                        this.CurrentCustomerCollection.Add(cst);

                    break;
                case "GeneralSettings":
                    this.CurrentGeneralSettings.Clear();

                    this.CurrentGeneralSettings.Add(this._generalSettingsRepository.FindGeneralSettings());

                    break;
                case "Item":
                    this.CurrentItemCollection.Clear();
                    
                    foreach (Item itm in this._itemRepository.FindAllItem())
                        this.CurrentItemCollection.Add(itm);
                    
                    break;
                case "SalesTransaction":
                    this.CurrentSalesTransactionCollection.Clear();

                    foreach (SalesTransaction slt in this._salesTransactionRepository.FindAllTransaction())
                        this.CurrentSalesTransactionCollection.Add(slt);

                    break;
                case "Supplier":
                    this.CurrentSupplierCollection.Clear();

                    foreach (Supplier spl in this._supplierRepository.FindAll())
                        this.CurrentSupplierCollection.Add(spl);

                    break;
                case "User":
                    this.CurrentUserCollection.Clear();

                    List<User> x = this._userRepository.FindAll();

                    foreach (User usr in x)
                        this.CurrentUserCollection.Add(usr);

                    break;
                case "UserRight":
                    this.CurrentUserRightCollection.Clear();

                    foreach (UserRight urg in this._userRightRepository.FindAll())
                        this.CurrentUserRightCollection.Add(urg);

                    break;
                case "TItem":
                    this.CurrentTItemCollection.Clear();

                    break;
            }
        }

        #endregion

        #region etc

        public void SwapProfileBoxState()
        {
            this.ProfileBoxState = !this.ProfileBoxState;
        }

        public void CloseTab<T>(PresenterBase<T> presenter)
        {
            View.RemoveTab(presenter);
        }

        public void Logout()
        {
            this.CurrentLogin.LoggingIn = false;
            this._userRepository.Save(this.CurrentLogin);

            View.NavigateToLogin();
        }

        #endregion

        #endregion
    }
}
