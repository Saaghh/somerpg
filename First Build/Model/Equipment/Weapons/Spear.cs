using System;

namespace First_Build
{
    public class Spear : Weapon
    {

        public float bladeS = 8f;
        public float pointS = 2f;

        protected static AttackType SpearSlash
        {
            get
            {
                return new AttackType("SpearSlashAttack");
            }
        }
        protected static AttackType SpearThrust
        {
            get
            {
                return new AttackType("SpearThrustAttack");
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
                return new AttackParams(weight, owner.swiftness * 1.2f, bladeS);
            }
            else if (attackType == SpearThrust)
            {
                return new AttackParams(Convert.ToSingle(weight * 0.8), owner.swiftness * 0.8f, pointS);
            }
            throw new ArgumentException("Unexpected AttackType");
        }
    }

}
