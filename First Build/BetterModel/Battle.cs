using First_Build.Controller;
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

namespace First_Build.BetterModel
{
    public class Battle
    {
        public static int amount = 0;
        public readonly string id = amount++.ToString();

        public (int width, int height) battleMapSize;

        public Tile[,] tiles;

        public Character[] playerTeam;
        public Character[] opponentTeam;

        public Bitmap texture;
        public BitmapSource textureSource;

        public Queue<Character> turnOrder = new Queue<Character>();

        public Battle((int w, int h) size, BattleWindow window)
        {
            battleMapSize = size;
            tiles = new Tile[size.w, size.h];
            GenerateMap();

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
                turnOrder.Enqueue(item);
            }
            foreach (var item in opponentTeam)
            {
                turnOrder.Enqueue(item);
            }
        }

        protected void IniTeams()
        {
            playerTeam = new Character[5];
            opponentTeam = new Character[5];

            for (int i = 0; i < playerTeam.Length; i++)
            {
                playerTeam[i] = new Character();
                var coord = HexMapMath.GetCharacterStarterTilePosition(battleMapSize, i, 0);
                playerTeam[i].EngageBattle(tiles[coord.x, coord.y]);
                playerTeam[i].Moved += Battle_Moved;
            }

            for (int i = 0; i < opponentTeam.Length; i++)
            {
                opponentTeam[i] = new Character();
                var coord = HexMapMath.GetCharacterStarterTilePosition(battleMapSize, i, 1);
                opponentTeam[i].EngageBattle(tiles[coord.x, coord.y]);
                playerTeam[i].Moved += Battle_Moved;
            }
        }

        private void Battle_Moved(object sender, Character.MoveEventArgs e)
        {
            var actor = sender as Character;
            Console.Write("{0} has moved to tile: {1}", actor.name, e.target.coord);
        }

        protected void GenerateAllCharactedControls(BattleWindow window)
        {
            GenerateTeamControls(playerTeam, window);
            GenerateTeamControls(opponentTeam, window);
        }

        protected void GenerateTeamControls(Character[] team, BattleWindow window)
        {
            foreach (var item in team)
            {
                var control = new CharacterControl(item);
                control.image.Source = item.textureSource;

                var coord = HexMapMath.GetHexCoordinate(item.position.coord.x, item.position.coord.y);

                Canvas.SetTop(control, coord.y);
                Canvas.SetLeft(control, coord.x);

                window.mapContainer.Children.Add(control);
            }
        }

        protected void GenerateMap()
        {
            for (int i = 0; i < battleMapSize.width; i++)
            {
                for (int j = 0; j < battleMapSize.height; j++)
                {
                    tiles[i, j] = new Tile((i, j));
                    tiles[i, j].terrain = Terrain.Flat;
                }
            }
        }

        protected void GenerateHexControls(Canvas canvas)
        {
            foreach (var item in tiles)
            {
                var control = new BattleMapControl(item.coord);

                var (x, y) = HexMapMath.GetHexCoordinate(item.coord.x, item.coord.y);

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
        }

        public BitmapSource PrepareMapTexture()
        {
            var textureSize = HexMapMath.GetMapPixelSize(battleMapSize);
            texture = new Bitmap(textureSize.width, textureSize.height);

            Graphics g = Graphics.FromImage(texture);
            foreach (var item in tiles)
            {
                var pixelCoord = HexMapMath.GetHexCoordinate(item.coord.x, item.coord.y, System.Drawing.Point.Empty);
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
    }
}
