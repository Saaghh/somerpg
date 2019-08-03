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
        static readonly (int x, int y) mapSize = (x: 20, y: 20);

        public BattleMap battleMap;

        public BattleWindow()
        {
            InitializeComponent();

            var size = HexMap.GetMapPixelSize(mapSize);
            mapContainer.Width = size.width;
            mapContainer.Height = size.height;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            battleMap = new BattleMap(mapSize, this);
        }
    }
}
