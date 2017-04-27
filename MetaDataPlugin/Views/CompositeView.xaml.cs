using Hearthstone_Deck_Tracker.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace HDT.Plugins.Custom.Views
{
    /// <summary>
    /// Interaction logic for CompositeView.xaml
    /// </summary>
    public partial class CompositeView : UserControl
    {
        UIElement OpponentPanel => Core.OverlayCanvas?.FindName("BorderStackPanelOpponent") as UIElement;

        public CompositeView()
        {
            InitializeComponent();

        
                DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(Border)).AddValueChanged(OpponentPanel, UpdatePosition);
                DependencyPropertyDescriptor.FromProperty(Canvas.TopProperty, typeof(Border)).AddValueChanged(OpponentPanel, UpdatePosition);
                DependencyPropertyDescriptor.FromProperty(Canvas.ActualWidthProperty, typeof(Border)).AddValueChanged(OpponentPanel, UpdatePosition);
          
        }

        internal void UpdatePosition(object sender, EventArgs e)
        {
            var panelLeft = OpponentPanel?.GetValue(Canvas.LeftProperty) as double?;
            var panelTop = OpponentPanel?.GetValue(Canvas.TopProperty) as double?;
            var panelWidth = OpponentPanel?.GetValue(Canvas.ActualWidthProperty) as double?;

            if (OpponentPanel != null && panelLeft != null && panelTop is double)
            {
                Canvas.SetTop(this, (double)panelTop);
                Canvas.SetLeft(this, (double)(panelLeft + panelWidth) + 5);
            }
            else
            {
                Canvas.SetTop(this, Core.OverlayCanvas.Height * 25 / 100);
                Canvas.SetLeft(this, Core.OverlayCanvas.Width * 20 / 100);
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

        public void SetMulliganVisibility(Visibility vis)
        {
            MulliganView.Visibility = vis;
        }

        public void Dispose()
        {
            DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(Border)).RemoveValueChanged(OpponentPanel, UpdatePosition);
            DependencyPropertyDescriptor.FromProperty(Canvas.TopProperty, typeof(Border)).RemoveValueChanged(OpponentPanel, UpdatePosition);
            DependencyPropertyDescriptor.FromProperty(Canvas.ActualWidthProperty, typeof(Border)).RemoveValueChanged(OpponentPanel, UpdatePosition);
        }

        private void DisplayList_LayoutUpdated(object sender, EventArgs e)
        {
            UpdatePosition(sender, e);
        }
    }
}
