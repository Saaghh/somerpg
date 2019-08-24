using First_Build.Controls.BattleControls;
using First_Build.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace First_Build
{
    public class Battle
    {
        public static int amount = 0;
        public readonly string id = amount++.ToString();

        public (int width, int height) battleMapSize;

        public BattleMap tiles = new BattleMap();

        public Party playerTeam;
        public Party opponentTeam;

        public Bitmap texture;

        public Queue<Character> turnOrder = new Queue<Character>();

        public event EventHandler<BattleEndEventArgs> BattleEnded;
        public event EventHandler<EventArgs> ActionChanged;
        public event EventHandler<EventArgs> TurnDone;


        Action action = new EmptyAction();

        BattleWindow window;

        BattleTile clickedTile;

        public Battle((int w, int h) size, BattleWindow window)
        {
            this.window = window;
            battleMapSize = size;

            IniTeams();
            GenerateAllCharactedControls(window);

            GenerateHexControls(window.mapContainer);
            DrawAllGraphics(window);

            FillQueue();

        }

        protected void FillQueue()
        {
            foreach (var item in playerTeam)
            {
                if (item.IsAlive)
                {
                    turnOrder.Enqueue(item);
                    item.GetReadyForNewRound();
                }
            }

            foreach (var item in opponentTeam)
            {
                if (item.IsAlive)
                {
                    turnOrder.Enqueue(item);
                    item.GetReadyForNewRound();
                }
            }
        }

        protected void IniTeams()
        {
            playerTeam = new Party();
            opponentTeam = new Party();

            for (int i = 0; i < playerTeam.Count; i++)
            {
                playerTeam[i] = Character.Warrior;
                var (x, y) = BattleMap.GetCharacterStarterTilePosition(battleMapSize, i, 0);
                playerTeam[i].EngageBattle(tiles[x, y] as BattleTile);
            }

            for (int i = 0; i < opponentTeam.Count; i++)
            {
                opponentTeam[i] = Character.Zombie;
                var (x, y) = BattleMap.GetCharacterStarterTilePosition(battleMapSize, i, 1);
                opponentTeam[i].EngageBattle(tiles[x, y] as BattleTile);
            }
        }

        protected void GenerateAllCharactedControls(BattleWindow window)
        {
            GenerateTeamControls(playerTeam, window);
            GenerateTeamControls(opponentTeam, window);
        }

        protected void GenerateTeamControls(Party team, BattleWindow window)
        {
            foreach (var item in team)
            {
                var control = new CharacterControl(item);

                var coord = HexMap.HexToPixel(item.battlePosition.coord);

                Canvas.SetTop(control, coord.Y);
                Canvas.SetLeft(control, coord.X);

                window.mapContainer.Children.Add(control);
            }
        }

        protected void GenerateHexControls(Canvas canvas)
        {
            foreach (BattleTile item in tiles)
            {
                var control = new BattleMapControl(item.coord);

                var point = HexMap.HexToPixel(item.coord);

                Canvas.SetTop(control, point.Y);
                Canvas.SetLeft(control, point.X);

                control.MouseUp += OnTileClick;

                canvas.Children.Add(control);
            }
        }

        private void OnTileClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var coord = ((BattleMapControl)sender).coord;

            BattleTile target = tiles[coord.X, coord.Y] as BattleTile;

            bool turnEnded = false;

            var previousTile = clickedTile;
            clickedTile = target;


            if (action == null && target.ContainsCharacter) { return; }
            
            if (action != null && action.IsAvaliable && clickedTile == previousTile)
            {
                turnEnded = !action.Do();
                action = null;
                window.HideChoiceHighlight();
            }
            else if (target.ContainsCharacter) //Если в нажатома тайле есть персонаж
            {
                if (tiles.GetDistanceBetweenTiles(turnOrder.Peek().battlePosition, target) <= 2)
                {
                    ActionChanged(this, new EventArgs());
                    action.AddTarget(target.character);
                    window.Highlight(target);
                }
                else
                {
                    clickedTile = previousTile;
                }
            }
            else //Если в нажатом тайле персонажа нет
            {
                ActionChanged(this, new EventArgs());
                var character = turnOrder.Peek();
                action = new MoveAction();
                action.AddActor(character);
                action.AddTarget(new Path(character.battlePosition, target, tiles, window));
                window.Highlight(target);
            }

            if (turnEnded)
            {
                EndTurn();
            }
        }

        public void CheckForEndGame()
        {
            if (!playerTeam.IsAlive)
            {
                BattleEnded(this, new BattleEndEventArgs("Zombie"));
            }
            if (!opponentTeam.IsAlive)
            {
                BattleEnded(this, new BattleEndEventArgs("Battle Bros"));
            }
        }

        public void DrawAllGraphics(BattleWindow window)
        {
            window.image.Source = Imaging.CreateBitmapSourceFromHBitmap(tiles.GetMapTexture().GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public void EndTurn()
        {
            ActionChanged(this, new EventArgs());

            action = new EmptyAction();

            turnOrder.Dequeue();

            if (turnOrder.Count == 0)
            {
                FillQueue();
            }
            TurnDone(this, new EventArgs());
            CheckForEndGame();
        }

        public void ChooseAction(Action action)
        {
            this.action = action;
        }

        public class BattleEndEventArgs
        {
            public string message;

            public BattleEndEventArgs(string winnerTeam)
            {
                message = winnerTeam + " has won the battle!";
            }
        }
    }
}
