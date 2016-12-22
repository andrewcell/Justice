using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace ICE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int _port;
        private static X509Certificate2 cert = null;
        public MainWindow()
        {
            InitializeComponent();
            textBoxa.Text = "16105";
            textBoxa_Copy.Text = "c:/users/andrew/certs/local.pfx";
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            _port = int.Parse(textBoxa.Text);
            Water w = new Water(textBoxa_Copy.Text, _port);
            
            w.RunServer();
            
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dia = new OpenFileDialog
            {
                DefaultExt = ".p12",
                Filter = "*.*|*.*"
            };

            bool? result = dia.ShowDialog();
            if (result == true)
            {
                textBoxa_Copy.Text = dia.FileName;
            }
        }

        public void updateLog(string a)
        {
            listBox.Items.Add(a);
        } 
        
    }
}
