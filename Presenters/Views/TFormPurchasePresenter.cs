using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using WelldonePOS.Helpers;
using WelldonePOS.Models.DataModels;
using WelldonePOS.Models.Enums;
using WelldonePOS.Presenters.Shells;
using WelldonePOS.UserControls.Views;

namespace WelldonePOS.Presenters.Views
{
    public class TFormPurchasePresenter : PresenterBase<TFormPurchaseView>
    {
        #region Private Fields
        #endregion

        #region ctor

        public TFormPurchasePresenter(AppPresenter appPresenter, TFormPurchaseView view)
            : base(view, "TabHeader")
        {
 
        }

        #endregion

        #region Public Properties

        public string TabHeader {
            get {
                return "Form Transaksi Pembayaran";
            }
        }

        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        #endregion

        #region Equality

        public bool Equals(TFormPurchasePresenter other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (this.GetType() == other.GetType());
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TFormPurchasePresenter);
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

        public static bool operator ==(TFormPurchasePresenter x, TFormPurchasePresenter y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(TFormPurchasePresenter x, TFormPurchasePresenter y)
        {
            return !(x == y);
        }

        #endregion
    }
}
