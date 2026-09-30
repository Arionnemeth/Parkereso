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

namespace Parkereso
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> meretek = new List<string> { "2x2", "4x4", "6x6" };
        public MainWindow()
        {
            InitializeComponent();
            ls_meret_valaszto.ItemsSource = meretek;
        }

        
    }

}