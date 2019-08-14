using First_Build.Controls.BattleControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace First_Build.View
{
    /// <summary>
    /// Логика взаимодействия для BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window
    {
        static readonly (int x, int y) mapSize = (x: HexMap.MAPWIDTH, y: HexMap.MAPHEIGHT);

        List<HighlightedTile> highlightedTiles = new List<HighlightedTile>();

        public Battle battle;

        HighlightedTile highlightedTile;


        public BattleWindow()
        {
            InitializeComponent();

            var size = HexMap.GetMapPixelSize(mapSize);
            mapContainer.Width = size.width;
            mapContainer.Height = size.height;

            battle = new Battle(mapSize, this);

            //battle.DrawAllGraphics(this);

            battle.BattleEnded += Battle_BattleEnded;
            battle.ActionChanged += Battle_ActionChanged;
        }

        private void Battle_ActionChanged(object sender, EventArgs e)
        {
            HidePath();
            HideChoiceHighlight();
        }

        public void DrawPath(Path path)
        {
            HidePath();
            foreach (Tile item in path.tiles)
            {
                var b = new SolidColorBrush(Color.FromArgb(50, 0, 0, 255));
                var control = new HighlightedTile(b);
                var coord = HexMap.GetHexCoordinate(item.coord.X, item.coord.Y);

                Canvas.SetLeft(control, coord.X);
                Canvas.SetTop(control, coord.Y);

                Panel.SetZIndex(control, -2);

                mapContainer.Children.Add(control);
                highlightedTiles.Add(control);
            }
        }

        public void HidePath()
        {
            if (highlightedTiles != null)
            {
                foreach (HighlightedTile item in highlightedTiles)
                {
                    mapContainer.Children.Remove(item);
                }
            }
        }

        public void Highlight(Tile tile)
        {
            HideChoiceHighlight();

            var b = new SolidColorBrush(Color.FromArgb(50, 255, 0, 0));
            var control = new HighlightedTile(b);
            var coord = HexMap.GetHexCoordinate(tile.coord.X, tile.coord.Y);

            Canvas.SetLeft(control, coord.X);
            Canvas.SetTop(control, coord.Y);

            Panel.SetZIndex(control, -2);

            mapContainer.Children.Add(control);
            highlightedTile = control;
        }

        public void HideChoiceHighlight()
        {
            if (highlightedTile != null)
            {
                mapContainer.Children.Remove(highlightedTile);
            }
        }

        private void Battle_BattleEnded(object sender, Battle.BattleEndEventArgs e)
        {
            MessageBox.Show(e.message);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - 50);
                    break;
                case Key.A:
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - 50);
                    break;
                case Key.S:
                    scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + 50);
                    break;
                case Key.D:
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + 50);
                    break;
            }
        }

        private void EndTurnButton_Click(object sender, RoutedEventArgs e)
        {
            battle.EndTurn();
        }
    }
}
