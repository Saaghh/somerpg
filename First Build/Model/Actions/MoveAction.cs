using System;

namespace First_Build
{
    //public abstract class Action
    //{
    //    protected Character actor;
    //    protected object target;
    //    protected bool isDone = false;

    //    public virtual bool IsAvaliable
    //    {
    //        get
    //        {
    //            if (isDone) { return false; }
    //            if (actor.ap <= 0) { return false; }
    //            return true;
    //        }
    //    }
    //    public bool IsDone { get => isDone; }

    //    public Action(Character actor, object target)
    //    {
    //        this.actor = actor;
    //        this.target = target;
    //    }

    //    /// <summary>
    //    /// Совершает действие. Возвращает true, если ход не завершен
    //    /// </summary>
    //    /// <returns>Возвращает true, если ход не завершен</returns>
    //    public virtual bool Do()
    //    {
    //        if (!IsAvaliable) { throw new Exception("Action is not avaliable"); }
    //        isDone = true;
    //        return false;
    //    }
    //}

    public class MoveAction : Action
    {
        public MoveAction() : base() { }


        public override void AddTarget(object target)
        {
            if (!(target is Path)) { throw new ArgumentException("Wrong target type. Expected - Path"); }
            base.AddTarget(target);
        }

        public override bool Do()
        {
            base.Do();
            actor.Move(target as Path);
            ((Path)target).ClearPath();
            return actor.HasActionPoints;
        }
    }



}
