using First_Build.Model.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build.Model.BattleMap
{
    public abstract class BattleMapModel
    {
        public (int width, int height) size;

        public TileModel[,] tiles;

        public BattleMapModel((int x, int y) measures)
        {
            size = measures;
            tiles = new TileModel[size.width, size.height];
        }

        public TileModel this[int x, int y]
        {
            get { return tiles[x, y]; }
        }
    }
}
