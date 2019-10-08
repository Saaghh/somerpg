using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
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
using System.Threading.Tasks;
using System.Collections.ObjectModel;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace somerpg_uwp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        WorldMap worldMap;
        CanvasBitmap[,] canvasImages;
        public MainPage()
        {
            this.InitializeComponent();
            worldMap = new WorldMap();
            canvasImages = new CanvasBitmap[worldMap.GetSize().X, worldMap.GetSize().Y];

            IniCanvas();
            DrawImages();
        }

        Polygon polygon;

        void DrawHighlightPolygon(System.Drawing.Point hex)
        {
            if (polygon != null)
            {
                //canvas.Children.Remove(polygon);
            }

            var coord = HexagonalMap.HexToPixel(hex);

            var newPoly = new Polygon();

            newPoly.Points.Add(new Point(50, 0));
            newPoly.Points.Add(new Point(150, 0));
            newPoly.Points.Add(new Point(200, 60));
            newPoly.Points.Add(new Point(150, 120));
            newPoly.Points.Add(new Point(50, 120));
            newPoly.Points.Add(new Point(0, 60));

            newPoly.Stroke = new SolidColorBrush(Colors.Wheat);

            Canvas.SetLeft(newPoly, coord.X);
            Canvas.SetTop(newPoly, coord.Y + HexagonalMap.HEXPIXELHEIGHTOFFSET);

            //canvas.Children.Add(newPoly);

            polygon = newPoly;
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

        private void TestMethod()
        {
            canvas.Translation = new System.Numerics.Vector3(0, 0, 400);
        }
        private void DrawImages()
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

                //new Uri("ms-appx:///Textures/WorldFlatTile.png", UriKind.RelativeOrAbsolute)
                img.Source = new BitmapImage(item.TextureUris[0]);

                img.Width = 200;
                img.Height = 200;

                var pos = HexagonalMap.HexToPixel(item.Coord);

                Canvas.SetLeft(img, pos.X);
                Canvas.SetTop(img, pos.Y);

                //img.Stroke = new SolidColorBrush(Colors.Wheat);

                //canvas.Children.Add(img);
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

                    //canvas.Children.Add(img);
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


                    //var x = Convert.ToSingle(canvas.Translation.X + xOffset);
                    //var y = Convert.ToSingle(canvas.Translation.Y + yOffset);

                    //canvas.Translation = new System.Numerics.Vector3(x, y, 0);

                    var x = canvas.Margin.Left + xOffset;
                    var y = canvas.Margin.Top + yOffset;

                    canvas.Margin = new Thickness(x, y, 0, 0);

                    //canvas.Invalidate();
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
                    scale = 0.99f;
                }
                else
                {
                    scale = 1.01f;
                }


                ScaleTransform s = (ScaleTransform)canvas.RenderTransform;
                s.ScaleX *= scale;
                s.ScaleY *= scale;

                canvas.Invalidate();

            }
        }

        private void canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(canvas);

            var hex = HexagonalMap.PixelToHex(new System.Drawing.Point(Convert.ToInt32(point.Position.X), Convert.ToInt32(point.Position.Y)));

            DrawHighlightPolygon(hex);
        }


        private void canvas_CreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }


        CanvasBitmap canvasImage;
        async Task CreateResourcesAsync(CanvasAnimatedControl sender)
        {

            canvasImage = await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Textures/FlatTile.png", UriKind.RelativeOrAbsolute));

        }

        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            for (int i = 0; i < worldMap.GetSize().X; i++)
            {
                for (int j = 0; j < worldMap.GetSize().Y; j++)
                {
                    //var item = canvasImages[i, j];
                    var coord = HexagonalMap.HexToPixel(new System.Drawing.Point(i, j));

                    args.DrawingSession.DrawImage(canvasImage, coord.X, coord.Y);
                    //args.DrawingSession.DrawImage(canvasImages[i, j], coord.X, coord.Y);
                }
            }
        }

    }
}
