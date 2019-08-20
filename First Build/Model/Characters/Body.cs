using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build
{
    public class BodyPart
    {
        public string name;
        public float area;
        public int maxHealth;
        public int health;

        public bool IsActive
        {
            get
            {
                if (health > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void TakeDamage(float damage)
        {
            health -= (int)damage;
        }

        public override string ToString()
        {
            return name +" - "+ health + "/" + maxHealth;
        }
    }
    public class Body
    {
        public bool isDead = false;
        public float absorbationK = 4;
        protected List<BodyPart> bodyParts = new List<BodyPart>();

        public virtual int Health
        {
            get
            {
                int sum = 0;
                foreach (var item in bodyParts)
                {
                    if (item.IsActive)
                    {
                        sum += item.health;
                    }
                }
                return sum;
            }
        }
        public virtual float Area
        {
            get
            {
                float sum = 0;
                foreach (var item in bodyParts)
                {
                    sum += item.area;
                }
                return sum;
            }
        }
        public virtual int MaxHealth
        {
            get
            {
                int sum = 0;
                foreach (var item in bodyParts)
                {
                    sum += item.maxHealth;
                }
                return sum;
            }
        }
        public virtual bool IsAlive
        {
            get
            {
                if (isDead)
                {
                    return false;
                }
                foreach (var item in bodyParts)
                {
                    if (!item.IsActive) { return false; }
                }
                return true;
            }
        }
        public virtual List<string> Status
        {
            get
            {
                var result = new List<string>();

                foreach (var item in bodyParts)
                {
                    result.Add(item.ToString());
                }

                return result;
            }
        }

        public void TakeEnergyHit(AttackParams attack)
        {
            if (attack.E > Health)
            {
                isDead = true;
            }
            else
            {
                float damagePerBodyPart = attack.E / bodyParts.Count;
                foreach (var item in bodyParts)
                {
                    item.TakeDamage(damagePerBodyPart / absorbationK);
                }
            }
        }
        public void TakeAttack(AttackParams attack)
        {
            if (attack.EperSquare >= (Health / Area))
            {
                isDead = true;
            }
            else
            {
                AddInjury();
            }
            TakeEnergyHit(attack);
        }
        private void AddInjury()
        {

        }
    }

    public class HumanBody : Body
    {
        public HumanBody()
        {
            bodyParts.Add(new BodyPart
            {
                name = "Head", 
                health = 40,
                maxHealth = 40,
            });
            bodyParts.Add(new BodyPart
            {
                name = "Torso",
                health = 120,
                maxHealth = 120,

            });
            bodyParts.Add(new BodyPart
            {
                name = "Stomach",
                health = 80,
                maxHealth = 80,

            });
            bodyParts.Add(new BodyPart
            {
                name = "LeftLeg",
                health = 50,
                maxHealth = 50,

            });
            bodyParts.Add(new BodyPart
            {
                name = "RightLeg",
                health = 50,
                maxHealth = 50,

            });
            bodyParts.Add(new BodyPart
            {
                name = "RightLeg",
                health = 50,
                maxHealth = 50,
            });
            bodyParts.Add(new BodyPart
            {
                name = "RightLeg",
                health = 50,
                maxHealth = 50,
            });
        }

    }

}
