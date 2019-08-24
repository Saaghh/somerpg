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
        MediaPlayer player = new MediaPlayer();


        public CharacterControl(Character character)
        {
            InitializeComponent();
            this.character = character;

            character.Moved += Character_Moved;
            character.GotAttacked += Character_GotAttacked;
            character.RoundStarted += Character_RoundStarter;
            character.Died += Character_Died;
            SetTexture();

            ShowData();
        }

        private void Character_Died(object sender, EventArgs e)
        {
            canvas.Visibility = Visibility.Hidden; 
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
            PlayHitSound();
        }

        private void ShowStats()
        {
            listBox.Items.Clear();

            foreach (var item in character.Status)
            {
                listBox.Items.Add(item);
            }

            foreach (var item in character.EquipmentStats)
            {
                listBox.Items.Add(item);
            }
        }

        private void PlayHitSound()
        {
            player.Open(new Uri("pack://siteoforigin:,,,/Resources/Sounds/HitSound.wav"));
            player.Play();
        }

        private void Character_Moved(object sender, Character.MoveEventArgs e)
        {
            var point = HexMap.HexToPixel(e.target.coord);
            Canvas.SetTop(this, point.Y);
            Canvas.SetLeft(this, point.X);

            ShowData();
        }

        public void ShowData()
        {
            ShowHealth();
            ShowAP();
            ShowName();
            ShowStats();
        }

        public void ShowHealth()
        {
            double pixelsPerHealth = healthBarBackground.Width / character.MaxHealth;

            if (character.IsAlive)
            {
                healthBar.Width = pixelsPerHealth * character.Health;
            }

            healthText.Text = character.Health + " / " + character.MaxHealth;
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
