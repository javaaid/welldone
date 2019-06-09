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
    public class TFormReceiptPresenter : PresenterBase<TFormReceiptView>
    {
        #region Private Fields
        #endregion

        #region ctor

        public TFormReceiptPresenter(AppPresenter appPresenter, TFormReceiptView view)
            : base(view, "TabHeader")
        {
 
        }

        #endregion

        #region Public Properties

        public string TabHeader {
            get {
                return "Form Transaksi Penerimaan";
            }
        }

        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        #endregion

        #region Equality

        public bool Equals(TFormReceiptPresenter other)
        {
            if (object.ReferenceEquals(null, other))
                return false;

            if (object.ReferenceEquals(this, other))
                return true;

            return (this.GetType() == other.GetType());
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TFormReceiptPresenter);
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

        public static bool operator ==(TFormReceiptPresenter x, TFormReceiptPresenter y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (object.ReferenceEquals(null, x))
                return false;

            return (x.Equals(y));
        }

        public static bool operator !=(TFormReceiptPresenter x, TFormReceiptPresenter y)
        {
            return !(x == y);
        }

        #endregion
    }
}
