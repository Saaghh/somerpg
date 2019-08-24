using System.Collections.Generic;

namespace First_Build
{
    public abstract class EquipmentObject
    {
        private string name;
        public Character owner;
        public string Name { get => name; protected set => name = value; }
        public bool HasOwner
        {
            get
            {
                if (owner != null) { return true; } else { return false; }
            }
        }
        public abstract List<Action> avaliableActions { get; }
    }
}
