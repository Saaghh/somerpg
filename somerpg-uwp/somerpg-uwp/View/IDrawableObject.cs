using Microsoft.Graphics.Canvas.UI.Xaml;

namespace somerpg_uwp
{
    public interface IDrawableObject
    {
        void Draw(CanvasAnimatedDrawEventArgs args, int x, int y);
    }
}
