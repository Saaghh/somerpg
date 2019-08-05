using First_Build.Model.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build.Model.Characters
{
    public class CharacterController : CharacterModel
    {
        public CharacterController(TileModel position) : base(position) { }

        public void Act(string command, object target)
        {
            switch (command)
            {
                case ActionCommands.ATTACK:
                    if(!(target is CharacterController)) { throw new ArgumentException("Only character can be attacked", "target"); }
                    Attack(target as CharacterController);
                    break;
                case ActionCommands.MOVE:
                    if (!(target is TileModel)) { throw new ArgumentException("Can move only to tiles", "target"); }
                    Move(target as TileController);
                    break;
                default:
                    throw new ArgumentException("Unknown command", "command");
            }
        }

        protected virtual void Move(TileController target)
        {
            var p = position as TileController;
            if (MovePossible(target))
            {
                p.Leave();
                target.Enter(this);
            }
        }

        protected virtual bool MovePossible(TileController target)
        {
            var cost = target.GetEnterCost();

            if (cost > actionPoints) { return false; }
            return true;
        }

        protected virtual void Attack(CharacterController target)
        {
            Console.WriteLine(name + " attacks " + target.name);
            target.GetAttacked(this);
        }

        protected virtual void GetAttacked(CharacterController actor)
        {
            if (clothing <= actor.weapon)
            {
                health -= (actor.weapon - clothing);
            }
            Console.WriteLine(name + " was attacked by " + actor.name);
        }
    }

    public static class ActionCommands
    {
        public const string ATTACK = "Attack";
        public const string MOVE = "Move";
    }

}
