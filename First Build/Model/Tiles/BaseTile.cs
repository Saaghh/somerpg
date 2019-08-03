using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using Image = System.Windows.Controls.Image;
using First_Build.View;

namespace First_Build.Model
{
    public class BaseTile
    {
        private string tileTypeName = "Base Tile";

        static int amount = 0;

        public readonly int id = amount++;
        public readonly (int x, int y) coord;

        public CharacterController containedCharacter;

        public string TileTypeName { get => tileTypeName; protected set => tileTypeName = value; }

        public BaseTile((int x, int y) coordiats)
        {
            coord = coordiats;
        }

        
    }

}
