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
        public WorldMap worldMap;
        Polygon polygon = new Polygon();
        Point rmbPressedPoint = new Point(0,0);

        public MainWindow(WorldMap worldMap)
        {
            this.worldMap = worldMap;
            InitializeComponent();
            IniCanvas();
            GenerateImages();

            //DrawMapImage();
            //DrawPolygons();
        }

        void IniCanvas()
        {
            var s = worldMap.GetSize();
            var (width, height) = HexagonalMap.GetMapPixelSize((s.X, s.Y));

            //canvas.Background = Brushes.DarkGray;

            canvas.Width = width;
            canvas.Height = height;

            canvas.RenderTransform = new ScaleTransform
            {
                CenterX = canvas.Width / 2,
                CenterY = canvas.Width / 2
            };
        }
        void GenerateImages()
        {
            foreach (WorldTile item in worldMap)
            {
                var textures = item.TextureUris;
                for (int i = 0; i < textures.Count; i++)
                {
                    Image img = new Image
                    {
                        Source = new BitmapImage(textures[i])
                    };

                    var pixelCoord = HexagonalMap.HexToPixel(item.Coord);

                    Canvas.SetTop(img, pixelCoord.Y);
                    Canvas.SetLeft(img, pixelCoord.X);

                    Panel.SetZIndex(img, i);

                    canvas.Children.Add(img);
                }
            }
        }
        void DrawPolygons()
        {
            foreach (Tile item in worldMap)
            {
                canvas.Children.Add(GetLocatedPolygon(item.Coord));
            }
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

            //p.MouseEnter += P_MouseEnter;
            //p.MouseLeave += P_MouseLeave;

            //p.Fill = Brushes.Transparent;

            Panel.SetZIndex(p, 0);

            p.Stroke = Brushes.Black;

            return p;
        }

        void DrawHighlightPolygonByPixel(Point p)
        {
            var tileCoord = HexagonalMap.PixelToHex(p);
            DrawClickedPolygon(tileCoord);
            Console.WriteLine(tileCoord);
        }
        void DrawClickedPolygon(System.Drawing.Point point)
        {
            canvas.Children.Remove(polygon);
            var p = GetLocatedPolygon(point);
            polygon = p;
            canvas.Children.Add(p);
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
        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DrawHighlightPolygonByPixel(e.GetPosition(canvas));
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            //DrawHighlightPolygonByPixel(e.GetPosition(canvas));
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var xOffset = e.GetPosition(window).X - rmbPressedPoint.X;
                var yOffset = e.GetPosition(window).Y - rmbPressedPoint.Y;

                var x = canvas.Margin.Left + xOffset; 
                var y = canvas.Margin.Top + yOffset;

                canvas.Margin = new Thickness(x, y, 0, 0);
            }
            rmbPressedPoint = e.GetPosition(window);
        }
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            float scale;
            if (e.Delta < 0)
            {
                scale = 0.95f;
            }
            else
            {
                scale = 1.05f;
            }


            ScaleTransform s = (ScaleTransform)canvas.RenderTransform;
            s.ScaleX *= scale;
            s.ScaleY *= scale;
        }
    }
}
