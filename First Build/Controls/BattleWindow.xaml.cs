using First_Build.Controller;
using First_Build.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace First_Build.View
{
    /// <summary>
    /// Логика взаимодействия для BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window
    {
        static readonly (int x, int y) mapSize = (x: 40, y: 40);


        float currentScrollOffsetX = 0;
        float currentScrollOffsetY = 0;

        public BattleVisualController batteController;

        public float CurrentScrollOffsetY
        {
            get => currentScrollOffsetY;
            set
            {
                currentScrollOffsetY = value;
                if (currentScrollOffsetY < 0) { currentScrollOffsetY = 0; }
                if (currentScrollOffsetY > scrollViewer.ScrollableHeight) { currentScrollOffsetY = (float)scrollViewer.ScrollableHeight; }

            }
        }
        public float CurrentScrollOffsetX
        {
            get => currentScrollOffsetX;
            set
            {
                currentScrollOffsetX = value;
                if (currentScrollOffsetX < 0) { currentScrollOffsetX = 0; }

                if (currentScrollOffsetX > scrollViewer.ScrollableWidth) { currentScrollOffsetX = (float)scrollViewer.ScrollableWidth; }
            }
        }

        public BattleWindow()
        {
            InitializeComponent();

            var size = HexMapMath.GetMapPixelSize(mapSize);
            mapContainer.Width = size.width;
            mapContainer.Height = size.height;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            batteController = new BattleVisualController(this);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    CurrentScrollOffsetY -= 50;
                    scrollViewer.ScrollToVerticalOffset(CurrentScrollOffsetY);
                    break;
                case Key.A:
                    CurrentScrollOffsetX -= 50;
                    scrollViewer.ScrollToHorizontalOffset(CurrentScrollOffsetX);
                    break;
                case Key.S:
                    CurrentScrollOffsetY += 50;
                    scrollViewer.ScrollToVerticalOffset(CurrentScrollOffsetY);
                    break;
                case Key.D:
                    CurrentScrollOffsetX += 50;
                    scrollViewer.ScrollToHorizontalOffset(CurrentScrollOffsetX);
                    break;
            }
        }
    }
}
