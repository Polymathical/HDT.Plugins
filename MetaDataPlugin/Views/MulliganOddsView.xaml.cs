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
using CoreAPI = Hearthstone_Deck_Tracker.API.Core;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Windows;
using HDT.Plugins.Custom.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace HDT.Plugins.Custom.Controls
{
    /// <summary>
    /// Interaction logic for MulliganOddsView.xaml
    /// </summary>
    public partial class MulliganOddsView : UserControl
    {
        internal MulliganOddsViewModel MulliganOddsVM => DataContext as MulliganOddsViewModel;

        // 1920x1080 Coords
        // Y = 680
        // 3 CARDS -513, 858, 1201
        // 4 cards X = 474, 729, 986, 1244
        // CARD WIDTH 215

        static readonly Point[] threeCardPositioning = new Point[]
            {
                new Point(513, 680),
                new Point(858, 680),
                new Point(1201, 680)
            };

        static readonly Point[] fourCardPositioning = new Point[]
        {
                new Point(474, 680),
                new Point(729, 680),
                new Point(986, 680),
                new Point(1244, 680)
        };


        public MulliganOddsView()
        {
            InitializeComponent();

            DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(Canvas)).AddValueChanged(CoreAPI.OverlayCanvas, UpdatePosition);
            DependencyPropertyDescriptor.FromProperty(Canvas.TopProperty, typeof(Canvas)).AddValueChanged(CoreAPI.OverlayCanvas, UpdatePosition);
            DependencyPropertyDescriptor.FromProperty(Canvas.ActualWidthProperty, typeof(Canvas)).AddValueChanged(CoreAPI.OverlayCanvas, UpdatePosition);
            DependencyPropertyDescriptor.FromProperty(Canvas.ActualHeightProperty, typeof(Canvas)).AddValueChanged(CoreAPI.OverlayCanvas, UpdatePosition);
        }

        public void UpdatePosition(object sender, EventArgs e)
        {
        
            if (MulliganOddsVM == null)
                return;

            var cardCount = MulliganOddsVM.MulliganCardOdds.Count();

            Point[] cardPositions = null;

            if (cardCount == 3)
                cardPositions = threeCardPositioning;
            else if (cardCount == 4)
                cardPositions = fourCardPositioning;
            else
            {
                Debug.WriteLine($"Card Count is {cardCount}");
                return;
            }
            
            var listItems = Helpers.GetUIElementsFromItemsControl(MulliganOddsItems);
            
            if (cardPositions.Count() != listItems.Count)
            {
                Debug.WriteLine("ListItemCount not equal to CardPositions Count");
                return;
            }

            int curItem = 0;
            foreach (UIElement uie in listItems.Where(li => li != null))
            {
                var p = cardPositions[curItem];
                (double x, double y) = Helpers.FromAbsoluteRefToOverlay(p.X, p.Y, CoreAPI.OverlayCanvas.ActualWidth, CoreAPI.OverlayCanvas.ActualHeight);
                 
                Canvas.SetTop(uie, y);
                Canvas.SetLeft(uie, x);
                curItem++;
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
            DependencyPropertyDescriptor.FromProperty(Canvas.LeftProperty, typeof(Canvas)).RemoveValueChanged(CoreAPI.OverlayCanvas, UpdatePosition);
            DependencyPropertyDescriptor.FromProperty(Canvas.TopProperty, typeof(Canvas)).RemoveValueChanged(CoreAPI.OverlayCanvas, UpdatePosition);
            DependencyPropertyDescriptor.FromProperty(Canvas.ActualWidthProperty, typeof(Canvas)).RemoveValueChanged(CoreAPI.OverlayCanvas, UpdatePosition);
            DependencyPropertyDescriptor.FromProperty(Canvas.ActualHeightProperty, typeof(Canvas)).RemoveValueChanged(CoreAPI.OverlayCanvas, UpdatePosition);
        }
    }
}
