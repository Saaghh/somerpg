using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build
{

    public abstract class Weapon : EquipmentObject
    {
        public (int min, int max) range = (1, 1);
        public float weight = 1f;
        public List<AttackType> avaliableAttackTypes = new List<AttackType>();

        public abstract AttackParams GetAttack(AttackType attackType);
        public void SetOwner(Character owner)
        {
            this.owner = owner;
        }

        public override string ToString()
        {
            return Name + " - " + weight;
        }
    }

}
