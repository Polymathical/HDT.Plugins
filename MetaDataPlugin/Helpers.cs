using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        public static (double x, double y) FromAbsoluteRefToOverlay(double absoluteX, double absoluteY, double containerWidth, double containerHeight)
        {
            double retX = absoluteX / 1920 * containerWidth;
            double retY = absoluteY / 1080 * containerHeight;

            return (retX, retY);
        }
         
        public static List<UIElement> GetUIElementsFromItemsControl(ItemsControl control)
        {
            List<UIElement> listItems = new List<UIElement>();
            foreach (var item in control.Items)
            {
                UIElement uiElement = control.ItemContainerGenerator.ContainerFromItem(item) as UIElement;
                listItems.Add(uiElement);
            }
            return listItems;
        }
        public static bool InDesignMode=> (bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue);
    }
}
