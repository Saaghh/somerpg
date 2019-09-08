using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
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

namespace DataCreator
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

        Terrain terrain;



        [DataContract]
        public struct Terrain
        {
            [DataMember]
            public int moveCost;
            [DataMember]
            public string type;
            [DataMember]
            public bool walkable;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            terrain.moveCost = Convert.ToInt32(textBox.Text);
            terrain.type = textBox1.Text;
            terrain.walkable = checkBox.IsChecked.Value;

            MemoryStream memoryStream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Terrain));
            FileStream fs = new FileStream("terrain.json", FileMode.Create, FileAccess.Write);

            serializer.WriteObject(memoryStream, terrain);

            memoryStream.Position = 0;
            memoryStream.WriteTo(fs);
            fs.Close();
            memoryStream.Close();
        }
    }
}
