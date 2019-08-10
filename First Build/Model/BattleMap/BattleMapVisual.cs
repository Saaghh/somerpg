using First_Build.BetterModel;
using First_Build.Controller;
using First_Build.Model.Tiles;
using First_Build.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace First_Build.Model.BattleMap
{
    public class BattleMapVisual : BattleMapController
    {
        static Random r = new Random();
        Canvas imageCanvas;

        public BattleMapVisual((int x, int y) size, Canvas images) : base(size)
        {
            imageCanvas = images;

            GenerateMap();
        }

        protected void GenerateMap()
        {
            for (int i = 0; i < size.width; i++)
            {
                for (int j = 0; j < size.height; j++)
                {
                    //Реализуем view карты
                    var image = new BattleMapImage();
                    var character = new CharacterControl(new Character());

                    var (x, y) = HexMapMath.GetHexCoordinate(i, j);

                    Canvas.SetTop(image, y);
                    Canvas.SetLeft(image, x);

                    Canvas.SetTop(character, y);
                    Canvas.SetLeft(character, x);

                    imageCanvas.Children.Add(image);
                    imageCanvas.Children.Add(character);

                    //Реализуем модель карты
                    TileVisual btc;
                    switch (r.Next(2))
                    {
                        case 1:
                            btc = new TileVisual((i, j), Properties.Resources.BasicTerrainJson, image, "Resources/WaterTile.png", character);
                            break;
                        case 0:
                            btc = new TileVisual((i, j), Properties.Resources.BasicTerrainJson, image, "Resources/ForestTile.png", character);
                            break;
                        default:
                            btc = new TileVisual((i, j), Properties.Resources.BasicTerrainJson, image, "Resources/ErrorTile.png", character);
                            break;
                    }
                    tiles[i, j] = btc;
                }
            }
        }

        public void RefreshMapVisual()
        {
            foreach (TileVisual item in tiles)
            {
                item.RefreshVisual();
            }
        }
    }
}
