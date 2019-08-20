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
        CharacterEquipment equipment;
        Body body = new HumanBody();

        static int amount       = 0;
        public string name      = "Nameless #" + amount++;

        public float maxHealth
        {
            get
            {
                return body.MaxHealth;
            }
        }
        public float health
        {
            get
            {
                return body.Health;
            }
        }

        public float swiftness  = 10f;

        public int maxAP        = 13;
        public int ap;

        public bool isAlive     = true;

        public Tile position;

        public Bitmap texture   = Properties.Resources.TestCharacter;
        public BitmapSource textureSource;

        public event EventHandler<MoveEventArgs> Moved;
        public event EventHandler<AttackedEventArgs> GotAttacked;
        public event EventHandler<EventArgs> RoundStarted;
        public event EventHandler<EventArgs> Died;

        public List<string> Status
        {
            get
            {
                return body.Status;
            }
        }
        public List<string> EquipmentStats
        {
            get
            {
                return equipment.Stats;
            }
        }

        public bool HasActionPoints
        {
            get
            {
                if (ap > 0) { return true; }
                else { return false; }
            }
        }

        public Character()
        {
            textureSource = Imaging.CreateBitmapSourceFromHBitmap(texture.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            equipment = new CharacterEquipment(this);
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

            RoundStarted(this, new EventArgs());
        }

        public virtual void EngageBattle(Tile position)
        {
            GetReadyForBattle();
            this.position = position;
            this.position.Enter(this);
        }

        public virtual bool TryToMove(Path path)
        {
            var p = position;

            for (int i = 1; i < path.Length; i++)
            {
                if (ap >= path[i].GetEnterCost())
                {
                    position.Leave();
                    path[i].Enter(this);
                    position = path[i];
                    ap -= path[i].GetEnterCost();
                    Moved(this, new MoveEventArgs(path[i]));

                }
            }
            if (position == p)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public virtual void Attack(Character target)
        {
            Random r = new Random();
            var aTs = equipment.rightHand.avaliableAttackTypes;
            var aT = aTs[r.Next(aTs.Count)];
            target.GetAttacked(equipment.rightHand.GetAttack(aT));
        }

        public virtual void GetAttacked(AttackParams attack)
        {
            equipment.Protect(attack);

            body.TakeAttack(attack);

            GotAttacked(this, new AttackedEventArgs(attack.attacker));

            CheckForDeath();
        }

        void CheckForDeath()
        {
            if (!body.IsAlive)
            {
                Died(this, new EventArgs());
            }
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
                    maxAP = 1,
                    name = "Test #" + amount,
                    texture = Properties.Resources.TestCharacter
                };
            }
        }

        public override string ToString()
        {
            return name;
        }

    }
}
