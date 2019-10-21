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
        Point rmbPressedPoint = new Point(0, 0);
        System.Drawing.Point highlightPoint;

        public MainPage()
        {
            this.InitializeComponent();
            worldMap = new WorldMap(33333);
            IniCanvas();
        }

        private void IniCanvas()
        {
            canvas.TargetElapsedTime = new TimeSpan(133333);
        }


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

                    //var x = canvas.Margin.Left + xOffset;
                    //var y = canvas.Margin.Top + yOffset;

                    //canvas.Margin = new Thickness(x, y, 0, 0);

                    globalOffsetX += Convert.ToInt32(xOffset);
                    globalOffsetY += Convert.ToInt32(yOffset);

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
                    scale = 0.97f;
                }
                else
                {
                    scale = 1.03f;
                }

                //ScaleTransform s = (ScaleTransform)canvas.RenderTransform;
                //s.ScaleX *= scale;
                //s.ScaleY *= scale;

                //canvas.Scale *= scale;

                //canvas.Size *= scale;

                //canvas.Height = 5000;
                //canvas.Width = 5000;


                //RefreshWindowSizeData();



                //canvas.Invalidate();

            }
        }

        private void canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(canvas);

            var x = Convert.ToInt32(point.Position.X) - globalOffsetX;
            var y = Convert.ToInt32(point.Position.Y) - globalOffsetY;

            highlightPoint = HexagonalMap.HexToPixel(HexagonalMap.PixelToHex(new System.Drawing.Point(x, y)));
        }

        private void canvas_CreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResourcesAsync(sender).AsAsyncAction());
        }

        private void toggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            var s = sender as ToggleSwitch;
            Settings.DrawLevels = s.IsOn;
        }

        private void toggleSwitch1_Toggled(object sender, RoutedEventArgs e)
        {
            var s = sender as ToggleSwitch;
            Settings.DrawStandart = s.IsOn;
        }

        private void toggleSwitch2_Toggled(object sender, RoutedEventArgs e)
        {
            var s = sender as ToggleSwitch;
            Settings.DrawInnerTriangles = s.IsOn;
        }

        private void toggleSwitch3_Toggled(object sender, RoutedEventArgs e)
        {
            var s = sender as ToggleSwitch;
            Settings.DrawPolygons = s.IsOn;
        }

        private void toggleSwitch4_Toggled(object sender, RoutedEventArgs e)
        {
            var s = sender as ToggleSwitch;
            Settings.DrawHighlightedPolygon = s.IsOn;
        }

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshWindowSizeData();
        }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshWindowSizeData();
            canvas.Paused = false;
        }
        
        private void RefreshWindowSizeData()
        {
            //ScaleTransform s = (ScaleTransform)canvas.RenderTransform;

            var w = Window.Current.Content.ActualSize;

            windowHeight = Convert.ToInt32(w.Y);
            windowWidth = Convert.ToInt32(w.X);
        }
    }
}
