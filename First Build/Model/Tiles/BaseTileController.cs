using First_Build.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace First_Build.Model
{
    public class BaseTileController : BaseTile
    {
        const string UriBase = "pack://application:,,,/First Build;component/";
        protected string UriEnd = "Resources/ErrorTile.png";
        public Uri spriteUri;

        BattleMapImage relatedImage;
        BattleMapControl relatedControl;

        public BaseTileController((int x, int y) coordiats, BattleMapImage image, BattleMapControl control) : base(coordiats)
        {
            Ini();
            spriteUri = new Uri(UriBase + UriEnd);
            relatedImage = image;
            relatedControl = control;

            relatedImage.image.Source = new BitmapImage(spriteUri);
            relatedControl.polygon.MouseUp += MouseClickEventHandler;
            relatedImage.text.Text = (coord.ToString());
        }

        protected virtual void Ini()
        {

        }

        protected virtual void MouseClickEventHandler(object sender, EventArgs args)
        {
            Console.WriteLine("Handeled by: " + coord);
        }
    }

}
