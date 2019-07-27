using First_Build.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace First_Build
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game game;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void StartGame()
        {
            game = new Game();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FillGrid(x, y);
        }

        int x = 2;
        int y = 1;
        public void FillGrid(int width, int height)
        {
            for (int j = 0; j < height; j++)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < width; i++)
            {
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
                for (int j = 0; j < height; j++)
                {
                    CharacterControl UIControl = new CharacterControl();
                    Grid.SetColumn(UIControl, i);
                    Grid.SetRow(UIControl, j);

                    mainGrid.Children.Add(UIControl);
                }
            }
        }
    }
}
