using First_Build.BetterModel;
using First_Build.Controller;
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
        public readonly Character character;
        public CharacterControl(Character character)
        {
            InitializeComponent();
            this.character = character;

            character.Moved += Character_Moved;
            character.Attacked += Character_Attacked;

            textBlock.Text = character.name + ": " + character.health + "/" + character.maxHealth;
        }

        private void Character_Attacked(object sender, Character.AttackedEventArgs e)
        {
            textBlock.Text = character.name + ": " + character.health + "/" + character.maxHealth;
        }

        private void Character_Moved(object sender, Character.MoveEventArgs e)
        {
            (int x, int y) = HexMapMath.GetHexCoordinate(e.target.coord.x, e.target.coord.y);
            Canvas.SetTop(this, y);
            Canvas.SetLeft(this, x);
        }
    }
}
