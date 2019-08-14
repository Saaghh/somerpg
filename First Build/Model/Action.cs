using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build
{
    public abstract class Action
    {
        protected Character actor;
        protected object target;
        protected bool isDone = false;

        public virtual bool IsAvaliable
        {
            get
            {
                if (isDone) { return false; }
                if (actor.ap <= 0) { return false; }
                return true;
            }
        }
        public bool IsDone { get => isDone; }

        public Action(Character actor, object target)
        {
            this.actor = actor;
            this.target = target;
        }

        /// <summary>
        /// Совершает действие. Возвращает true, если ход не завершен
        /// </summary>
        /// <returns>Возвращает true, если ход не завершен</returns>
        public virtual bool Do()
        {
            if (!IsAvaliable) { throw new Exception("Action is not avaliable"); }
            isDone = true;
            return false;
        }
    }

    public class MoveAction : Action
    {
        public MoveAction(Character actor, Path target) : base(actor, target) { }

        public override bool Do()
        {
            base.Do();
            actor.TryToMove(target as Path);
            ((Path)target).ClearPath();
            return actor.HasActionPoints;
        }
    }

    public class AttackAction : Action
    {
        public AttackAction(Character actor, Character target) : base(actor, target) { }

        public override bool Do()
        {
            base.Do();
            actor.Attack(target as Character);
            return actor.HasActionPoints;
        }
    }

    public class EmptyAction : Action
    {
        public override bool IsAvaliable => false;

        public EmptyAction() : base(null, null) { }
    }

    public class EndTurnAction : Action
    {
        public override bool IsAvaliable => true;

        public EndTurnAction() : base(null, null) { }
    }



}
