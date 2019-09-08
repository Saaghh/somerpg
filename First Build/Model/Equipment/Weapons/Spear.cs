using System;
using System.Collections.Generic;

namespace First_Build
{
    public class Spear : Weapon
    {

        public float bladeS = 8f;
        public float pointS = 2f;

        protected static AttackType SpearSlash = new AttackType("SpearSlashAttack");
        protected static AttackType SpearThrust = new AttackType("SpearThrustAttack");

        public override List<Action> avaliableActions
        {
            get
            {
                var s = new List<Action>();

                
                s.Add(new AttackAction(GetAttack(SpearSlash))
                {
                    header = "Рубящий удар копьем",
                    text = "Наносит рубящий удар лезвием копья"

                });
                s.Add(new AttackAction(GetAttack(SpearThrust))
                {
                    header = "Колющий удар копьем",
                    text = "Наносит колющий удар концом копья"
                });
                return s;
            }
        }

        public Spear()
        {
            range.max = 2;
            Name = "Spear";
        }

        public override AttackParams GetAttack(AttackType attackType)
        {
            if (attackType == SpearSlash)
            {
                return new AttackParams(weight, owner.swiftness * 1.2f, bladeS, 4);
            }
            else if (attackType == SpearThrust)
            {
                return new AttackParams(Convert.ToSingle(weight * 0.8), owner.swiftness * 0.8f, pointS, 4.5f);
            }
            throw new ArgumentException("Unexpected AttackType");
        }
    }

}
