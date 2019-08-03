using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace First_Build.Model
{
    public class CharacterController : Character
    {
        const string UriBase = "pack://application:,,,/First Build;component/";
        protected string UriEnd = "Resources/TestCharacter.png";
        public Uri spriteUri;

        public CharacterController() : base()
        {
            spriteUri = new Uri(UriBase + UriEnd);
        }

        public void DrawCharacter(CharacterControl characterControl)
        {
            characterControl.image.Source = new BitmapImage(spriteUri);
        }

        public Uri GetSpriteUri()
        {
            return spriteUri;
        }
    }
}
