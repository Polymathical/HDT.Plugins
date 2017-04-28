using Hearthstone_Deck_Tracker.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HDT.Plugins.Custom.Controls
{
    /// <summary>
    /// Interaction logic for MulliganOddsView.xaml
    /// </summary>
    public partial class MulliganOddsView : UserControl
    {

        public MulliganOddsView()
        {
            InitializeComponent();
        }

        public void UpdatePosition()
        {
            try
            {
                var h = Core.OverlayCanvas.ActualHeight;
                var w = Core.OverlayCanvas.ActualHeight;

                Canvas.SetTop(this, h * .5 - this.ActualHeight / 2);
                Canvas.SetLeft(this, w * .5 - this.ActualWidth / 2);
            }
            catch
            {
                Debug.WriteLine("Failed to position MulliganOddsView");
            }
           
        }

        public void Show()
        {
            this.Visibility = Visibility.Visible;
        }

        public void Hide()
        {

            this.Visibility = Visibility.Hidden;
        }

        public void Dispose()
        {

        }
    }
}
