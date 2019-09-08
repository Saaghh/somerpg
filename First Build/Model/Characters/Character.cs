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

        public float swiftness  = 10f;
        public int maxAP        = 7;
        public int ap;

        public CharacterEquipment equipment;
        public Body body = new HumanBody();
        public BattleTile battlePosition;
        public System.Drawing.Point position;
        public BitmapSource textureSource;
        public Bitmap texture   = Properties.Resources.TestCharacter;

        public event EventHandler<MoveEventArgs> Moved;
        public event EventHandler<AttackedEventArgs> GotAttacked;
        public event EventHandler<EventArgs> RoundStarted;
        public event EventHandler<EventArgs> Died;

        public bool IsAlive
        {
            get
            {
                return body.IsAlive;
            }
        }
        public float MaxHealth
        {
            get
            {
                return body.MaxHealth;
            }
        }
        public float Health
        {
            get
            {
                return body.Health;
            }
        }
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

        public List<Action> AvaliableActions
        {
            get
            {
                var s = equipment.avaliableActions;

                foreach (var item in s)
                {
                    item.AddActor(this);
                }

                return s;
            }
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
        public virtual void EngageBattle(BattleTile position)
        {
            GetReadyForBattle();
            this.battlePosition = position;
            this.battlePosition.Enter(this);
        }
        public virtual void Attack(Character target, AttackParams attackParams)
        {
            attackParams.attacker = this;

            target.GetAttacked(attackParams);
        }
        public virtual void GetAttacked(AttackParams attack)
        {
            equipment.Protect(attack);

            body.TakeAttack(attack);

            GotAttacked(this, new AttackedEventArgs(attack.attacker));

            CheckForDeath();
        }
        public virtual void Move(Path path)
        {
            var p = battlePosition;

            for (int i = 1; i < path.Length; i++)
            {
                if (ap >= ((BattleTile)path[i]).GetEnterCost())
                {
                    battlePosition.Leave();
                    ((BattleTile)path[i]).Enter(this);
                    battlePosition = (BattleTile)path[i];
                    ap -= ((BattleTile)path[i]).GetEnterCost();
                    Moved(this, new MoveEventArgs((BattleTile)path[i]));
                }
            }
        }
        public override string ToString()
        {
            return name;
        }
        void CheckForDeath()
        {
            if (!IsAlive)
            {
                Died(this, new EventArgs());
            }
        }


        public BitmapSource PrepareTexture()
        {
            textureSource = Imaging.CreateBitmapSourceFromHBitmap(texture.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            return textureSource;
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
            public BattleTile target;

            public MoveEventArgs(BattleTile tile)
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
    }
}
