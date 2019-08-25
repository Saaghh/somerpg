using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build
{
    public class TileContent
    {
        public string type;
        public Bitmap texture;
    }

    public class City : TileContent
    {
        static protected int id = 0;
        public string name;

        public City()
        {
            type = "City";
            name = "ct " + id++;
            texture = Properties.Resources.Village;
        }

        public override string ToString()
        {
            return type + " - " + name;
        }
    }
}
