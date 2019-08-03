using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using First_Build.Model;
using static First_Build.Model.Character.ActionCommands;
using System.Windows;
using First_Build.View;

namespace First_Build.Controller
{
    public class Battle
    {

        public Battle()
        {
            teams.Add(new List<Character>());
            teams.Add(new List<Character>());

            teams[0].Add(new Character());
            teams[1].Add(new Character());
            teams[1].Add(new Character());
            teams[1].Add(new Character());

            IniBattle();
        }

        public Battle(List<List<Character>> characters, bool isPlayed)
        {
            teams = characters;
            IniBattle();
        }

        public readonly string historicalName = "Test Battle";

        public int turnNumber = 0;

        public List<List<Character>> teams = new List<List<Character>>();

        public Queue<Character> turnOrder = new Queue<Character>();

        public bool isBattlePlayed = true;

        void IniBattle()
        {
            FormTestQueue();
        }

        public Character StartTurn()
        {
            return turnOrder.Peek();
        }

        public void MakeTurn(TurnCommand userCommand)
        {
            Character actor = turnOrder.Peek();
            TurnCommand turnCommand;

            if (actor.controlled)
            {
                turnCommand = userCommand;
            }
            else
            {
                turnCommand = new TurnCommand();
            }

            switch (turnCommand.stringCommand)
            {
                case ATTACK:
                    actor.Attack(turnCommand.target);
                    break;
                case SKIP:
                    Console.WriteLine(actor.name + "has skipped.");
                    break;
            }
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
            foreach (Character c in teams[0])
            {
                turnOrder.Enqueue(c);
            }
        }
        public class TurnCommand
        {
            public readonly string stringCommand;
            public readonly Character target;
        }
    }

    public class Battle1
    {
        BattleMap battleMap;

        CharacterController[] playerParty;
        CharacterController[] opponentParty;

        readonly (int w, int h) battlefieldSize = (12, 12);

        public Battle1(BattleWindow window)
        {
            battleMap = new BattleMap(battlefieldSize, window);

            playerParty = new CharacterController[3];
            opponentParty = new CharacterController[5];

            IniParty(playerParty, 0);
            IniParty(opponentParty, 1);
        }

        void IniParty(CharacterController[] characters, int number)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i] = new CharacterController();
            }

            for (int i = 0; i < characters.Length; i++)
            {
                var coord = HexMapMath.GetCharacterStarterPosition(battlefieldSize, i, number);
                battleMap[coord.x, coord.y].PutCharacter(characters[i]);
            }
        }
    }
}
