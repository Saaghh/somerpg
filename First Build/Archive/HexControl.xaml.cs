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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace First_Build.View
{
    /// <summary>
    /// Логика взаимодействия для HexControl.xaml
    /// </summary>
    public partial class HexControl : UserControl
    {
        BaseTile data;
        public HexControl((int x, int y) coordinate)
        {
            InitializeComponent();
            data = new BaseTile(coordinate);
            text.Text = (data.coord.x + ", " + data.coord.y);
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Tile: " + data.coord.x + "; " + data.coord.y);
        }

        private void Polygon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine("Polygon: " + data.coord.x + "; " + data.coord.y);

        }

        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(image.Source.ToString());
        }
    }
}
