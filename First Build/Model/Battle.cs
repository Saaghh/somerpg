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

        public HexMap tiles = new HexMap();

        public Party playerTeam;
        public Party opponentTeam;

        public Bitmap texture;

        public Queue<Character> turnOrder = new Queue<Character>();

        public event EventHandler<BattleEndEventArgs> BattleEnded;
        public event EventHandler<EventArgs> ActionChanged;

        Action action = new EmptyAction();

        BattleWindow window;

        Tile clickedTile;

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
                if (item.isAlive)
                {
                    turnOrder.Enqueue(item);
                    item.GetReadyForNewRound();
                }
            }

            foreach (var item in opponentTeam)
            {
                if (item.isAlive)
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
                var (x, y) = HexMap.GetCharacterStarterTilePosition(battleMapSize, i, 0);
                playerTeam[i].EngageBattle(tiles[x, y]);
            }

            for (int i = 0; i < opponentTeam.Count; i++)
            {
                opponentTeam[i] = Character.Zombie;
                var (x, y) = HexMap.GetCharacterStarterTilePosition(battleMapSize, i, 1);
                opponentTeam[i].EngageBattle(tiles[x, y]);
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

                var coord = HexMap.GetHexCoordinate(item.position.coord.X, item.position.coord.Y);

                Canvas.SetTop(control, coord.Y);
                Canvas.SetLeft(control, coord.X);

                window.mapContainer.Children.Add(control);
            }
        }

        protected void GenerateHexControls(Canvas canvas)
        {
            foreach (Tile item in tiles)
            {
                var control = new BattleMapControl(item.coord);

                var point = HexMap.GetHexCoordinate(item.coord.X, item.coord.Y);

                Canvas.SetTop(control, point.Y);
                Canvas.SetLeft(control, point.X);

                control.MouseUp += OnTileClick;

                canvas.Children.Add(control);
            }
        }

        private void OnTileClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var coord = ((BattleMapControl)sender).coord;

            Tile target = tiles[coord.X, coord.Y];

            bool turnEnded = false;

            if (action.IsAvaliable && target == clickedTile)
            {
                turnEnded = !action.Do();
                window.HideChoiceHighlight();
            }
            else if (target.ContainsCharacter)
            {
                ActionChanged(this, new EventArgs());
                action = new AttackAction(turnOrder.Peek(), target.character);
                window.Highlight(target);
            }
            else
            {
                ActionChanged(this, new EventArgs());
                var character = turnOrder.Peek();
                action = new MoveAction(character, new Path(character.position, target, tiles, window));
                window.Highlight(target);
            }

            if (turnEnded)
            {
                EndTurn();
            }

            clickedTile = target;
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

        public class BattleEndEventArgs
        {
            public string message;

            public BattleEndEventArgs(string winnerTeam)
            {
                message = winnerTeam + " has won the battle!";
            }
        }

        public void EndTurn()
        {
            ActionChanged(this, new EventArgs());

            action = new EmptyAction();

            turnOrder.Dequeue();

            CheckForEndGame();
        }
    }
}
