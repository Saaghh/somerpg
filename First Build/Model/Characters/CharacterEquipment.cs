using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build
{
    public class CharacterEquipment
    {
        public List<WearableEquiment> bodyEquipment = new List<WearableEquiment>();

        public Weapon rightHand;


        public List<Action> avaliableActions
        {
            get
            {
                var s = new List<Action>();

                if (rightHand != null)
                {
                    foreach (var item in rightHand.avaliableActions)
                    {
                        s.Add(item);
                    }
                }
                //foreach (var item in bodyEquipment)
                //{
                //    foreach (var item2 in item.avaliableActions)
                //    {

                //    }
                //}

                return s;
            }
        }

        public CharacterEquipment(Character owner)
        {
            bodyEquipment.Add(new ChainMail
            {
                owner = owner
            });
            rightHand = new Sword
            {
                owner = owner
            };
        }
        public AttackParams Protect(AttackParams attack)
        {
            bool isPenetrated = true;
            foreach (var item in bodyEquipment)
            {
                if (!attack.IsActive) continue; 
                if (isPenetrated)
                {
                    isPenetrated = item.TryPenetrate(attack);
                }
                else
                {
                    item.AbsorbEnergy(attack);
                }
            }
            return attack;
        }
        public List<string> Stats
        {
            get
            {
                var list = new List<string>();

                list.Add(rightHand.ToString());

                foreach (var item in bodyEquipment)
                {
                    list.Add(item.ToString());
                }
                return list;
            }
        }
    }
}
