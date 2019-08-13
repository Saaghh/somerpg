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
            character.GotAttacked += Character_GotAttacked;
            character.RoundStarter += Character_RoundStarter;

            SetTexture();

            ShowData();
        }

        private void SetTexture()
        {
            image.Source = character.PrepareTexture();
        }

        private void Character_RoundStarter(object sender, EventArgs e)
        {
            ShowData();
        }

        private void Character_GotAttacked(object sender, Character.AttackedEventArgs e)
        {
            ShowData();
            if (!character.isAlive) { canvas.Visibility = Visibility.Hidden; }
        }

        private void Character_Moved(object sender, Character.MoveEventArgs e)
        {
            (int x, int y) = HexMap.GetHexCoordinate(e.target.coord.x, e.target.coord.y);
            Canvas.SetTop(this, y);
            Canvas.SetLeft(this, x);

            ShowData();
        }

        public void ShowData()
        {
            ShowHealth();
            ShowAP();
            ShowName();
        }

        public void ShowHealth()
        {
            double pixelsPerHealth = healthBarBackground.Width / character.maxHealth;

            if (character.isAlive)
            {
                healthBar.Width = pixelsPerHealth * character.health;
            }

            healthText.Text = character.health + " / " + character.maxHealth;
        }

        public void ShowAP()
        {
            double pixelsPerAP = apBarBackground.Width / character.maxAP;

            apBar.Width = pixelsPerAP * character.ap;

            apText.Text = character.ap + " / " + character.maxAP;
        }

        public void ShowName()
        {
            nameText.Text = character.name;
        }
    }
}
