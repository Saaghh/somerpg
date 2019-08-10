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

        public event EventHandler<MoveEventArgs> Moved;
        public event EventHandler<AttackedEventArgs> Attacked;

        public Bitmap texture   = Properties.Resources.TestCharacter;
        public BitmapSource textureSource;
        public Character()
        {
            textureSource = Imaging.CreateBitmapSourceFromHBitmap(texture.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        protected virtual void GetReadyForBattle()
        {
            ap = maxAP;
        }

        public virtual void EngageBattle(Tile position)
        {
            GetReadyForBattle();
            //if (!TryToMove(position)) { throw new ArgumentException("Unexpected position"); };
            this.position = position;
            Console.WriteLine(name + "has engaged battle");
        }

        public virtual bool TryToMove(Tile target)
        {
            if (target.character == null & ap >= target.GetEnterCost())
            {
                position.Leave();
                target.Enter(this);
                position = target;
                ap -= target.GetEnterCost();
                Moved(this, new MoveEventArgs(target));
                return true;
            }
            return false;
        }

        public virtual void Attack(Character target)
        {
            Console.WriteLine(name + " attacks " + target.name);
            target.GetAttacked(this);
        }

        public virtual void GetAttacked(Character attacker)
        {
            if (armor <= attacker.weapon)
            {
                health -= (attacker.weapon - armor);
            }
            Attacked(this, new AttackedEventArgs(attacker));
            Console.WriteLine(name + " was attacked by " + attacker.name);
        }


        public class AttackedEventArgs : EventArgs
        {
            public Character attacker;
            public AttackedEventArgs(Character character)
            {
                attacker = character;
            }
        }

        public class MoveEventArgs : EventArgs
        {
            public Tile target;

            public MoveEventArgs(Tile tile)
            {
                target = tile;
            }
        }
    }
}
