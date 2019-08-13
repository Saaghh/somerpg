using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;

namespace First_Build.View
{
    /// <summary>
    /// Логика взаимодействия для BattleMapControl.xaml
    /// </summary>
    public partial class BattleMapControl : UserControl
    {
        public System.Drawing.Point coord;

        public BattleMapControl(System.Drawing.Point position)
        {
            InitializeComponent();
            coord = position;
        }
        private void Polygon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            polygon.Fill = Brushes.Gray;
        }

        private void Polygon_MouseLeave(object sender, MouseEventArgs e)
        {
            polygon.Fill = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0,0,0,0));
        }

        private void Polygon_MouseEnter(object sender, MouseEventArgs e)
        {
            polygon.Fill = Brushes.Gray;
        }

        private void Polygon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            polygon.Fill = Brushes.WhiteSmoke;
        }
    }
}
