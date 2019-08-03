using First_Build.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build.Model
{
    class ForestTile : BaseTileController
    {
        public ForestTile((int x, int y) coordiats, BattleMapImage image, BattleMapControl control) : base (coordiats, image, control)
        {
            
        }

        protected override void MouseClickEventHandler(object sender, EventArgs args)
        {
            Console.WriteLine("You have clicked on a FOREST in " + coord);
        }

        protected override void Ini()
        {
            TileTypeName = "Forest Tile";

            UriEnd = "Resources/ForestTile.png";
        }
    }

    class WaterTile : BaseTileController
    {
        public WaterTile((int x, int y) coordiats, BattleMapImage image, BattleMapControl control) : base(coordiats, image, control)
        {
            
        }

        protected override void MouseClickEventHandler(object sender, EventArgs args)
        {
            Console.WriteLine("You have clicked on a WATER in " + coord);
        }
        protected override void Ini()
        {
            TileTypeName = "Water Tile";

            UriEnd = "Resources/WaterTile.png";
        }
    }
}
