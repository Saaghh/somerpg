using System;

namespace First_Build
{
    public class BattleMap : HexMap
    {
        public BattleMap() : base() { }

        public override Tile[,] Tiles { get => tiles; set => tiles = value; }

        protected override void GenerateMap()
        {
            Tiles = new BattleTile[MapWidth, MapHeight];

            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {
                    Tiles[i, j] = new BattleTile(new System.Drawing.Point(i, j));
                    if (r.Next(15) == 0)
                    {
                        ((BattleTile)Tiles[i, j]).terrain = Terrain.BattleTerrain.Stone;
                    }
                    else
                    {
                        ((BattleTile)Tiles[i, j]).terrain = Terrain.BattleTerrain.Flat;
                    }
                }
            }
        }
        public static (int x, int y) GetCharacterStarterTilePosition((int w, int h) mapSize, int order, int team)
        {
            (int x, int y) position;

            switch (team)
            {
                case 0:
                    position.x = mapSize.w / 8 * 3;
                    position.y = mapSize.h / 8 * 3 + order;
                    break;
                case 1:
                    position.x = mapSize.w / 8 * 5;
                    position.y = mapSize.h / 8 * 3 + order;
                    break;
                default:
                    throw new Exception("Сражаться могут лишь 2 команды");
            }

            return position;
        }
    }
}

