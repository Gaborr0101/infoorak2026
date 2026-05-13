using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kockaak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        Random random = new Random();


        private void button_Click(object sender, RoutedEventArgs e)
        {
            panel.Children.Clear();
            int szam;

            try
            {
                szam=int.Parse(szamok.Text);

            }

            catch
            {
                MessageBox.Show("Nem számot adtál meg!");
                return; 
            }
            for (int i = 0; i < szam; i++)
            {

                WrapPanel sor = new WrapPanel();
                int osszeg = 0;

                for (int j = 0; j < 3; j++)
                {
                    int kocka = random.Next(1, 7);
                    Image kep = new Image();
                    kep = new Image();
                    
                    kep.Source = new BitmapImage(new Uri($"pack://application:,,,/kepek/{kocka}.png", UriKind.Absolute));
                    kep.Width = 50;
                    kep.Height = 50;
                    sor.Children.Add(kep);
                    osszeg += kocka;
                }


                if (osszeg < 10)
                {
                    sor.Children.Insert(0,new Image()
                    {
                        Source = new BitmapImage(new Uri($"pack://application:,,,/kepek/Anni.jpg", UriKind.Absolute)),
                        Width = 50,
                        Height = 50
                    });
                }
                else
                {
                    sor.Children.Add( new Image()
                    {
                        Source = new BitmapImage(new Uri($"pack://application:,,,/kepek/Panni.jpg", UriKind.Absolute)),
                        Width = 50,
                        Height = 50
                    });
                }

                    panel.Children.Add(sor);


             

            }
        }
    }
}