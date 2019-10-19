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
    public enum ResourceKey
    {
        //Terrains
        GrasslandTerrain,
        
        //Landscapes
        ForestLandscape,
        MountainLandscape,
        
        //Geometries
        HexGeometry,

        TriangleTL,
        TriangleT,
        TriangleTR,
        TriangleBR,
        TriangleB,
        TriangleBL,
        
        //Brushes
        RedBrush,
        OrangeBrush,
        WhiteBrush,
        LightGreenBrush,
        DarkGreenBrush,
        BlackBrush,
        
        //Misc
        HighlightPolygonImage,
        WaterImageBrush,
        ErrorImage
    }

    public sealed partial class MainPage : Page
    {
        //Dictionary<string, CanvasBitmap> images = new Dictionary<string, CanvasBitmap>();
        //Dictionary<int, CanvasSolidColorBrush> brushes = new Dictionary<int, CanvasSolidColorBrush>();
        //List<CanvasGeometry> innerTriangles = new List<CanvasGeometry>();
        //CanvasGeometry hex;
        //CanvasSolidColorBrush blackBrush;

        public static Dictionary<ResourceKey, object> DrawingResources { get; } = new Dictionary<ResourceKey, object>();

        //Add images to dictionary here
        async Task CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            //Adding image textures
            DrawingResources.Add(
                ResourceKey.GrasslandTerrain, await CanvasBitmap.LoadAsync(sender, new Uri(
                "ms-appx:///Textures/FlatYellow.png", UriKind.RelativeOrAbsolute)));
            DrawingResources.Add(
                ResourceKey.ForestLandscape, await CanvasBitmap.LoadAsync(sender, new Uri(
                "ms-appx:///Textures/Forest.png", UriKind.RelativeOrAbsolute)));
            DrawingResources.Add(
                ResourceKey.HighlightPolygonImage, await CanvasBitmap.LoadAsync(sender, new Uri(
                "ms-appx:///Textures/Highlight.png", UriKind.RelativeOrAbsolute)));
            DrawingResources.Add(
                ResourceKey.MountainLandscape, await CanvasBitmap.LoadAsync(sender, new Uri(
                "ms-appx:///Textures/Mountain.png", UriKind.RelativeOrAbsolute)));
            DrawingResources.Add(
                ResourceKey.ErrorImage, await CanvasBitmap.LoadAsync(sender, new Uri(
                "ms-appx:///Textures/Error.png", UriKind.RelativeOrAbsolute)));

            //Standart hex
            DrawingResources.Add(ResourceKey.HexGeometry, CanvasGeometry.CreatePolygon(sender, new Vector2[6]
            {
                new Vector2(50, 80),
                new Vector2(150, 80),
                new Vector2(200, 140),
                new Vector2(150, 200),
                new Vector2(50, 200),
                new Vector2(0, 140)
            }));


            //Adding inner hex triangles
            DrawingResources.Add(ResourceKey.TriangleTL, CanvasGeometry.CreatePolygon(sender, new Vector2[3]
            {
                new Vector2(50, 80),
                new Vector2(100, 140),
                new Vector2(0, 140)
            }));
            DrawingResources.Add(ResourceKey.TriangleT , CanvasGeometry.CreatePolygon(sender, new Vector2[3]
            {
                new Vector2(50, 80),
                new Vector2(100, 140),
                new Vector2(150, 80)
            }));
            DrawingResources.Add(ResourceKey.TriangleTR, CanvasGeometry.CreatePolygon(sender, new Vector2[3]
            {
                new Vector2(200, 140),
                new Vector2(100, 140),
                new Vector2(150, 80)
            }));
            DrawingResources.Add(ResourceKey.TriangleBR, CanvasGeometry.CreatePolygon(sender, new Vector2[3]
            {
                new Vector2(50, 200),
                new Vector2(100, 140),
                new Vector2(0, 140)
            }));
            DrawingResources.Add(ResourceKey.TriangleB , CanvasGeometry.CreatePolygon(sender, new Vector2[3]
            {
                new Vector2(200, 140),
                new Vector2(100, 140),
                new Vector2(150, 200)
            }));
            DrawingResources.Add(ResourceKey.TriangleBL, CanvasGeometry.CreatePolygon(sender, new Vector2[3]
            {
                new Vector2(100, 140),
                new Vector2(50, 200),
                new Vector2(150, 200)
            }));

            //Brushes for level map mode
            DrawingResources.Add(ResourceKey.RedBrush, new CanvasSolidColorBrush(sender, Colors.Red));
            DrawingResources.Add(ResourceKey.OrangeBrush, new CanvasSolidColorBrush(sender, Colors.Orange));
            DrawingResources.Add(ResourceKey.WhiteBrush, new CanvasSolidColorBrush(sender, Colors.White));
            DrawingResources.Add(ResourceKey.LightGreenBrush, new CanvasSolidColorBrush(sender, Colors.LightGreen));
            DrawingResources.Add(ResourceKey.DarkGreenBrush, new CanvasSolidColorBrush(sender, Colors.DarkGreen));

            //Standart black brush
            DrawingResources.Add(ResourceKey.BlackBrush, new CanvasSolidColorBrush(sender, Color.FromArgb(60, 0, 0, 0)));

            //Adding water brush
            DrawingResources.Add(ResourceKey.WaterImageBrush, new CanvasImageBrush(sender, await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Textures/Water.png", UriKind.RelativeOrAbsolute)))
            {
                ExtendX = CanvasEdgeBehavior.Mirror,
                ExtendY = CanvasEdgeBehavior.Wrap
            });
        }
    }
}
