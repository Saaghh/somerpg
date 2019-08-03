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
                    //Реализуем view карты
                    var control = new BattleMapControl();
                    var image = new BattleMapImage();
                    var character = new CharacterControl();
                    var (x, y) = HexMapMath.GetHexCoordinate(i, j);

                    Canvas.SetTop(control, y);
                    Canvas.SetLeft(control, x);

                    Canvas.SetTop(image, y);
                    Canvas.SetLeft(image, x);

                    Canvas.SetTop(character, y);
                    Canvas.SetLeft(character, x);


                    window.controlCanvas.Children.Add(control);
                    window.imageCanvas.Children.Add(image);
                    window.imageCanvas.Children.Add(character);

                    //Реализуем модель карты
                    BaseTileController btc;
                    switch (r.Next(2))
                    {
                        case 0:
                            btc = new ForestTile((i, j), image, control, character);
                            break;
                        case 1:
                            btc = new WaterTile((i, j), image, control, character);
                            break;
                        default:
                            btc = new BaseTileController((i, j), image, control, character);
                            break;
                    }
                    battleTiles[i, j] = btc;
                }
            }
        }

        public BaseTileController this[int x, int y]
        {
            get { return battleTiles[x, y]; }
        }
    }
}
