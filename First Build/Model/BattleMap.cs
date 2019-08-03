using First_Build.Controller;
using First_Build.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace First_Build.Model
{
    public class BattleMap
    {
        public BaseTileController[,] battleTiles;

        public (int width, int height) mapSize;

        Random r = new Random();

        public BattleMap((int width, int height) size, BattleWindow window)
        {
            mapSize = size;
            battleTiles = new BaseTileController[mapSize.width, mapSize.height];
            for (int i = 0; i < mapSize.width; i++)
            {
                for (int j = 0; j < mapSize.height; j++)
                {
                    var control = new BattleMapControl();
                    var image = new BattleMapImage();
                    var (x, y) = HexMap.GetHexCoordinate(i, j);

                    Canvas.SetTop(control, y);
                    Canvas.SetLeft(control, x);

                    Canvas.SetTop(image, y);
                    Canvas.SetLeft(image, x);


                    window.controlCanvas.Children.Add(control);
                    window.imageCanvas.Children.Add(image);

                    BaseTileController btc;
                    switch (r.Next(2))
                    {
                        case 0:
                            btc = new ForestTile((i, j), image, control);
                            break;
                        case 1:
                            btc = new WaterTile((i, j), image, control);
                            break;
                        default:
                            btc = new BaseTileController((i, j), image, control);
                            break;
                    }
                    battleTiles[i, j] = btc;
                }
            }
        }
        void AddChildren(Canvas canvas, Control control, int x, int y)
        {
            var h = control;
            var m = HexMap.GetHexCoordinate(x, y);

            Canvas.SetTop(h, m.y);
            Canvas.SetLeft(h, m.x);

            canvas.Children.Add(h);
        }
    }
}
