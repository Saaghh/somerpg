using First_Build.Model.Tiles;
using First_Build.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace First_Build.Model.Characters
{
    public class CharacterVisual : CharacterController
    {
        protected BitmapImage sprite;

        public CharacterVisual(string spriteUriEnd, TileVisual position) : base(position)
        {
            sprite = new BitmapImage(new Uri(Properties.Resources.UriBase + spriteUriEnd));

            RefreshVisual();
        }

        public virtual void RefreshVisual()
        {
            position.characterControl.image.Source = sprite;
            position.characterControl.textBlock.Text = name + ": " + health;
        }

        protected virtual void RemoveVisual()
        {
            position.characterControl.image.Source = null;
            position.characterControl.textBlock.Text = "";
        }

        protected override void Move(TileController target)
        {
            RemoveVisual();
            position = target;
            base.Move(target);
            RefreshVisual();
        }
    }
}
