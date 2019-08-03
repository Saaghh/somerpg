using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace First_Build.Model
{
    public class CharacterController : CharacterBase
    {
        const string UriBase = "pack://application:,,,/First Build;component/";
        protected string UriEnd = "Resources/TestCharacter.png";
        public Uri spriteUri;

        CharacterControl control;

        public CharacterController() : base()
        {
            spriteUri = new Uri(UriBase + UriEnd);
        }

        public void DrawCharacter(CharacterControl characterControl)
        {
            control = characterControl;
            control.image.Source = new BitmapImage(spriteUri);
            control.textBlock.Text = name + ": " + health.ToString();
        }

        public Uri GetSpriteUri()
        {
            return spriteUri;
        }

        public override void Attack(CharacterBase target)
        {
            base.Attack(target);
            control.textBlock.Text = name + ": " + health.ToString();
        }
        public override void GetHit(int damage)
        {
            base.GetHit(damage);
            control.textBlock.Text = name + ": " + health.ToString();
        }
    }
}
