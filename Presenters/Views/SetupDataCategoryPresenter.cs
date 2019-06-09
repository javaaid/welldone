using System.Collections.ObjectModel;

using WelldonePOS.Models.DataModels;
using WelldonePOS.Presenters.Shells;
using WelldonePOS.UserControls.Views;

namespace WelldonePOS.Presenters.Views
{
    public class SetupDataCategoryPresenter : PresenterBase<SetupDataCategoryView>
    {
        #region Private Fields

        private readonly AppPresenter _appPresenter;

        private bool _isIdle;

        private ObservableCollection<Category> _currentCategoryCollection;
        private ObservableCollection<Category> _currentCategoryCollectionTemp;
        private Category _currentCategory;
        private Category _currentCategoryTemp;

        private ObservableCollection<Item> _currentItemCollection;

        #endregion

        #region ctor

        public SetupDataCategoryPresenter(AppPresenter appPresenter, SetupDataCategoryView view)
            : base(view, "TabHeader")
        {
            this._appPresenter = appPresenter;

            this._isIdle = true;

            this._currentCategoryCollection = appPresenter.CurrentCategoryCollection;
            this._currentCategoryCollectionTemp = new ObservableCollection<Category>();
            this._currentCategory = new Category();
            this._currentCategoryTemp = null;

            this._currentItemCollection = appPresenter.CurrentItemCollection;
        }

        #endregion

        #region Public Properties

        public string TabHeader {
            get {
                return "Setup Data Kategori";
            }
        }

        public AppPresenter Presenter {
            get {
                return this._appPresenter;
            }
        }

        public bool IsIdle {
            get {
                return this._isIdle;
            }
            set {
                this._isIdle = value;
                OnPropertyChanged("IsIdle");
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
        public ObservableCollection<Category> CurrentCategoryCollectionTemp {
            get {
                return this._currentCategoryCollectionTemp;
            }
            set {
                this._currentCategoryCollectionTemp = value;
                OnPropertyChanged("CurrentCategoryCollectionTemp");
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
        public Category CurrentCategoryTemp {
            get {
                return this._currentCategoryTemp;
            }
            set {
                this._currentCategoryTemp = value;
                OnPropertyChanged("CurrentCategoryTemp");
            }
        }

        public ObservableCollection<Item> CurrentItemCollection {
            get {
                return this._currentItemCollection;
            }
        }

        #endregion

        #region Private Methods

        #region Fill Temp

        private void FillTemp(Category category)
        {
            this.CurrentCategoryTemp = new Category()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Status = category.Status
            };
        }

        #endregion

        #region Return Temp

        private void ReturnTemp()
        {
            this.CurrentCategory = new Category()
            {
                Id = this.CurrentCategoryTemp.Id,
                Name = this.CurrentCategoryTemp.Name,
                Description = this.CurrentCategoryTemp.Description,
                Status = this.CurrentCategoryTemp.Status
            };
        }

        #endregion

        #endregion

        #region Public Methods

        #region Select & Deselect

        public void SelectThis(Category category)
        {
            this.CurrentCategoryCollectionTemp.Add(category);
        }

        public void DeselectThis(Category category)
        {
            for (int i = 0; i < this.CurrentCategoryCollectionTemp.Count; i++)
            {
                if (this.CurrentCategoryCollectionTemp[i].Equals(category))
                    this.CurrentCategoryCollectionTemp.Remove(category);
            }
        }

        #endregion

        #region Save

        public bool Save()
        {
            bool isSaved = false;

            this.ReturnTemp();

            if (this.Presenter.Save(this.CurrentCategory))
                isSaved = true;

            return isSaved;
        }

        #endregion

        #region Delete

        public void Delete()
        {
            for (int i = 0; i < this.CurrentCategoryCollectionTemp.Count; i++)
            {
                for (int j = 0; j < this.CurrentCategoryCollection.Count; j++)
                {
                    if (this.CurrentCategoryCollection[j].Equals(this.CurrentCategoryCollectionTemp[i]))
                    {
                        this.Presenter.Delete(this.CurrentCategoryCollection[j]);
                        break;
                    }
                }
            }
        }

        #endregion

        #region Detail

        public void Detail()
        {
            this.CurrentCategory = this.CurrentCategoryCollectionTemp[0];
            this.FillTemp(this.CurrentCategoryCollectionTemp[0]);
        }

        #endregion

        #region Clear Detail

        public void ClearDetail()
        {
            this.CurrentCategory = new Category();
            this.CurrentCategoryTemp = new Category();
        }

        #endregion

        #region etc

        public void ClearSelection()
        {
            this.CurrentCategoryCollectionTemp = new ObservableCollection<Category>();
            this.Presenter.Reload("Category");
        }

        public void Close()
        {
            this.Presenter.CloseTab(this);
            this.Presenter.Reload("Category");
        }

        #endregion

        #endregion

        #region Equality

        public bool Equals(SetupDataCategoryPresenter other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (this.GetType() == other.GetType());
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as SetupDataCategoryPresenter);
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

        public static bool operator ==(SetupDataCategoryPresenter x, SetupDataCategoryPresenter y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(SetupDataCategoryPresenter x, SetupDataCategoryPresenter y)
        {
            return !(x == y);
        }

        #endregion
    }
}
