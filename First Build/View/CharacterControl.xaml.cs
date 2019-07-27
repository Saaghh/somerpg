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
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class CharacterControl : UserControl
    {
        public Character character = new Character();

        public Character target;

        public CharacterControl()
        {
            InitializeComponent();
        }
        public CharacterControl(Character trgt): this()
        {
            target = trgt;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            character.Attack(target);
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            textBox.Text = character.name + ": " +character.health.ToString();
        }
    }
}
