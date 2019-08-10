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

        public Battle((int w, int h) size, BattleWindow window)
        {
            battleMapSize = size;
            tiles = new Tile[size.w, size.h];
            GenerateMap();
            PrepareMapTexture();

            window.image.Source = textureSource;

            GenerateControls(window.mapContainer);
        }

        protected void GenerateMap()
        {
            for (int i = 0; i < battleMapSize.width; i++)
            {
                for (int j = 0; j < battleMapSize.height; j++)
                {
                    tiles[i, j] = new Tile((i, j));
                    tiles[i, j].terrain = Terrain.Water;
                }
            }
        }

        protected void GenerateControls(Canvas canvas)
        {
            foreach (var item in tiles)
            {
                var control = new BattleMapControl(item.coord);

                var pixelCoord = HexMapMath.GetHexCoordinate(item.coord.x, item.coord.y);

                Canvas.SetTop(control, pixelCoord.y);
                Canvas.SetLeft(control, pixelCoord.x);

                canvas.Children.Add(control);
            }
        }

        public void PrepareMapTexture()
        {
            var textureSize = HexMapMath.GetMapPixelSize(battleMapSize);
            texture = new Bitmap(textureSize.width, textureSize.height);

            Graphics g = Graphics.FromImage(texture);
            foreach (var item in tiles)
            {
                var pixelCoord = HexMapMath.GetHexCoordinate(item.coord.x, item.coord.y, System.Drawing.Point.Empty);
                g.DrawImage(item.terrain.texture, pixelCoord);
            }

            texture.Save("BattleMap-" + id+".png");

            textureSource = Imaging.CreateBitmapSourceFromHBitmap(texture.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
