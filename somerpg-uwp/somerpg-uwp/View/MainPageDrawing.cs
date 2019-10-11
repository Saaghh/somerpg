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
        //Canvas refresh cycle
        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            DrawLevels(args);
            //DrawStandart(args);
            DrawHighLightedPolygon(args);
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
        
        //Standart canvas tiles draw
        private void DrawStandart(CanvasAnimatedDrawEventArgs args)
        {
            //Drawing tiles
            for (int j = 0; j < worldMap.GetSize().Y; j++)
            {
                //Two cycles for proper order
                for (int i = 1; i < worldMap.GetSize().X; i += 2)
                {
                    var item = worldMap[i, j];
                    
                    args.DrawingSession.DrawImage(images[item.Terrain.Name], item.DrawPoint.X, item.DrawPoint.Y);
                }

                for (int i = 0; i < worldMap.GetSize().X; i += 2)
                {
                    var item = worldMap[i, j];

                    args.DrawingSession.DrawImage(images[item.Terrain.Name], item.DrawPoint.X, item.DrawPoint.Y);
                }
            }
        }

        //Draw highlighted polygon
        private void DrawHighLightedPolygon(CanvasAnimatedDrawEventArgs args)
        {
            //Drawing highlight polygon
            if (highlightPoint != null)
            {
                args.DrawingSession.DrawImage(images["Highlight"], highlightPoint.X, highlightPoint.Y);
            }
        }
    }
}
