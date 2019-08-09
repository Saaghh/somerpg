using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using First_Build.Model;
using System.Windows;
using First_Build.View;
using First_Build.Model.BattleMap;
using First_Build.Model.Characters;
using System.Windows.Controls;
using First_Build.Model.Tiles;

namespace First_Build.Controller
{
    public abstract class BattleVisualModel
    {
        static protected Random r = new Random();

        protected BattleMapVisual battleMap;

        protected CharacterVisual[] playerParty;
        protected CharacterVisual[] opponentParty;

        protected BattleWindow window;

        protected readonly (int width, int height) battlefieldSize = (40, 40);

        public Queue<CharacterVisual> turnOrder = new Queue<CharacterVisual>();
    }

    public class BattleVisualController : BattleVisualModel
    {

        public BattleVisualController(BattleWindow battleWindow)
        {
            window = battleWindow;
            battleMap = new BattleMapVisual(battlefieldSize, window.imageCanvas);

            playerParty = new CharacterVisual[5];
            opponentParty = new CharacterVisual[5];

            IniControlCanvas();

            IniParty(playerParty, 0);
            IniParty(opponentParty, 1);

            FormQueue();
        }

        void IniControlCanvas()
        {
            for (int i = 0; i < battlefieldSize.width; i++)
            {
                for (int j = 0; j < battlefieldSize.height; j++)
                {
                    var (x, y) = HexMapMath.GetHexCoordinate(i, j);

                    var control = new BattleMapControl((i, j));

                    control.MouseUp += ClickEventHandler;

                    Canvas.SetTop(control, y);
                    Canvas.SetLeft(control, x);

                    window.controlCanvas.Children.Add(control);
                }
            }
        }

        void IniParty(CharacterVisual[] characters, int number)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                var (x, y) = HexMapMath.GetCharacterStarterPosition(battlefieldSize, i, number);
                characters[i] = new CharacterVisual("Resources/TestCharacter.png", (TileVisual)battleMap[x, y]);
                var t = battleMap[x, y] as TileController;
                t.Enter(characters[i]);
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

            if (battleMap[s.coord.x, s.coord.y].character != null)
            {
                var target = battleMap[s.coord.x, s.coord.y].character;
                var attacker = turnOrder.Dequeue();
                attacker.Act(ActionCommands.ATTACK, target);
            }
            else
            {
                var target = battleMap[s.coord.x, s.coord.y] as TileController;
                var actor = turnOrder.Dequeue();
                actor.Act(ActionCommands.MOVE, target);
            }

            AutoAttack();
        }

        void AutoAttack()
        {
            CheckQueueForEmptyness();

            if (opponentParty.Contains(turnOrder.Peek()))
            {
                var attacker = turnOrder.Dequeue();

                attacker.Act(ActionCommands.ATTACK, playerParty[r.Next(playerParty.Length)]);

                AutoAttack();
            }
        }

        void CheckQueueForEmptyness()
        {
            if (turnOrder.Count == 0) { FormQueue(); }
        }
    }
}
