using First_Build.Controller;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace First_Build.BetterModel
{
    public class Character
    {
        static int amount       = 0;
        public string name      = "Nameless #" + amount++;

        public float maxHealth  = 200;
        public float health     = 200;
        public float weapon     = 17;
        public float armor      = 5;

        public int maxAP        = 13;
        public int ap;

        public Tile position;

        public Bitmap texture;
        public BitmapSource controlTexture;

        public Character()
        {
            texture = Properties.Resources.TestCharacter.Clone() as Bitmap;
        }

        protected virtual void GetReadyForBattle()
        {
            ap = maxAP;
            PrepareTexture();
        }

        public virtual void EngageBattle(Tile position)
        {
            if (!TryToMove(position)) { throw new ArgumentException("Unexpected position"); };
            Console.WriteLine(name + "has engaged battle");
        }

        protected virtual bool TryToMove(Tile target)
        {
            if (target.character == null & ap >= target.GetEnterCost())
            {
                position.Leave();
                target.Enter(this);
                position = target;
                ap -= target.GetEnterCost();
                return true;
            }
            return false;
        }

        protected virtual void Attack(Character target)
        {
            Console.WriteLine(name + " attacks " + target.name);
            target.GetAttacked(this);
        }

        protected virtual void GetAttacked(Character actor)
        {
            if (armor <= actor.weapon)
            {
                health -= (actor.weapon - armor);
            }
            Console.WriteLine(name + " was attacked by " + actor.name);
        }

        public BitmapSource PrepareTexture()
        {
            controlTexture = Imaging.CreateBitmapSourceFromHBitmap(texture.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            return controlTexture;
        }

        private static Bitmap MergeBitmaps(Bitmap bmp1, Bitmap bmp2)
        {
            Bitmap result = new Bitmap(Math.Max(bmp1.Width, bmp2.Width),
                                       Math.Max(bmp1.Height, bmp2.Height));
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp2, System.Drawing.Point.Empty);
                g.DrawImage(bmp1, System.Drawing.Point.Empty);
            }
            return result;
        }

        //public static byte[] ImageToByte2(Image img)
        //{
        //    using (var stream = new MemoryStream())
        //    {
        //        img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        //        return stream.ToArray();
        //    }
        //}

        //public static BitmapSource ConvertBitmap(Bitmap source)
        //{
        //    return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
        //                  source.GetHbitmap(),
        //                  IntPtr.Zero,
        //                  Int32Rect.Empty,
        //                  BitmapSizeOptions.FromEmptyOptions());
        //}

        //public static Bitmap BitmapFromSource(BitmapSource bitmapsource)
        //{
        //    Bitmap bitmap;
        //    using (var outStream = new MemoryStream())
        //    {
        //        BitmapEncoder enc = new BmpBitmapEncoder();
        //        enc.Frames.Add(BitmapFrame.Create(bitmapsource));
        //        enc.Save(outStream);
        //        bitmap = new Bitmap(outStream);
        //    }
        //    return bitmap;
        //}
    }
}
