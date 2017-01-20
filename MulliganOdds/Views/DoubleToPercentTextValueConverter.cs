using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DeckTrackerCustom
{
    public class DoubleToPercentTextValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "NaN";

            var valueTypeCode = Type.GetTypeCode(value.GetType());

            if (valueTypeCode != TypeCode.Double && valueTypeCode != TypeCode.Single)
                return "NaN";

            var nfi = new NumberFormatInfo();
            nfi.PercentDecimalDigits = 0;
            nfi.PercentPositivePattern = 1;

            return String.Format(nfi, "{0:P}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double retValue = 0.0;

            if (value is string == false)
                return retValue;

            var valString = (string)value;
            if (Double.TryParse(valString, out retValue))
                return retValue;
            else
                return 0.0;

        }
    }
}
