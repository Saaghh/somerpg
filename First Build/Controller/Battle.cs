using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using First_Build.Model;
using static First_Build.Model.CharacterBase.ActionCommands;
using System.Windows;
using First_Build.View;

namespace First_Build.Controller
{
    public class Battle
    {

        public Battle()
        {
            teams.Add(new List<CharacterBase>());
            teams.Add(new List<CharacterBase>());

            teams[0].Add(new CharacterBase());
            teams[1].Add(new CharacterBase());
            teams[1].Add(new CharacterBase());
            teams[1].Add(new CharacterBase());

            IniBattle();
        }

        public Battle(List<List<CharacterBase>> characters, bool isPlayed)
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

    public class BattleController
    {
        static Random r = new Random();

        BattleMap battleMap;

        CharacterController[] playerParty;
        CharacterController[] opponentParty;

        BattleWindow battleWindow;

        readonly (int w, int h) battlefieldSize = (12, 12);

        public Queue<CharacterController> turnOrder = new Queue<CharacterController>();

        public BattleController(BattleWindow window)
        {

            battleWindow = window;
            battleMap = new BattleMap(battlefieldSize, battleWindow);

            playerParty = new CharacterController[3];
            opponentParty = new CharacterController[5];

            IniParty(playerParty, 0);
            IniParty(opponentParty, 1);

            FormQueue();

            foreach (BattleMapControl item in battleWindow.controlCanvas.Children)
            {
                item.MouseUp += ClickEventHandler;
            }
        }

        void FormQueue()
        {
            foreach (var item in playerParty)
            {
                turnOrder.Enqueue(item);
            }
            foreach (var item in opponentParty)
            {
                turnOrder.Enqueue(item);
            }
        }

        private void ClickEventHandler(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var s = sender as BattleMapControl;

            var target = battleMap[s.coord.x, s.coord.y].containedCharacter;

            CharacterController attacker;

            if (target != null)
            {
                attacker = turnOrder.Dequeue();

                attacker.Attack(target);

                Console.WriteLine(attacker.name + " has attacked " + target.name);
            }

            AutoAttack();
        }

        void AutoAttack()
        {
            CheckQueueForEmptyness();

            if (opponentParty.Contains(turnOrder.Peek()))
            {
                var attacker = turnOrder.Dequeue();

                attacker.Attack(playerParty[r.Next(playerParty.Length)]);

                AutoAttack();
            }
        }

        void CheckQueueForEmptyness()
        {
            if (turnOrder.Count == 0) { FormQueue(); }
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
