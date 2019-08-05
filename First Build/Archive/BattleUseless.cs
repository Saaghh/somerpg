using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Build.Model
{
    public class BattleUseless
    {

        public BattleUseless()
        {
            teams.Add(new List<CharacterBase>());
            teams.Add(new List<CharacterBase>());

            teams[0].Add(new CharacterBase());
            teams[1].Add(new CharacterBase());
            teams[1].Add(new CharacterBase());
            teams[1].Add(new CharacterBase());

            IniBattle();
        }

        public BattleUseless(List<List<CharacterBase>> characters, bool isPlayed)
        {
            teams = characters;
            IniBattle();
        }

        public readonly string historicalName = "Test Battle";

        public int turnNumber = 0;

        public List<List<CharacterBase>> teams = new List<List<CharacterBase>>();

        public Queue<CharacterBase> turnOrder = new Queue<CharacterBase>();

        public bool isBattlePlayed = true;

        void IniBattle()
        {
            FormTestQueue();
        }

        public CharacterBase StartTurn()
        {
            return turnOrder.Peek();
        }

        public void MakeTurn(TurnCommand userCommand)
        {
            CharacterBase actor = turnOrder.Peek();
            TurnCommand turnCommand;

            //if (actor.controlled)
            //{
            //    turnCommand = userCommand;
            //}
            //else
            //{
            //    turnCommand = new TurnCommand();
            //}

            //switch (turnCommand.stringCommand)
            //{
            //    case ATTACK:
            //        actor.Attack(turnCommand.target);
            //        break;
            //    case SKIP:
            //        Console.WriteLine(actor.name + "has skipped.");
            //        break;
            //}
        }

        public void EndTurn()
        {
            turnOrder.Dequeue();
        }

        void FormTestQueue()
        {
            turnOrder.Clear();

            //Добавляем персонажа игрока
            turnOrder.Enqueue(teams[0][0]);

            //Добавляем персонажей команды противника
            foreach (CharacterBase c in teams[0])
            {
                turnOrder.Enqueue(c);
            }
        }
        public class TurnCommand
        {
            public readonly string stringCommand;
            public readonly CharacterBase target;
        }
    }

}
