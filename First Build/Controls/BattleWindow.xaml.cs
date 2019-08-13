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
        static readonly (int x, int y) mapSize = (x: 20, y: 20);

        public Battle battle;

        public BattleWindow()
        {
            InitializeComponent();

            var size = HexMap.GetMapPixelSize(mapSize);
            mapContainer.Width = size.width;
            mapContainer.Height = size.height;

            battle = new Battle(mapSize, this);

            image.Source = battle.textureSource;

            battle.BattleEnded += Battle_BattleEnded;
        }

        private void Battle_BattleEnded(object sender, Battle.BattleEndEventArgs e)
        {
            MessageBox.Show(e.message);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - 50);
                    break;
                case Key.A:
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - 50);
                    break;
                case Key.S:
                    scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + 50);
                    break;
                case Key.D:
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + 50);
                    break;
            }
        }
    }
}
