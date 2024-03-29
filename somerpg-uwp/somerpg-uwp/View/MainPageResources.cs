﻿using Microsoft.Graphics.Canvas;
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
        List<CanvasGeometry> innerTriangles = new List<CanvasGeometry>();
        CanvasGeometry hex;
        CanvasSolidColorBrush blackBrush;

        public static Dictionary<string, object> DrawingResources { get; } = new Dictionary<string, object>();

        //Add images to dictionary here
        async Task CreateResourcesAsync(CanvasAnimatedControl sender)
        {
            //Textures for standart mode
            images.Add(
                "FlatTile", await CanvasBitmap.LoadAsync(sender, new Uri(
                    "ms-appx:///Textures/FlatYellow.png", UriKind.RelativeOrAbsolute)));
            images.Add(
                "Forest", await CanvasBitmap.LoadAsync(sender, new Uri(
                    "ms-appx:///Textures/Forest.png", UriKind.RelativeOrAbsolute)));
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


            //Adding inner hex triangles
            innerTriangles.Add(CanvasGeometry.CreatePolygon(sender, new Vector2[3]
            {
                new Vector2(50, 80),
                new Vector2(100, 140),
                new Vector2(0, 140)
            }));
            innerTriangles.Add(CanvasGeometry.CreatePolygon(sender, new Vector2[3]
            {
                new Vector2(50, 80),
                new Vector2(100, 140),
                new Vector2(150, 80)
            }));
            innerTriangles.Add(CanvasGeometry.CreatePolygon(sender, new Vector2[3]
            {
                new Vector2(200, 140),
                new Vector2(100, 140),
                new Vector2(150, 80)
            }));
            innerTriangles.Add(CanvasGeometry.CreatePolygon(sender, new Vector2[3]
            {
                new Vector2(50, 200),
                new Vector2(100, 140),
                new Vector2(0, 140)
            }));
            innerTriangles.Add(CanvasGeometry.CreatePolygon(sender, new Vector2[3]
            {
                new Vector2(200, 140),
                new Vector2(100, 140),
                new Vector2(150, 200)
            }));
            innerTriangles.Add(CanvasGeometry.CreatePolygon(sender, new Vector2[3]
            {
                new Vector2(100, 140),
                new Vector2(50, 200),
                new Vector2(150, 200)
            }));

            //Brushes for level map mode
            brushes.Add(-2, new CanvasSolidColorBrush(sender, Colors.Red));
            brushes.Add(-1, new CanvasSolidColorBrush(sender, Colors.Orange));
            brushes.Add(0, new CanvasSolidColorBrush(sender, Colors.White));
            brushes.Add(1, new CanvasSolidColorBrush(sender, Colors.LightGreen));
            brushes.Add(2, new CanvasSolidColorBrush(sender, Colors.DarkGreen));

            //Standart black brush
            blackBrush = new CanvasSolidColorBrush(sender, Color.FromArgb(60, 0, 0, 0));

            //Adding water brush
            DrawingResources.Add("WaterBrush", new CanvasImageBrush(sender, await CanvasBitmap.LoadAsync(sender, new Uri("ms-appx:///Textures/Water.png", UriKind.RelativeOrAbsolute)))
            {
                ExtendX = CanvasEdgeBehavior.Mirror,
                ExtendY = CanvasEdgeBehavior.Wrap
            });


        }
    }
}
