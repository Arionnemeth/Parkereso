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
        private List<int> karakterek;

        int score;
        public MainWindow()
        {
            InitializeComponent();
            ls_meret_valaszto.ItemsSource = meretek;
        }

        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            score_block.Text = "0";
            score = 0;
            int meret_i;
            if (ls_meret_valaszto.SelectedItem != null)
            {
                string meret = ls_meret_valaszto.SelectedItem.ToString();
                meret_i = int.Parse(meret.Split('x')[0]);

                for (int i = 0; i < meret_i; i++)
                {
                    puzzle_Grid.RowDefinitions.Clear();
                    puzzle_Grid.ColumnDefinitions.Clear();
                    for (int j = 0; j < meret_i; j++)
                    {
                        puzzle_Grid.RowDefinitions.Add(new RowDefinition());
                        puzzle_Grid.ColumnDefinitions.Add(new ColumnDefinition());
                    }
                    List<int> karakterek = new List<int>();
                    for (int k = 0; k < meret_i * meret_i / 2; k++)
                    {
                        karakterek.Add(k);
                        karakterek.Add(k);
                    }
                    karakterek = karakterek.Shuffle().ToList();
                    int index = 0;
                    for (int k = 0; k < meret_i * meret_i; k++)
                    {
                        Button btn = new Button();
                        btn.Background = Brushes.LawnGreen;
                        btn.Name = "btn_" + karakterek[index++].ToString();
                        btn.FontSize = 20;
                        btn.FontWeight = FontWeights.Bold;
                        btn.Content = "?";
                        Grid.SetRow(btn, k / meret_i);
                        Grid.SetColumn(btn, k % meret_i);
                        puzzle_Grid.Children.Add(btn);
                        btn.Click += Btn_Click;
                    }

                }
            }
        }

        string elozo_btn = null;
        Button elozo = null;
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            
            Button btn = sender as Button;
            if (btn != null)
            {
                btn.Content = btn.Name.Split('_')[1];
                btn.Background = Brushes.LightSkyBlue;
            }
            string felirat = btn.Content.ToString();
            if (elozo_btn == null)
            {
                elozo_btn = felirat;
                elozo = btn;
                btn.Background = Brushes.LightSkyBlue;

            }
            else if (elozo_btn == felirat)
            {
                MessageBox.Show("Találtál egy párt!");
                elozo_btn = null;
                btn.Foreground = Brushes.LightGreen;
                elozo.Foreground = Brushes.LightGreen;
                btn.IsEnabled = false;
                elozo.IsEnabled = false;
                score += 1;
                score_block.Text = score.ToString();
            }
            else
            {
                MessageBox.Show("Nem találtál párt!");
                elozo_btn = null;
                btn.Content = "?";
                elozo.Content = "?";
                score += 1;
                score_block.Text = score.ToString();
                btn.Background = Brushes.LawnGreen; 
                elozo.Background = Brushes.LawnGreen;
            }
        }
    }
}