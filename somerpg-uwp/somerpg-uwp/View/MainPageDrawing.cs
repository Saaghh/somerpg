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

namespace somerpg_uwp
{
    public sealed partial class MainPage : Page
    {
        int globalOffsetX = 0;
        int globalOffsetY = 0;

        //Canvas refresh cycle
        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            if (Settings.DrawStandart) { DrawStandart(args); }
            //if (Settings.DrawLevels) { DrawLevels(args); }
            //if (Settings.DrawInnerTriangles) { DrawInnerTriangles(args); }
            //if (Settings.DrawPolygons) { DrawPolygons(args); }
            //if (Settings.DrawHighlightedPolygon) { DrawHighlightedPolygon(args); }
        }

        //Draw map levels
        private void DrawLevels(CanvasAnimatedDrawEventArgs args)
        {
            for (int j = 0; j < worldMap.GetSize().Y; j++)
            {
                //Two cycles for proper order
                for (int i = 1; i < worldMap.GetSize().X; i += 2)
                {
                    var item = worldMap[i, j];
                    args.DrawingSession.FillGeometry(hex, new System.Numerics.Vector2(item.DrawPoint.X, item.DrawPoint.Y), brushes[item.WorldLevel]);
                }

                for (int i = 0; i < worldMap.GetSize().X; i += 2)
                {
                    var item = worldMap[i, j];
                    args.DrawingSession.FillGeometry(hex, new System.Numerics.Vector2(item.DrawPoint.X, item.DrawPoint.Y), brushes[item.WorldLevel]);
                }
            }
        }

        //Simple polygons drawing
        private void DrawPolygons(CanvasAnimatedDrawEventArgs args)
        {
            foreach (Tile item in worldMap)
            {
                args.DrawingSession.DrawGeometry(hex, new System.Numerics.Vector2(item.DrawPoint.X, item.DrawPoint.Y), brushes[item.WorldLevel]);
            }
        }

        //Draw inner triangles
        private void DrawInnerTriangles(CanvasAnimatedDrawEventArgs args)
        {
            for (int j = 0; j < worldMap.GetSize().Y; j++)
            {
                //Two cycles for proper order
                for (int i = 1; i < worldMap.GetSize().X; i += 2)
                {
                    var item = worldMap[i, j];
                    foreach (var triangle in innerTriangles)
                    {
                        args.DrawingSession.DrawGeometry(triangle, new System.Numerics.Vector2(item.DrawPoint.X + globalOffsetX, item.DrawPoint.Y + globalOffsetY), blackBrush);
                    }
                }

                for (int i = 0; i < worldMap.GetSize().X; i += 2)
                {
                    var item = worldMap[i, j];
                    foreach (var triangle in innerTriangles)
                    {
                        args.DrawingSession.DrawGeometry(triangle, new System.Numerics.Vector2(item.DrawPoint.X + globalOffsetX, item.DrawPoint.Y + globalOffsetY), blackBrush);
                    }
                }
            }
        }
        
        //Standart canvas tiles draw
        private void DrawStandart(CanvasAnimatedDrawEventArgs args)
        {
            int offsetX = globalOffsetX;
            int offsetY = globalOffsetY;

            //Drawing tiles
            for (int j = 0; j < worldMap.GetSize().Y; j++)
            {
                //Two cycles for proper order
                for (int i = 1; i < worldMap.GetSize().X; i += 2)
                {
                    var item = worldMap[i, j];

                    var x = item.DrawPoint.X + offsetX;
                    var y = item.DrawPoint.Y + offsetY;

                    if (x < 2000 && x > -HexagonalMap.HEXPIXELWIDTH && y < 2000 && y > -(HexagonalMap.HEXPIXELHEIGHT + HexagonalMap.HEXPIXELHEIGHTOFFSET))
                    {
                        args.DrawingSession.DrawImage(images[item.Terrain.Name], x, y);
                    }

                    //args.DrawingSession.DrawImage(images[item.Terrain.Name], item.DrawPoint.X + offsetX, item.DrawPoint.Y + offsetY);
                }

                for (int i = 0; i < worldMap.GetSize().X; i += 2)
                {
                    //var item = worldMap[i, j];

                    //args.DrawingSession.DrawImage(images[item.Terrain.Name], item.DrawPoint.X + offsetX, item.DrawPoint.Y + offsetY);

                    var item = worldMap[i, j];

                    var x = item.DrawPoint.X + offsetX;
                    var y = item.DrawPoint.Y + offsetY;

                    if (x < 2000 && x > -HexagonalMap.HEXPIXELWIDTH && y < 2000 && y > -(HexagonalMap.HEXPIXELHEIGHT + HexagonalMap.HEXPIXELHEIGHTOFFSET))
                    {
                        args.DrawingSession.DrawImage(images[item.Terrain.Name], x, y);
                    }
                }
            }
        }

        //Draw highlighted polygon
        private void DrawHighlightedPolygon(CanvasAnimatedDrawEventArgs args)
        {
            //Drawing highlight polygon
            if (highlightPoint != null)
            {
                args.DrawingSession.DrawImage(images["Highlight"], highlightPoint.X, highlightPoint.Y);
            }
        }
    }
}
