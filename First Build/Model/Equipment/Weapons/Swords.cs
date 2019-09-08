using System;
using System.Collections.Generic;

namespace First_Build
{
    public class Sword : Weapon
    {
        public float bladeS = 13f;
        public float pointS = 3.5f;



        protected static AttackType SwordTopDown
        {
            get
            {
                return new AttackType("SwordTopDownAttack");
            }
        }
        protected static AttackType SwordThrust
        {
            get
            {
                return new AttackType("SwordThrustAttack");
            }
        }

        public Sword()
        {
            weight *= 6;
            Name = "Sword";
            avaliableAttackTypes.Add(SwordTopDown);
            avaliableAttackTypes.Add(SwordThrust);
        }

        public override List<Action> avaliableActions
        {
            get
            {
                var s = new List<Action>();
                s.Add(new AttackAction(GetAttack(SwordTopDown))
                {
                    header = "Рубящий удар мечом",
                    text = "Наносит рубящий удар мечом"
                });
                s.Add(new AttackAction(GetAttack(SwordThrust))
                {
                    header = "Пыр мечом",
                    text = "Наносит колющий удар концом меча"
                });
                return s;
            }
        }

        public override AttackParams GetAttack(AttackType attackType)
        {

            if (attackType == SwordTopDown)
            {
                return new AttackParams
                    (
                        weight, 
                        owner.swiftness * 2,
                        bladeS,
                        3.5f
                    );
            }
            else if (attackType == SwordThrust)
            {
                return new AttackParams
                    (
                        Convert.ToSingle(weight * 0.8),
                        owner.swiftness,
                        pointS,
                        5
                    );
            }
            throw new ArgumentException("Unexpected AttackType");
        }
    }

    public class LongSword : Sword
    {
        public LongSword() : base()
        {
            weight = Convert.ToSingle(weight * 1.2);
            range.max = 2;
            Name = "LongSword";
        }
    }

}

