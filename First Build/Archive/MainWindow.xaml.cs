using First_Build.Model;
using First_Build.Controller;
using First_Build.View;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        public void StartGame()
        {
            Game.IniGame();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateMap();
        }

        public void CreateMap()
        {
            mainGrid.Children.Add(new CharacterControl());
            var control = mainGrid.Children[1] as CharacterControl;
            control.Margin = new Thickness(300, 300, 0, 0);
        }

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

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            var b = new BattleWindow();
            b.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var b = new BattleWindow();
            b.Show();
            Close();
        }
    }
}
