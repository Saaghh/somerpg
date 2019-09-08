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

namespace First_Build.Controls.BattleControls
{
    /// <summary>
    /// Логика взаимодействия для ActionControl.xaml
    /// </summary>
    public partial class ActionControl : UserControl
    {
        public Action action;
        public event EventHandler<EventArgs> ActionChosen;

        public ActionControl(Action action, int id)
        {
            InitializeComponent();
            this.action = action;
            DisplayData();
            Position(id);
        }

        void Position(int id)
        {
            Canvas.SetLeft(this, (id * button.Width) + 3);
        }

        string DescriptionToString()
        {
            string s = "";
            foreach (var item in action.Description)
            {
                s += item + "\r\n";
            }
            return s;
        }

        void DisplayData()
        {
            textBlock.Text = DescriptionToString();
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            descriptionGrid.Visibility = Visibility.Visible;
        }

        private void Rectangle_MouseLeave(object sender, MouseEventArgs e)
        {
            descriptionGrid.Visibility = Visibility.Hidden;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ActionChosen(this, new EventArgs());
            SelectButton();
        }

        public void HideSelection()
        {
            button.Fill = Brushes.White;
        }
        public void SelectButton()
        {
            button.Fill = Brushes.DarkGray;
        }
    }
}
