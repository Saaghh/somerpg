using First_Build.Controls.BattleControls;
using First_Build.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace First_Build
{
    public class Path
    {
        public List<Tile> tiles = new List<Tile>();
        BattleWindow window;

        public int Length => tiles.Count;

        public bool IsEmpty
        {
            get { if (tiles.Count == 0 || tiles == null) { return true; } else { return false; } }
        }

        public Path(BattleTile start, BattleTile target, HexMap map, BattleWindow window)
        {
            this.window = window;

            tiles = map.GetTilesFromPoints(AStar.FindPath(map, start.coord, target.coord, false));

            window.DrawPath(this);
        }

        public void ClearPath()
        {
            tiles.Clear();

            window.HidePath();
        }

        public Tile this[int x]
        {
            get
            {
                return tiles[x];
            }
        }
    }
}
