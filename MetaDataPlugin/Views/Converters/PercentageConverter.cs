using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace HDT.Plugins.Custom.Converters
{
    public class PercentageConverter : MarkupExtension, IMultiValueConverter
    {
        private static PercentageConverter _instance;

        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Double.TryParse(values[0].ToString(), out var paramOne);
            Double.TryParse(values[1].ToString(), out var paramTwo);
            Double.TryParse(values[2].ToString(), out var paramThree);

            return paramOne * paramTwo * paramThree;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _instance ?? (_instance = new PercentageConverter());
        }

      
    }
}
