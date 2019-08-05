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
        CharacterControl relatedCharacterImage;

        public BaseTileController((int x, int y) coordiats, BattleMapImage image, BattleMapControl control, CharacterControl character) : base(coordiats)
        {
            Ini();
            spriteUri = new Uri(UriBase + UriEnd);
            relatedImage = image;
            relatedControl = control;
            relatedCharacterImage = character;

            relatedImage.image.Source = new BitmapImage(spriteUri);
            relatedControl.polygon.MouseUp += MouseClickEventHandler;
            relatedControl.coord = coord;
            relatedImage.text.Text = (coord.ToString());
        }

        protected virtual void Ini()
        {

        }

        public void PutCharacter(CharacterController1 character)
        {
            containedCharacter = character;
            containedCharacter.DrawCharacter(relatedCharacterImage);
        }

        protected virtual void MouseClickEventHandler(object sender, EventArgs args)
        {
            Console.WriteLine("Handeled by: " + coord);
        }
    }

}
