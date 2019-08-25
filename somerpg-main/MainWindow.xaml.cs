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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace somerpg_main
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly WorldMap worldMap = new WorldMap();
        public MainWindow()
        {
            InitializeComponent();
            IniCanvas();
            GenerateImages();
            //DrawMapImage();
            DrawPolygons();
        }

        void GenerateImages()
        {
            foreach (Tile item in worldMap)
            {
                Image i = new Image
                {
                    Source = new BitmapImage(item.Terrain.textureUri)
                };
                TextBlock t = new TextBlock();
                t.Text = item.Coord.ToString();

                var pixelCoord = HexagonalMap.HexToPixel(item.Coord);

                Canvas.SetTop(i, pixelCoord.Y);
                Canvas.SetLeft(i, pixelCoord.X);

                Canvas.SetTop(t, pixelCoord.Y + 140);
                Canvas.SetLeft(t, pixelCoord.X + 70);

                t.Foreground = Brushes.White;

                canvas.Children.Add(i);
                canvas.Children.Add(t);
            }
        }

        void IniCanvas()
        {
            var s = worldMap.GetSize();
            var (width, height) = HexagonalMap.GetMapPixelSize((s.X, s.Y));

            canvas.Background = Brushes.DarkGray;

            canvas.Width = width;
            canvas.Height = height;
        }

        void DrawPolygons()
        {
            foreach (Tile item in worldMap)
            {
                canvas.Children.Add(GetLocatedPolygon(item.Coord));
            }
        }

        Polygon GetLocatedPolygon(System.Drawing.Point coord)
        {
            Polygon p = new Polygon();
            var offset = HexagonalMap.HexToPixel(coord);
            offset.Y += 80;
            p.Points.Add(new Point(0, 60));
            p.Points.Add(new Point(50, 120));
            p.Points.Add(new Point(150, 120));
            p.Points.Add(new Point(200, 60));
            p.Points.Add(new Point(150, 0));
            p.Points.Add(new Point(50, 0));

            Canvas.SetTop(p, offset.Y);
            Canvas.SetLeft(p, offset.X);

            p.MouseEnter += P_MouseEnter;
            p.MouseLeave += P_MouseLeave;

            p.Fill = Brushes.Transparent;

            Panel.SetZIndex(p, 0);

            p.Stroke = Brushes.Black;

            return p;
        }

        void DrawMapImage()
        {
            var image = new Image();

            var s = Imaging.CreateBitmapSourceFromHBitmap(
                worldMap.GetMapTexture().GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            image.Source = s;

            canvas.Children.Add(image);
        }

        private void P_MouseLeave(object sender, MouseEventArgs e)
        {
            var p = sender as Polygon;
            p.Fill = Brushes.Transparent;
        }

        private void P_MouseEnter(object sender, MouseEventArgs e)
        {
            var p = sender as Polygon;
            p.Fill = Brushes.LightGray;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            HexagonalMap.PixelToHex(e.GetPosition(canvas));
            listBox.Items.Add(e.GetPosition(canvas));
            Console.WriteLine(e.GetPosition(canvas));
        }
    }
}
