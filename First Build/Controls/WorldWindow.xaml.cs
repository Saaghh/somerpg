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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace First_Build.Controls.BattleControls
{
    /// <summary>
    /// Логика взаимодействия для WorldWindow.xaml
    /// </summary>
    public partial class WorldWindow : Window
    {
        WorldMap worldMap;
        public WorldWindow()
        {
            InitializeComponent();
            worldMap = new WorldMap();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            image.Source = Imaging.CreateBitmapSourceFromHBitmap(worldMap.GetMapTexture().GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
