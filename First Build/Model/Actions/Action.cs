using System;
using System.Collections.Generic;

namespace First_Build
{
    public abstract class Action 
    {
        protected Character actor;
        protected object target;

        protected bool isDone   = false;
        public string header    = "Empty Header";
        public string text      = "Empty Text";
        public float apCost     = 0;

        public bool IsDone { get => isDone; }
        public virtual bool IsAvaliable
        {
            get
            {
                if (isDone) { return false; }
                if (actor.ap <= apCost) { return false; }
                return true;
            }
        }
        public virtual List<string> Description
        {
            get
            {
                var s = new List<string>();

                s.Add(header);
                s.Add(text);
                s.Add($"Cost: {apCost.ToString()}");

                return s;
            }
        }

        public void AddActor(Character actor)
        {
            this.actor = actor;
        }
        public virtual void AddTarget(object target)
        {
            this.target = target;
        }
        public virtual bool Do()
        {
            if (!IsAvaliable) { throw new Exception("Action is not avaliable"); }
            isDone = true;
            return false;
        }
    }
}
