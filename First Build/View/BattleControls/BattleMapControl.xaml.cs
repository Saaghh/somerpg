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
    /// Логика взаимодействия для BattleMapControl.xaml
    /// </summary>
    public partial class BattleMapControl : UserControl
    {
        public BattleMapControl()
        {
            InitializeComponent();
        }
        private void Polygon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            polygon.Fill = Brushes.Gray;
        }

        private void Polygon_MouseLeave(object sender, MouseEventArgs e)
        {
            polygon.Fill = new SolidColorBrush(Color.FromArgb(0,0,0,0));
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
