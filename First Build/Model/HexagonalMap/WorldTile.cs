using System.Drawing;
using Point = System.Drawing.Point;

namespace First_Build
{
    public class WorldTile : Tile
    {
        private TileContent content;
        public WorldTile(Point coord) : base(coord)
        {
        }

        public TileContent Content { get => content; set => content = value; }

        public override Bitmap Texture
        {
            get
            {
                if (content != null)
                {
                    var img = base.Texture; 
                    Graphics g = Graphics.FromImage(img);
                    g.DrawImage(content.texture, new Point(0, 0));
                    return img;
                }
                else
                {
                    return base.Texture;
                }
            }
        }
    }
}
