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

namespace First_Build
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

        public bool isAlive     = true;

        public Tile position;

        public event EventHandler<MoveEventArgs> Moved;
        public event EventHandler<AttackedEventArgs> GotAttacked;
        public event EventHandler<EventArgs> RoundStarter;
        public event EventHandler<EventArgs> Died;

        public Bitmap texture   = Properties.Resources.TestCharacter;
        public BitmapSource textureSource;

        public Character()
        {
            textureSource = Imaging.CreateBitmapSourceFromHBitmap(texture.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public BitmapSource PrepareTexture()
        {
            textureSource = Imaging.CreateBitmapSourceFromHBitmap(texture.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            return textureSource;
        }

        protected virtual void GetReadyForBattle()
        {
            ap = maxAP;
        }

        public virtual void GetReadyForNewRound()
        {
            ap = maxAP;

            RoundStarter(this, new EventArgs());
        }

        public virtual void EngageBattle(Tile position)
        {
            GetReadyForBattle();
            this.position = position;
            this.position.Enter(this);
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
            target.GetAttacked(this);
        }

        public virtual void GetAttacked(Character attacker)
        {
            if (armor <= attacker.weapon)
            {
                health -= (attacker.weapon - armor);
            }
            if (health <= 0)
            {
                isAlive = false;
                Died(this, new EventArgs());
            }
            GotAttacked(this, new AttackedEventArgs(attacker));
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

        public static Character Warrior
        {
            get
            {
                return new Character
                {
                    armor = 20,
                    maxHealth = 300,
                    health = 300,
                    name = "Warrior #" + amount,
                    texture = Properties.Resources.Warrior
                };
            }
        }
        public static Character Zombie
        {
            get
            {
                return new Character
                {
                    armor = 0,
                    weapon = 30,
                    maxHealth = 500,
                    health = 500,
                    name = "Zombie #" + amount,
                    texture = Properties.Resources.Zombie
                };
            }
        }
        public static Character Test
        {
            get
            {
                return new Character
                {
                    armor = 0,
                    weapon = 1,
                    maxHealth = 3,
                    health = 1,
                    maxAP = 1,
                    name = "Test #" + amount,
                    texture = Properties.Resources.TestCharacter
                };
            }
        }

    }
}
