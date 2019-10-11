using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI;

namespace somerpg_uwp
{
    public sealed partial class MainPage : Page
    {
        Dictionary<string, CanvasBitmap> images = new Dictionary<string, CanvasBitmap>();
        Dictionary<int, CanvasSolidColorBrush> brushes = new Dictionary<int, CanvasSolidColorBrush>();
        CanvasGeometry hex;
        
        //Add images to dictionary here
        async Task CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            //Textures for standart mode
            images.Add(
                "FlatTile", await CanvasBitmap.LoadAsync(sender, new Uri(
                    "ms-appx:///Textures/FlatYellow.png", UriKind.RelativeOrAbsolute)));
            images.Add(
                "Forest", await CanvasBitmap.LoadAsync(sender, new Uri(
                    "ms-appx:///Textures/PineForest.png", UriKind.RelativeOrAbsolute)));
            images.Add(
                "Highlight", await CanvasBitmap.LoadAsync(sender, new Uri(
                    "ms-appx:///Textures/Highlight.png", UriKind.RelativeOrAbsolute)));
            images.Add(
                "Water", await CanvasBitmap.LoadAsync(sender, new Uri(
                    "ms-appx:///Textures/Water.png", UriKind.RelativeOrAbsolute)));
            images.Add(
                "Mountain", await CanvasBitmap.LoadAsync(sender, new Uri(
                    "ms-appx:///Textures/Mountain.png", UriKind.RelativeOrAbsolute)));

            //Standart hex
            hex = CanvasGeometry.CreatePolygon(sender, new Vector2[6]
            {
                new Vector2(50, 80),
                new Vector2(150, 80),
                new Vector2(200, 140),
                new Vector2(150, 200),
                new Vector2(50, 200),
                new Vector2(0, 140)
            });

            //Brushes for level map mode
            brushes.Add(-2, new CanvasSolidColorBrush(sender, Colors.Red));
            brushes.Add(-1, new CanvasSolidColorBrush(sender, Colors.Orange));
            brushes.Add(0, new CanvasSolidColorBrush(sender, Colors.White));
            brushes.Add(1, new CanvasSolidColorBrush(sender, Colors.LightGreen));
            brushes.Add(2, new CanvasSolidColorBrush(sender, Colors.DarkGreen));
        }
    }
}
