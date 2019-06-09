using System;
using System.Windows.Markup;

namespace WelldonePOS.Helpers
{
    public class EnumBindingHelper : MarkupExtension
    {
        #region Private Fields

        private Type _enumType;

        #endregion

        #region ctor

        public EnumBindingHelper(Type enumType)
        {
            this._enumType = enumType;
        }

        #endregion

        #region Public Properties

        public Type EnumType {
            get {
                return this._enumType;
            }
            set {
                if (value != null)
                {
                    Type enumType = Nullable.GetUnderlyingType(value) ?? value;

                    if (!enumType.IsEnum)
                        throw new ArgumentException("Type must be an enum.");
                }

                this._enumType = value;
            }
        }

        #endregion

        #region Public Methods

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (this.EnumType == null)
                throw new InvalidOperationException("The Enum type must be specified.");

            Type actualType = Nullable.GetUnderlyingType(this.EnumType) ?? this.EnumType;
            Array values = Enum.GetValues(actualType);

            if (this.EnumType == actualType)
                return values;

            Array valuesTemp = Array.CreateInstance(actualType, values.Length + 1);

            values.CopyTo(valuesTemp, 1);

            return valuesTemp;
        }

        #endregion
    }
}
