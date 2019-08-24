using System;
using System.Collections.Generic;

namespace First_Build
{
    public class AttackAction : Action
    { 

        AttackParams attackParams;

        public AttackAction(AttackParams attackParams) : base()
        {
            this.attackParams = attackParams;
        }

        public override bool IsAvaliable
        {
            get
            {
                return base.IsAvaliable && (actor != null) && (actor is Character);
            }
        }

        public override void AddTarget(object target)
        {
            if (!(target is Character)) { throw new ArgumentException("Wrong target type. Expected - Character"); }
            base.AddTarget(target);
        }

        public override bool Do()
        {
            base.Do();
            actor.Attack(target as Character, attackParams);
            return actor.HasActionPoints;
        }

        public override List<string> Description
        {
            get
            {
                var s = base.Description;

                s.Add($"Energy: {attackParams.E.ToString()}");
                s.Add($"Penetrations: {attackParams.EperSquare.ToString()}");

                return s;
            }
        }
    }



}
