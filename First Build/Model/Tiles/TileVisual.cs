using First_Build.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace First_Build.Model.Tiles
{
    public class TileVisual : TileController
    {
        protected BitmapImage sprite;
        protected BattleMapImage control;

        public TileVisual((int x, int y) coord, string terrainJson, BattleMapImage tileControl, string spriteUriEnd, CharacterControl characterControl) : base(coord, terrainJson, characterControl)
        {
            control = tileControl;
            sprite = new BitmapImage(new Uri(Properties.Resources.UriBase + spriteUriEnd));

            RefreshVisual();
        }

        public virtual void RefreshVisual()
        {
            control.image.Source = sprite;
            control.text.Text = terrain.type + " in " + position;
        }
    }
}
