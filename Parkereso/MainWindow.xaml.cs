using System.Diagnostics.Eventing.Reader;
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

       public Dictionary<string, string> f1 = new Dictionary<string, string>
                        {
                            {"Lewis Hamilton", "Ferrari"},
                            {"Max Verstappen", "Red Bull Racing"},
                            {"Charles Leclerc", "Ferrari"},
                            {"Lando Norris", "McLaren"},
                            {"Sergio Perez", "Caddilac"},
                            {"George Russell", "Mercedes"},
                            {"Carlos Sainz", "Williams"},
                            {"Oscar Piastri", "McLaren"},
                            {"Valtteri Bottas", "Caddilac"},
                            {"Fernando Alonso", "Aston Martin"},
                            {"Esteban Ocon", "Alpine"},
                            {"Pierre Gasly", "Alpine"},
                            {"Logan Sargeant", "Racing Bulls"},
                            {"Gabriel Bortoleto", "Audi"},
                            {"Nico Hulkenberg", "Audi"},
                            {"Nicolas Goatifi", "Williams"},
                            {"Lance Stroll", "Aston Martin"},
                            { "Sebastian Vettel", "Red Bull Racing"}
                        };

        int score;
        public MainWindow()
        {
            InitializeComponent();
            ls_meret_valaszto.ItemsSource = meretek;
            ls_jatekmod.ItemsSource = new List<string> { "Számok", "Emojik", "F1-esek és csapataik" };
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
                    if (ls_jatekmod.SelectedItem == "Számok")
                    {
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
                    else if (ls_jatekmod.SelectedItem == "Emojik")
                    {
                        List<string> smilek = new List<string>();
                        List<string> emojik = new List<string> { "😀", "😂", "😍", "😎", "🤔", "😢", "😡", "🥳", "😇", "🤯", "🥶", "😱","😭","❤️", "💔","✔️","😯","🌑" };
                        for (int k = 0; k < meret_i * meret_i / 2; k++)
                        {
                            smilek.Add(emojik[k]);
                            smilek.Add(emojik[k]);
                        }
                        smilek = smilek.Shuffle().ToList();
                        int index = 0;
                        for (int k = 0; k < meret_i * meret_i; k++)
                        { 
                                Button btn = new Button();
                                btn.DataContext = smilek[index++];
                                btn.Background = Brushes.LawnGreen;
                                btn.FontSize = 20;
                                btn.FontWeight = FontWeights.Bold;
                                btn.Content = "?";
                                Grid.SetRow(btn, k / meret_i);
                                Grid.SetColumn(btn, k % meret_i);
                                puzzle_Grid.Children.Add(btn);
                                btn.Click += Btn_Click;    
                        }
                    }
                    else if(ls_jatekmod.SelectedItem == "F1-esek és csapataik")
                    {
                       
                        List<string> parok = new List<string>();
                        foreach (var item in f1.Take(meret_i * meret_i / 2))
                        {
                            parok.Add(item.Key);
                            parok.Add(item.Value);
                        }

                        parok = parok.Shuffle().ToList();
                        int index = 0;
                        for (int k = 0; k < meret_i * meret_i; k++)
                        {
                            Button btn = new Button();
                            btn.Background = Brushes.LawnGreen;
                            btn.DataContext = parok[index++];
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
        }

        string elozo_btn = null;
        Button elozo = null;
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (ls_jatekmod.SelectedItem == "Számok")
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
            else if (ls_jatekmod.SelectedItem == "Emojik")
            {
                Button btn = sender as Button;
                if (btn != null)
                {
                    btn.Content = btn.DataContext.ToString();
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
            else if (ls_jatekmod.SelectedItem?.ToString() == "F1-esek és csapataik")
            {
                Button btn = sender as Button;

                string felirat = btn.DataContext.ToString();
                btn.Content = felirat;
                btn.Background = Brushes.LightSkyBlue;

                if (elozo_btn == null)
                {
                    elozo_btn = felirat;
                    elozo = btn;
                }
                else {
                    bool talalat =
                        (f1.ContainsKey(elozo_btn) && f1[elozo_btn] == felirat) ||
                        (f1.ContainsKey(felirat) && f1[felirat] == elozo_btn);

                    if (talalat)
                    {
                        MessageBox.Show("Találtál egy párt!");

                        btn.Foreground = Brushes.LightGreen;
                        elozo.Foreground = Brushes.LightGreen;
                        btn.IsEnabled = false;
                        elozo.IsEnabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Nem találtál párt!");

                        btn.Content = "?";
                        elozo.Content = "?";
                        btn.Background = Brushes.LawnGreen;
                        elozo.Background = Brushes.LawnGreen;
                    }
                    score += 1;
                    score_block.Text = score.ToString();

                    elozo_btn = null;
                    elozo = null;
                }

               
            }


        }

 
    }
}