using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace somerpg_uwp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public WorldMap worldMap;

        public MainPage()
        {
            this.InitializeComponent();
            worldMap = new WorldMap();
            IniCanvas();
            DrawPolygons();
        }

        private void IniCanvas()
        {
            var s = worldMap.GetSize();

            var (width, height) = HexagonalMap.GetMapPixelSize((s.X, s.Y));

            canvas.Width = width;
            canvas.Height = height;

            canvas.RenderTransform = new ScaleTransform
            {
                CenterX = canvas.Width / 2,
                CenterY = canvas.Height / 2
            };
        }

        private void DrawPolygons()
        {
            //var poly = new Polygon();
            //poly.Points.Add(new Point(50, 0));
            //poly.Points.Add(new Point(150, 0));
            //poly.Points.Add(new Point(200, 60));
            //poly.Points.Add(new Point(150, 120));
            //poly.Points.Add(new Point(50, 120));
            //poly.Points.Add(new Point(0, 60));

            foreach (Tile item in worldMap)
            {
                var img = new Image();

                //TODO: fix this shit
                img.Source = new BitmapImage(new Uri("ms-appx:///Textures/WorldFlatTile.png", UriKind.RelativeOrAbsolute));

                img.Width = 200;
                img.Height = 200;

                var pos = HexagonalMap.HexToPixel(item.Coord);

                Canvas.SetLeft(img, pos.X);
                Canvas.SetTop(img, pos.Y);

                //img.Stroke = new SolidColorBrush(Colors.Wheat);

                canvas.Children.Add(img);
            }
        }

        private void GenerateImages()
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

                    //Panel.SetZIndex(img, i);

                    canvas.Children.Add(img);
                }
            }
        }


        Point rmbPressedPoint = new Point(0, 0);

        private void canvas_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Pointer ptr = e.Pointer;

            if (ptr.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                Windows.UI.Input.PointerPoint ptrPt = e.GetCurrentPoint(maingrid);

                if (ptrPt.Properties.IsLeftButtonPressed)
                {
                    var xOffset = ptrPt.Position.X - rmbPressedPoint.X;
                    var yOffset = ptrPt.Position.Y - rmbPressedPoint.Y;

                    var x = canvas.Margin.Left + xOffset;
                    var y = canvas.Margin.Top + yOffset;

                    canvas.Margin = new Thickness(x, y, 0, 0);
                }

                rmbPressedPoint = ptrPt.Position;
            }

            //if (e.LeftButton == MouseButtonState.Pressed)
            //{
            //    var xOffset = e.GetPosition(window).X - rmbPressedPoint.X;
            //    var yOffset = e.GetPosition(window).Y - rmbPressedPoint.Y;

            //    var x = canvas.Margin.Left + xOffset;
            //    var y = canvas.Margin.Top + yOffset;

            //    canvas.Margin = new Thickness(x, y, 0, 0);
            //}
            //rmbPressedPoint = e.GetPosition(window);
        }

        private void canvas_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {
            Pointer ptr = e.Pointer;
            if (ptr.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {

                float scale;
                if (e.GetCurrentPoint(maingrid).Properties.MouseWheelDelta < 0)
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
}
