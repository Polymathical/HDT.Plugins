using System.Windows;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.API;
using System;
using Hearthstone_Deck_Tracker.Utility.Logging;
using System.ComponentModel;

namespace HDT.Plugins.MassiveDynamic
{
    /// <summary>
    ///  
    /// </summary>
    public partial class MulliganOddsWindow : IDisposable
    {
        UIElement opponentPanel => Core.OverlayCanvas.FindName("BorderStackPanelOpponent") as UIElement;

        public MulliganOddsWindow(WindowViewModel vm) : base()
        {
            this.DataContext = vm;
        }

        public MulliganOddsWindow()
        {
            InitializeComponent();

            DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(Border)).AddValueChanged(opponentPanel, UpdatePosition);
            DependencyPropertyDescriptor.FromProperty(Canvas.TopProperty, typeof(Border)).AddValueChanged(opponentPanel, UpdatePosition);
            DependencyPropertyDescriptor.FromProperty(Canvas.ActualWidthProperty, typeof(Border)).AddValueChanged(opponentPanel, UpdatePosition);
        }

        internal void UpdatePosition(object sender, EventArgs e)
        {
            var panelLeft = opponentPanel?.GetValue(Canvas.LeftProperty) as double?;
            var panelTop = opponentPanel?.GetValue(Canvas.TopProperty) as double?;
            var panelWidth = opponentPanel?.GetValue(Canvas.ActualWidthProperty) as double?;
            // Log.WriteLine("MulliganOdds: " + panelLeft + " " + panelTop + " " + panelWidth, LogType.Info);

            if (opponentPanel != null && panelLeft != null && panelTop is double)
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
            DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(Border)).RemoveValueChanged(opponentPanel, UpdatePosition);
            DependencyPropertyDescriptor.FromProperty(Canvas.TopProperty, typeof(Border)).RemoveValueChanged(opponentPanel, UpdatePosition);
            DependencyPropertyDescriptor.FromProperty(Canvas.ActualWidthProperty, typeof(Border)).RemoveValueChanged(opponentPanel, UpdatePosition);
        }

        private void DisplayList_LayoutUpdated(object sender, EventArgs e)
        {
            UpdatePosition(sender, e);
        }
    }
}
