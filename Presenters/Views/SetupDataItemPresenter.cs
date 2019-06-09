using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using WelldonePOS.Models.DataModels;
using WelldonePOS.Models.Enums;
using WelldonePOS.Presenters.Shells;
using WelldonePOS.UserControls.Views;

namespace WelldonePOS.Presenters.Views
{
    public class SetupDataItemPresenter : PresenterBase<SetupDataItemView>
    {
        #region Private Fields

        private readonly AppPresenter _appPresenter;

        private bool _isItemIdle;
        private bool _isStocksIdle;
        private bool _isUnitsIdle;

        private ObservableCollection<Item> _currentItemCollection;
        private ObservableCollection<Item> _currentItemCollectionTemp;
        private Item _currentItem;
        private Item _currentItemTemp;

        private ObservableCollection<Category> _currentCategoryCollection;
        private Category _currentCategory;

        private ObservableCollection<Stock> _currentStockCollection;
        private Stock _currentStock;
        private Stock _currentStockTemp;

        private ObservableCollection<UnitOfPurchase> _currentUnitOfPurchaseCollection;
        private UnitOfPurchase _currentUnitOfPurchase;
        private UnitOfPurchase _currentUnitOfPurchaseTemp;

        private ObservableCollection<UnitOfSales> _currentUnitOfSalesCollection;
        private UnitOfSales _currentUnitOfSales;
        private UnitOfSales _currentUnitOfSalesTemp;

        private int _currentTotalStockPreview;
        private int _currentStockPreview;

        #endregion

        #region ctor

        public SetupDataItemPresenter(AppPresenter appPresenter, SetupDataItemView view)
            : base(view, "TabHeader")
        {
            this._appPresenter = appPresenter;

            this._isItemIdle = true;
            this._isStocksIdle = true;
            this._isUnitsIdle = true;

            this._currentItemCollection = appPresenter.CurrentItemCollection;
            this._currentItemCollectionTemp = new ObservableCollection<Item>();
            this._currentItem = new Item();
            this._currentItemTemp = null;

            this._currentCategoryCollection = new ObservableCollection<Category>(appPresenter.FindCategory(true));
            this._currentCategory = null;

            this._currentStockCollection = new ObservableCollection<Stock>();
            this._currentStock = new Stock();
            this._currentStockTemp = null;

            this._currentUnitOfSalesCollection = new ObservableCollection<UnitOfSales>();
            this._currentUnitOfSales = new UnitOfSales();
            this._currentUnitOfSalesTemp = null;

            this._currentTotalStockPreview = 0;
            this._currentStockPreview = 0;
        }

        #endregion

        #region Public Properties

        public string TabHeader {
            get {
                return "Setup Data Barang";
            }
        }

        public AppPresenter Presenter {
            get {
                return this._appPresenter;
            }
        }

        public bool IsItemIdle {
            get {
                return this._isItemIdle;
            }
            set {
                this._isItemIdle = value;
                OnPropertyChanged("IsItemIdle");
            }
        }
        public bool IsStocksIdle {
            get {
                return this._isStocksIdle;
            }
            set {
                this._isStocksIdle = value;
                OnPropertyChanged("IsStocksIdle");
            }
        }
        public bool IsUnitsIdle {
            get {
                return this._isUnitsIdle;
            }
            set {
                this._isUnitsIdle = value;
                OnPropertyChanged("IsUnitsIdle");
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
        public ObservableCollection<Item> CurrentItemCollectionTemp {
            get {
                return this._currentItemCollectionTemp;
            }
            set {
                this._currentItemCollectionTemp = value;
                OnPropertyChanged("CurrentItemCollectionTemp");
            }
        }
        public Item CurrentItem {
            get {
                return this._currentItem;
            }
            set {
                this._currentItem = value;
                OnPropertyChanged("CurrentItem");
            }
        }
        public Item CurrentItemTemp {
            get {
                return this._currentItemTemp;
            }
            set {
                this._currentItemTemp = value;
                OnPropertyChanged("CurrentItemTemp");
            }
        }

        public ObservableCollection<Category> CurrentCategoryCollection {
            get {
                return this._currentCategoryCollection;
            }
            set {
                this._currentCategoryCollection = value;
                OnPropertyChanged("CurrentCategoryCollection");
            }
        }
        public Category CurrentCategory {
            get {
                return this._currentCategory;
            }
            set {
                this._currentCategory = value;
                OnPropertyChanged("CurrentCategory");
            }
        }

        public ObservableCollection<Stock> CurrentStockCollection {
            get {
                return this._currentStockCollection;
            }
            set {
                this._currentStockCollection = value;
                OnPropertyChanged("CurrentStockCollection");
            }
        }
        public Stock CurrentStock {
            get {
                return this._currentStock;
            }
            set {
                this._currentStock = value;
                OnPropertyChanged("CurrentStock");
            }
        }
        public Stock CurrentStockTemp {
            get {
                return this._currentStockTemp;
            }
            set {
                this._currentStockTemp = value;
                OnPropertyChanged("CurrentStockTemp");
            }
        }

        public ObservableCollection<UnitOfPurchase> CurrentUnitOfPurchaseCollection {
            get {
                return this._currentUnitOfPurchaseCollection;
            }
            set {
                this._currentUnitOfPurchaseCollection = value;
                OnPropertyChanged("CurrentUnitOfPurchaseCollection");
            }
        }
        public UnitOfPurchase CurrentUnitOfPurchase {
            get {
                return this._currentUnitOfPurchase;
            }
            set {
                this._currentUnitOfPurchase = value;
                OnPropertyChanged("CurrentUnitOfPurchase");
            }
        }
        public UnitOfPurchase CurrentUnitOfPurchaseTemp {
            get {
                return this._currentUnitOfPurchaseTemp;
            }
            set {
                this._currentUnitOfPurchaseTemp = value;
                OnPropertyChanged("CurrentUnitOfPurchaseTemp");
            }
        }

        public ObservableCollection<UnitOfSales> CurrentUnitOfSalesCollection {
            get {
                return this._currentUnitOfSalesCollection;
            }
            set {
                this._currentUnitOfSalesCollection = value;
                OnPropertyChanged("CurrentUnitOfSalesCollection");
            }
        }
        public UnitOfSales CurrentUnitOfSales {
            get {
                return this._currentUnitOfSales;
            }
            set {
                this._currentUnitOfSales = value;
                OnPropertyChanged("CurrentUnitOfSales");
            }
        }
        public UnitOfSales CurrentUnitOfSalesTemp {
            get {
                return this._currentUnitOfSalesTemp;
            }
            set {
                this._currentUnitOfSalesTemp = value;
                OnPropertyChanged("CurrentUnitOfSalesTemp");
            }
        }

        public int CurrentTotalStockPreview {
            get {
                return this._currentTotalStockPreview;
            }
            set {
                this._currentTotalStockPreview = value;
                OnPropertyChanged("CurrentTotalStockPreview");
            }
        }
        public int CurrentStockPreview {
            get {
                return this._currentStockPreview;
            }
            set {
                this._currentStockPreview = value;
                OnPropertyChanged("CurrentStockPreview");
            }
        }

        #endregion

        #region Private Methods

        #region Fill Temp

        private void FillTemp(UnitOfSales unitOfSales)
        {
            this.CurrentUnitOfSalesTemp = new UnitOfSales()
            {
                Id = unitOfSales.Id,
                ClusterId = unitOfSales.ClusterId,
                Barcode = unitOfSales.Barcode,
                Code = unitOfSales.Code,
                Name = unitOfSales.Name,
                QtyPerUOS = unitOfSales.QtyPerUOS,
                DefaultPriceOfSales = unitOfSales.DefaultPriceOfSales,
                Status = unitOfSales.Status
            };
        }

        private void FillTemp(UnitOfPurchase unitOfPurchase)
        {
            this.CurrentUnitOfPurchaseTemp = new UnitOfPurchase()
            {
                Id = unitOfPurchase.Id,
                ClusterId = unitOfPurchase.ClusterId,
                Barcode = unitOfPurchase.Barcode,
                Code = unitOfPurchase.Code,
                Name = unitOfPurchase.Name,
                QtyPerUOP = unitOfPurchase.QtyPerUOP,
                Status = unitOfPurchase.Status
            };
        }

        private void FillTemp(Item item)
        {
            this.CurrentItemTemp = new Item()
            {
                Id = item.Id,
                Name = item.Name,
                Group = item.Group,
                RefUnit = item.RefUnit,
                MinimumProfit = item.MinimumProfit,
                SafetyStockPortion = item.SafetyStockPortion,
                Status = item.Status
            };
        }

        #endregion

        #region Return Temp

        private void ReturnUnitOfSalesTemp() 
        {
            this.CurrentUnitOfSales = new UnitOfSales()
            {
                Id = this.CurrentUnitOfSalesTemp.Id,
                ClusterId = this.CurrentItem.Id,
                Barcode = this.CurrentUnitOfSalesTemp.Barcode,
                Code = this.CurrentUnitOfSalesTemp.Code,
                Name = this.CurrentUnitOfSalesTemp.Name,
                QtyPerUOS = this.CurrentUnitOfSalesTemp.QtyPerUOS,
                DefaultPriceOfSales = this.CurrentUnitOfSalesTemp.DefaultPriceOfSales,
                Status = this.CurrentUnitOfSalesTemp.Status
            };
        }

        private void ReturnUnitOfPurchaseTemp()
        {
            this.CurrentUnitOfPurchase = new UnitOfPurchase()
            {
                Id = this.CurrentUnitOfPurchaseTemp.Id,
                ClusterId = this.CurrentItem.Id,
                Barcode = this.CurrentUnitOfPurchaseTemp.Barcode,
                Code = this.CurrentUnitOfPurchaseTemp.Code,
                Name = this.CurrentUnitOfPurchaseTemp.Name,
                QtyPerUOP = this.CurrentUnitOfPurchaseTemp.QtyPerUOP,
                Status = this.CurrentUnitOfPurchaseTemp.Status
            };
        }

        private void ReturnStockTemp()
        {
            this.CurrentStock = new Stock()
            {
                Id = this.CurrentStockTemp.Id,
                ClusterId = this.CurrentItem.Id,
                ReceptionDate = DateTime.Now,
                Origin = StockOrigin.InitialStock,
                InitialQty = this.CurrentStockTemp.InitialQty,
                CurrentQty = this.CurrentStockTemp.InitialQty,
                PriceOfPurchase = this.CurrentStockTemp.PriceOfPurchase,
                Status = this.CurrentStockTemp.Status
            };
        }

        private void ReturnItemTemp()
        {
            this.CurrentItem = new Item()
            {
                Id = this.CurrentItemTemp.Id,
                Name = this.CurrentItemTemp.Name,
                Group = this.CurrentItemTemp.Group,
                RefUnit = this.CurrentItemTemp.RefUnit,
                MinimumProfit = this.CurrentItemTemp.MinimumProfit,
                SafetyStockPortion = this.CurrentItemTemp.SafetyStockPortion,
                Status = this.CurrentItemTemp.Status
            };
        }

        #endregion

        #region Calculation

        private void CalculateTotalStockPreview()
        {
            this.CurrentTotalStockPreview = 0;

            for (int i = 0; i < this.CurrentItem.Stocks.Count; i++)
            {
                this.CurrentTotalStockPreview = this.CurrentTotalStockPreview + this.CurrentItem.Stocks[i].CurrentQty;
            }
        }

        private void CalculateStockPreview(int qtyPerUnit)
        {
            this.CurrentStockPreview = this.CurrentTotalStockPreview / qtyPerUnit;
        }

        #endregion

        #endregion

        #region Public Methods

        #region Select & Deselect

        public void SelectThis(Item item)
        {
            this.CurrentItemCollectionTemp.Add(item);
        }

        public void DeselectThis(Item item)
        {
            for (int i = 0; i < this.CurrentItemCollectionTemp.Count; i++)
            {
                if (this.CurrentItemCollectionTemp[i].Equals(item))
                    this.CurrentItemCollectionTemp.Remove(item);
            }
        }

        #endregion

        #region Save

        public bool SaveUnitOfSales()
        {
            bool isSaved = false;

            this.ReturnUnitOfSalesTemp();

            if (this.Presenter.Save(this.CurrentUnitOfSales, this.CurrentUnitOfSalesCollection))
            {
                for (int i = 0; i < this.CurrentItemCollection.Count; i++)
                {
                    if (this.CurrentItemCollection[i].Equals(this.CurrentItem))
                    {
                        this.CurrentItem = this.CurrentItemCollection[i];
                        this.CurrentUnitOfSalesCollection = new ObservableCollection<UnitOfSales>(this.CurrentItemCollection[i].UnitsOfSales);
                        break;
                    }
                }

                isSaved = true;
            }

            return isSaved;
        }

        public bool SaveUnitOfPurchase()
        {
            bool isSaved = false;

            this.ReturnUnitOfPurchaseTemp();

            if (this.Presenter.Save(this.CurrentUnitOfPurchase, this.CurrentUnitOfPurchaseCollection))
            {
                for (int i = 0; i < this.CurrentItemCollection.Count; i++)
                {
                    if (this.CurrentItemCollection[i].Equals(this.CurrentItem))
                    {
                        this.CurrentItem = this.CurrentItemCollection[i];
                        this.CurrentUnitOfPurchaseCollection = new ObservableCollection<UnitOfPurchase>(this.CurrentItemCollection[i].UnitsOfPurchase);
                        break;
                    }
                }

                isSaved = true;
            }

            return isSaved;
        }

        public bool SaveStock()
        {
            bool isSaved = false;

            this.ReturnStockTemp();

            if (this.Presenter.Save(this.CurrentStock))
            {
                for (int i = 0; i < this.CurrentItemCollection.Count; i++)
                {
                    if (this.CurrentItemCollection[i].Equals(this.CurrentItem))
                    {
                        this.CurrentItem = this.CurrentItemCollection[i];
                        this.CurrentStockCollection = new ObservableCollection<Stock>(this.CurrentItemCollection[i].Stocks);
                        break;
                    }
                }

                this.CalculateTotalStockPreview();

                isSaved = true;
            }

            return isSaved;
        }

        public bool SaveItem()
        {
            bool isSaved = false;

            this.ReturnItemTemp();

            if (this.Presenter.Save(this.CurrentItem))
                isSaved = true;

            return isSaved;
        }

        #endregion

        #region Delete

        public void DeleteUnitOfSales(UnitOfSales unitOfSales)
        {
            this.CurrentUnitOfSales = unitOfSales;
            this.Presenter.Delete(this.CurrentUnitOfSales);

            for (int i = 0; i < this.CurrentItemCollection.Count; i++)
            {
                if (this.CurrentItemCollection[i].Equals(this.CurrentItem))
                {
                    this.CurrentItem = this.CurrentItemCollection[i];
                    this.CurrentUnitOfSalesCollection = new ObservableCollection<UnitOfSales>(this.CurrentItemCollection[i].UnitsOfSales);
                    break;
                }
            }
        }

        public void DeleteUnitOfPurchase(UnitOfPurchase unitOfPurchase) 
        {
            this.CurrentUnitOfPurchase = unitOfPurchase;
            this.Presenter.Delete(this.CurrentUnitOfPurchase);

            for (int i = 0; i < this.CurrentItemCollection.Count; i++)
            {
                if (this.CurrentItemCollection[i].Equals(this.CurrentItem))
                {
                    this.CurrentItem = this.CurrentItemCollection[i];
                    this.CurrentUnitOfPurchaseCollection = new ObservableCollection<UnitOfPurchase>(this.CurrentItemCollection[i].UnitsOfPurchase);
                    break;
                }
            }
        }

        public void DeleteStock()
        {
            this.Presenter.Delete(this.CurrentStockCollection[0]);

            for (int i = 0; i < this.CurrentItemCollection.Count; i++)
            {
                if (this.CurrentItemCollection[i].Equals(this.CurrentItem))
                {
                    this.CurrentItem = this.CurrentItemCollection[i];
                    this.CurrentStockCollection = new ObservableCollection<Stock>(this.CurrentItemCollection[i].Stocks);
                    break;
                }
            }

            this.CalculateTotalStockPreview();
        }

        public void DeleteItem()
        {
            for (int i = 0; i < this.CurrentItemCollectionTemp.Count; i++)
            {
                for (int j = 0; j < this.CurrentItemCollection.Count; j++)
                {
                    if (this.CurrentItemCollection[j].Equals(this.CurrentItemCollectionTemp[i]))
                    {
                        this.Presenter.Delete(this.CurrentItemCollection[j]);
                        break;
                    }
                }
            }
        }

        #endregion

        #region Detail

        public void UnitOfSalesDetail(UnitOfSales unitOfSales)
        {
            this.CurrentUnitOfSales = unitOfSales;
            this.FillTemp(unitOfSales);
        }

        public void UnitOfPurchaseDetail(UnitOfPurchase unitOfPurchase)
        {
            this.CurrentUnitOfPurchase = unitOfPurchase;
            this.FillTemp(unitOfPurchase);
        }

        public void UnitsDetail()
        {
            this.CurrentItem = this.CurrentItemCollectionTemp[0];
            this.CurrentUnitOfPurchaseCollection = new ObservableCollection<UnitOfPurchase>(this.CurrentItemCollectionTemp[0].UnitsOfPurchase);
            this.CurrentUnitOfSalesCollection = new ObservableCollection<UnitOfSales>(this.CurrentItemCollectionTemp[0].UnitsOfSales);

            this.CalculateTotalStockPreview();
        }

        public void StocksDetail()
        {
            this.CurrentItem = this.CurrentItemCollectionTemp[0];
            this.CurrentStockCollection = new ObservableCollection<Stock>(this.CurrentItemCollectionTemp[0].Stocks);

            this.CalculateTotalStockPreview();
        }

        public void ItemDetail()
        {
            this.CurrentItem = this.CurrentItemCollectionTemp[0];
            this.FillTemp(this.CurrentItemCollectionTemp[0]);
        }

        #endregion

        #region Clear Detail

        public void ClearGroupDetail()
        {
            this.CurrentItemCollectionTemp = new ObservableCollection<Item>();
        }

        public void ClearUnitOfSalesDetail()
        {
            this.CurrentUnitOfSales = new UnitOfSales();
            this.CurrentUnitOfSalesTemp = new UnitOfSales();

            this.CurrentStockPreview = 0;
        }

        public void ClearUnitOfPurchaseDetail()
        {
            this.CurrentUnitOfPurchase = new UnitOfPurchase();
            this.CurrentUnitOfPurchaseTemp = new UnitOfPurchase();

            this.CurrentStockPreview = 0;
        }

        public void ClearUnitsDetail()
        {
            this.CurrentItem = new Item();
            this.CurrentItemTemp = new Item();

            this.CalculateTotalStockPreview();
        }

        public void ClearStockDetail()
        {
            this.CurrentStock = new Stock();
            this.CurrentStockTemp = new Stock();
        }

        public void ClearStocksDetail()
        {
            this.CurrentItem = new Item();
            this.CurrentItemTemp = new Item();

            this.CalculateTotalStockPreview();
        }

        public void ClearItemDetail()
        {
            this.CurrentItem = new Item();
            this.CurrentItemTemp = new Item();
        }

        #endregion

        #region Update Preview

        public void UpdateStockPreview(string qtyPerUnit)
        {
            this.CalculateStockPreview(int.Parse(qtyPerUnit));
        }

        #endregion

        #region etc

        public void Filter(string prop, string status, string keywords)
        {
            this.Presenter.FilterItem(prop, status, keywords);
        }

        public void ClearSelection()
        {
            this.CurrentItemCollectionTemp = new ObservableCollection<Item>();
            this.Presenter.Reload("Item");
        }

        public void Close()
        {
            this.Presenter.CloseTab(this);
            this.Presenter.Reload("Item");
        }

        #endregion

        #endregion

        #region Equality

        public bool Equals(SetupDataItemPresenter other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (this.GetType() == other.GetType());
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as SetupDataItemPresenter);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int hashingBase = (int)2166136261;
                const int hashingMultiplier = 16777619;

                int hash = hashingBase;

                hash = (hash * hashingMultiplier) ^ (!object.ReferenceEquals(null, this.GetType()) ? this.GetType().GetHashCode() : 0);

                return hash;
            }
        }

        public static bool operator ==(SetupDataItemPresenter x, SetupDataItemPresenter y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(SetupDataItemPresenter x, SetupDataItemPresenter y)
        {
            return !(x == y);
        }

        #endregion
    }
}
