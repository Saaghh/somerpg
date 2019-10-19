using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Microsoft.Graphics.Canvas.Brushes;
using Windows.UI.Xaml;

namespace somerpg_uwp
{
    public sealed partial class MainPage : Page
    {
        int globalOffsetX = 0;
        int globalOffsetY = 0;

        int windowWidth;
        int windowHeight;

        //Canvas refresh cycle
        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            if (Settings.DrawStandart) { DrawTiles(args); }
            if (Settings.DrawHighlightedPolygon) { DrawHighlightedPolygon(args); }
        }

        private void DrawTileBorder(CanvasAnimatedDrawEventArgs args, int x, int y, Tile tile)
        {
            CanvasSolidColorBrush brush;
            switch (tile.WorldLevel)
            {
                case -2:
                    brush = DrawingResources[ResourceKey.RedBrush] as CanvasSolidColorBrush;
                    break;
                case -1:
                    brush = DrawingResources[ResourceKey.OrangeBrush] as CanvasSolidColorBrush;
                    break;
                case 1:
                    brush = DrawingResources[ResourceKey.LightGreenBrush] as CanvasSolidColorBrush;
                    break;
                case 2:
                    brush = DrawingResources[ResourceKey.DarkGreenBrush] as CanvasSolidColorBrush;
                    break;
                default:
                    brush = DrawingResources[ResourceKey.WhiteBrush] as CanvasSolidColorBrush;
                    break;
            }
            args.DrawingSession.DrawGeometry(DrawingResources[ResourceKey.HexGeometry] as CanvasGeometry, x, y, brush, 3f);
        }

        //Draw inner triangles
        private void DrawOrnament(CanvasAnimatedDrawEventArgs args, int x, int y)
        {
            args.DrawingSession.DrawGeometry(DrawingResources[ResourceKey.TriangleTL] as CanvasGeometry, x, y, DrawingResources[ResourceKey.BlackBrush] as CanvasSolidColorBrush);
            args.DrawingSession.DrawGeometry(DrawingResources[ResourceKey.TriangleT ] as CanvasGeometry, x, y, DrawingResources[ResourceKey.BlackBrush] as CanvasSolidColorBrush);
            args.DrawingSession.DrawGeometry(DrawingResources[ResourceKey.TriangleTR] as CanvasGeometry, x, y, DrawingResources[ResourceKey.BlackBrush] as CanvasSolidColorBrush);
            args.DrawingSession.DrawGeometry(DrawingResources[ResourceKey.TriangleBR] as CanvasGeometry, x, y, DrawingResources[ResourceKey.BlackBrush] as CanvasSolidColorBrush);
            args.DrawingSession.DrawGeometry(DrawingResources[ResourceKey.TriangleB ] as CanvasGeometry, x, y, DrawingResources[ResourceKey.BlackBrush] as CanvasSolidColorBrush);
            args.DrawingSession.DrawGeometry(DrawingResources[ResourceKey.TriangleBL] as CanvasGeometry, x, y, DrawingResources[ResourceKey.BlackBrush] as CanvasSolidColorBrush);
        }
        
        //Standart canvas tiles draw
        private void DrawTiles(CanvasAnimatedDrawEventArgs args)
        {
            int offsetX = globalOffsetX;
            int offsetY = globalOffsetY;


            //Drawing tiles
            for (int j = 0; j < worldMap.GetSize().Y; j++)
            {
                int k = 1;

                bool cycleEnd = false;
                bool odd = true;

                //Drawing odd tiles first
                while (!cycleEnd)
                {
                    var item = worldMap[k, j];

                    var x = item.DrawPoint.X + offsetX;
                    var y = item.DrawPoint.Y + offsetY;

                    if (x < windowWidth && x > -HexagonalMap.HEXPIXELWIDTH && y < windowHeight && y > -(HexagonalMap.HEXPIXELHEIGHT + HexagonalMap.HEXPIXELHEIGHTOFFSET))
                    {
                        //Drawing each tile
                        if (Settings.DrawStandart) { item.Draw(args, offsetX, offsetY); }
                        if (Settings.DrawInnerTriangles) { DrawOrnament(args, x, y); }
                        if (Settings.DrawPolygons) { DrawTileBorder(args, x, y, item); }
                    }

                    k += 2;

                    if (k >= worldMap.GetSize().X)
                    {
                        if (odd)
                        {
                            k = 0;
                            odd = false;
                        }
                        else
                        {
                            cycleEnd = true;
                        }
                    }
                }
            }

            //args.DrawingSession.FillGeometry(innerTriangles[0], 500, 400, DrawingResources["WaterBrush"] as ICanvasBrush);
            //args.DrawingSession.FillGeometry(innerTriangles[1], 500, 400, DrawingResources["WaterBrush"] as ICanvasBrush);
            //args.DrawingSession.FillGeometry(innerTriangles[2], 500, 400, DrawingResources["WaterBrush"] as ICanvasBrush);
            //args.DrawingSession.FillGeometry(innerTriangles[3], 500, 400, DrawingResources["WaterBrush"] as ICanvasBrush);
            //args.DrawingSession.FillGeometry(innerTriangles[4], 500, 400, DrawingResources["WaterBrush"] as ICanvasBrush);
            //args.DrawingSession.FillGeometry(innerTriangles[5], 500, 400, DrawingResources["WaterBrush"] as ICanvasBrush);
            //args.DrawingSession.FillGeometry(hex, 175, 0, DrawingResources["WaterBrush"] as ICanvasBrush);
            //args.DrawingSession.FillGeometry(innerTriangles[0], 400, 400, blackBrush);
        }

        //Draw highlighted polygon
        private void DrawHighlightedPolygon(CanvasAnimatedDrawEventArgs args)
        {
            //Drawing highlight polygon
            if (highlightPoint != null)
            {
                args.DrawingSession.DrawImage(DrawingResources[ResourceKey.HighlightPolygonImage] as CanvasBitmap, highlightPoint.X + globalOffsetX, highlightPoint.Y + globalOffsetY);
            }
        }
        
        //Draw map levels
        //private void DrawLevels(CanvasAnimatedDrawEventArgs args)
        //{
        //    for (int j = 0; j < worldMap.GetSize().Y; j++)
        //    {
        //        //Two cycles for proper order
        //        for (int i = 1; i < worldMap.GetSize().X; i += 2)
        //        {
        //            var item = worldMap[i, j];
        //            args.DrawingSession.FillGeometry(hex, new System.Numerics.Vector2(item.DrawPoint.X, item.DrawPoint.Y), brushes[item.WorldLevel]);
        //        }

        //        for (int i = 0; i < worldMap.GetSize().X; i += 2)
        //        {
        //            var item = worldMap[i, j];
        //            args.DrawingSession.FillGeometry(hex, new System.Numerics.Vector2(item.DrawPoint.X, item.DrawPoint.Y), brushes[item.WorldLevel]);
        //        }
        //    }
        //}

        //Simple polygons drawing
    }
}
