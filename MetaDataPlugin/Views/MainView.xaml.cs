using System.Windows;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.API;
using System;
using Hearthstone_Deck_Tracker.Utility.Logging;
using System.ComponentModel;
using System.Windows.Media;
using HDT.Plugins.Custom.ViewModels;

namespace HDT.Plugins.Custom
{
    public partial class MainView : IDisposable
    {
        UIElement OpponentPanel => Core.OverlayCanvas.FindName("BorderStackPanelOpponent") as UIElement;
 
        public MainView()
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
