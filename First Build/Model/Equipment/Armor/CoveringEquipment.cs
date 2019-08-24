using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build
{
    public abstract class WearableEquiment : EquipmentObject
    {
        protected (float limit, float current, float k) 
            absorbation, //Поглощение энергии в "Е"
            damage, //Урон по броне в энергии на площадь
            penetration; //Пробитие брони в энергии на площадь
        protected float viscosity; //Коэффициент поглощения энегрии при пробитии

        public virtual AttackParams AbsorbEnergy(AttackParams attack)
        {
            var starterE = attack.E;

            attack.GetAbsorbed(absorbation.current);

            absorbation.current -= starterE / absorbation.k;

            return attack;
        }
        public virtual bool TryPenetrate(AttackParams attack)
        {
            if (attack.EperSquare > penetration.current) //Если пробило
            {
                GetPenetrated(attack);
                return true;
            }
            else if (attack.EperSquare > damage.current) //Если не пробило, но нанесло урон
            {
                GetDamaged(attack);
                return false;
            }
            else //Если броня не получает урона
            {
                GetHit(attack);
                return false;
            }
        }

        protected virtual void GetPenetrated(AttackParams attack)
        {
            damage.current -= attack.EperSquare / damage.k;
            penetration.current -= attack.EperSquare / penetration.k;
            attack.GetAbsorbed(attack.E / viscosity);
            AbsorbEnergy(attack);
        }
        protected virtual void GetDamaged(AttackParams attack)
        {
            damage.current -= attack.EperSquare / damage.k;
            penetration.current -= attack.EperSquare / penetration.k;
            AbsorbEnergy(attack);
        }
        protected virtual void GetHit(AttackParams attack)
        {
            AbsorbEnergy(attack);
        }

        public void RepairTo(float percent)
        {
            absorbation.current = percent * absorbation.limit;
            damage.current = percent * damage.limit;
            penetration.current = percent * penetration.limit;
        }

        public override string ToString()
        {
            return Name + " - (" + absorbation.current + ", " + damage.current + ", " + penetration.current + ")";
        }
    }

    public class ChainMail : WearableEquiment
    {
        public ChainMail()
        {
            Name = "ChainMail";
            absorbation = (1000f, 1000f, 3f);
            damage = (150f, 150f, 3f);
            penetration = (200f, 150f, 4f);
            viscosity = 2f;
        }

        public override List<Action> avaliableActions => null;
    }
}
