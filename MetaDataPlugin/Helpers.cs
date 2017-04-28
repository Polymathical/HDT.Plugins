using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDT.Plugins.Custom
{
    public static class Helpers
    {
        public static string ToPercentString(double d, int precision = 0, bool includePercentSign = true)
        {
            var nfi = new NumberFormatInfo()
            {
                PercentDecimalDigits = precision
            };
            nfi.PercentSymbol = includePercentSign ? nfi.PercentSymbol : String.Empty;
            nfi.PercentPositivePattern = 1;

            return String.Format(nfi, "{0:P}", d);
        }


    }
}
