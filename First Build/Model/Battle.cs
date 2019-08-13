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
        public BitmapSource textureSource;

        public Queue<Character> turnOrder = new Queue<Character>();

        public event EventHandler<BattleEndEventArgs> BattleEnded;

        public Battle((int w, int h) size, BattleWindow window)
        {
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

                var coord = HexMap.GetHexCoordinate(item.position.coord.x, item.position.coord.y);

                Canvas.SetTop(control, coord.y);
                Canvas.SetLeft(control, coord.x);

                window.mapContainer.Children.Add(control);
            }
        }

        protected void GenerateHexControls(Canvas canvas)
        {
            foreach (var item in tiles.GetArray())
            {
                var control = new BattleMapControl(item.coord);

                var (x, y) = HexMap.GetHexCoordinate(item.coord.x, item.coord.y);

                Canvas.SetTop(control, y);
                Canvas.SetLeft(control, x);

                control.MouseUp += OnTileClick;

                canvas.Children.Add(control);
            }
        }

        private void OnTileClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var control = sender as BattleMapControl;
            var coord = control.coord;

            var tile = tiles[coord.x, coord.y];

            if (turnOrder.Count == 0) { FillQueue(); }
            var character = turnOrder.Peek();

            if (tile.character == null)
            {
                if (!character.TryToMove(tile)) { Console.WriteLine("Can't move here"); }
                else { turnOrder.Dequeue(); }
            }
            else
            {
                character.Attack(tile.character);
                turnOrder.Dequeue();
            }

            CheckForEndGame();
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

        public BitmapSource PrepareMapTexture()
        {
            var textureSize = HexMap.GetMapPixelSize(battleMapSize);
            texture = new Bitmap(textureSize.width, textureSize.height);

            Graphics g = Graphics.FromImage(texture);
            foreach (var item in tiles.GetArray())
            {
                var pixelCoord = HexMap.GetHexCoordinate(item.coord.x, item.coord.y, System.Drawing.Point.Empty);
                g.DrawImage(item.terrain.texture, pixelCoord);
            }

            texture.Save("BattleMap-" + id + ".png");

            return textureSource = Imaging.CreateBitmapSourceFromHBitmap(texture.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public void DrawAllGraphics(BattleWindow window)
        {
            PrepareMapTexture();
            window.image.Source = textureSource;
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
