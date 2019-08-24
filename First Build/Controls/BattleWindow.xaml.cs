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
        static readonly (int x, int y) mapSize = (20, 20);

        List<HighlightedTile> highlightedTiles = new List<HighlightedTile>();

        public Battle battle;

        HighlightedTile highlightedTile;


        public BattleWindow()
        {
            InitializeComponent();

            var (width, height) = HexMap.GetMapPixelSize(mapSize);
            mapContainer.Width = width;
            mapContainer.Height = height;

            battle = new Battle(mapSize, this);

            //battle.DrawAllGraphics(this);

            battle.BattleEnded += Battle_BattleEnded;
            battle.ActionChanged += Battle_ActionChanged;
            battle.TurnDone += Battle_TurnDone;

            Battle_TurnDone(null, null);
        }

        private void Battle_TurnDone(object sender, EventArgs e)
        {
            ShowCurrentCharacterStats();
            ShowActions(battle.turnOrder.Peek());
            foreach (ActionControl item in actionsCanvas.Children)
            {
                item.HideSelection();
            }
        }

        private void ShowCurrentCharacterStats()
        {
            listBox.Items.Clear();
            var c = battle.turnOrder.Peek();

            listBox.Items.Add(c.name);

            foreach (var item in c.Status)
            {
                listBox.Items.Add(item);
            }
            foreach (var item in c.equipment.avaliableActions)
            {
                foreach (var item2 in item.Description)
                {
                    listBox.Items.Add(item2);
                }
            }
        }

        private void Battle_ActionChanged(object sender, EventArgs e)
        {
            HidePath();
            HideChoiceHighlight();
            foreach (ActionControl item in actionsCanvas.Children)
            {
                item.HideSelection();
            }
        }

        public void DrawPath(Path path)
        {
            HidePath();
            foreach (BattleTile item in path.tiles)
            {
                var b = new SolidColorBrush(Color.FromArgb(50, 0, 0, 255));
                var control = new HighlightedTile(b);
                var coord = HexMap.HexToPixel(item.coord);

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

        public void Highlight(BattleTile tile)
        {
            HideChoiceHighlight();

            var b = new SolidColorBrush(Color.FromArgb(50, 255, 0, 0));
            var control = new HighlightedTile(b);
            var coord = HexMap.HexToPixel(tile.coord);

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

        public void ShowActions(Character character)
        {
            actionsCanvas.Children.Clear();
            int i = 0;

            foreach (var item in character.AvaliableActions)
            {
                var actionControl = new ActionControl(item, i++);
                actionControl.ActionChosen += ActionControl_ActionChosen;
                actionsCanvas.Children.Add(actionControl);
            }
        }

        private void ActionControl_ActionChosen(object sender, EventArgs e)
        {
            var s = sender as ActionControl;
            battle.ChooseAction(s.action);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WorldWindow w = new WorldWindow();
            w.Show();
            Close();
        }
    }
}
