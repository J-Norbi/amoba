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
using System.Windows.Shapes;

namespace _2_14fi_WPF_masodik
{
    /// <summary>
    /// Interaction logic for second.xaml
    /// </summary>
    public partial class second : Window
    {
        List<Button> buttons = new List<Button>();
        private bool Red = false;
        private int[,] data = new int[3, 3];
        private int ClickCount = 0;
        public second()
        {
            InitializeComponent();
            Start();
        }
        void Start()
        {
            jatekos1.Content += Dataa.users[0].username;
            jatekos2.Content += Dataa.users[1].username;
            pontok1.Content += $"{Dataa.users[0].win}/{Dataa.users[0].lose}/{Dataa.users[0].draw}";
            pontok2.Content += $"{Dataa.users[1].win}/{Dataa.users[1].lose}/{Dataa.users[1].draw}";
            for (int i = 0; i < 9; i++)
            {
                int row = (int)Math.Floor((double)i / 3);
                int col = i % 3;
                Button oneButton = new Button();
                Grid.SetRow(oneButton, row);
                Grid.SetColumn(oneButton, col);
                buttons.Add(oneButton);
                gameplace.Children.Add(oneButton);
                oneButton.FontSize = 80;
                oneButton.Foreground = new SolidColorBrush(Colors.White);
                oneButton.Click += ClickEvent;
                //oneButton.Content = i;
            }

        }
        private void ClickEvent(Object s, EventArgs e)
        {
            //megkeressük a mátrixban a gombot
            int index = buttons.IndexOf((s as Button));
            int row = (int)Math.Floor((double)index / 3);
            int col = index % 3;

            if (data[row, col] == 0)
            {
                //eltároljuk melyik színt kattintottuk
                int num = 1;
                if (Red) num = -1;
                data[row, col] = num;
                //kiszínezzük a megfelelő színre a gombot
                SolidColorBrush color = new SolidColorBrush(Colors.Blue);
                if (Red)
                {
                    color = new SolidColorBrush(Colors.Red);
                    (s as Button).Content = "O";
                }
                else
                {
                    (s as Button).Content = "X";
                }
                (s as Button).Background = color;
                //következő játékos jön, másik színnel
                Red = !Red;
                ClickCount++;
            }

            //ShowMatrix();
            int check = CheckBoard();
            //MessageBox.Show(check.ToString());
            if (check == 1)
                MessageBox.Show("Az első játékos nyert.");
            else if (check == -1)
                MessageBox.Show("A második játékos nyert.");
            else if (ClickCount == 9)
                MessageBox.Show("Döntetlen");
            else
                return;
            //csak akkor fut le, ha vége a játéknak

            buttons.ForEach(button => button.Click -= ClickEvent);
        }
        private int CheckBoard()
        {
            int result = 0;
            for (int i = 0; i < 3; i++)
            {
                int sum = 0;
                int sum2 = 0;
                for (int j = 0; j < 3; j++)
                {
                    sum += data[i, j];
                    sum2 += data[j, i];
                }
                if(result == 0)
                    result = Sum(sum, sum2, 3);
            }
            if(result == 0) {
                int sum = 0;
                int sum2 = 0;
                for (int i = 0; i < 3; i++)
                {
                    sum += data[i, i];
                    sum2 += data[2 - i, i];
                }
                result = Sum(sum, sum2, 3);
            }

            return result;
        }
        private int Sum(int sum, int sum2, int max)
        {
            if (sum == max || sum2 == max)
            {
                return 1;
            }
            else if (sum == -max || sum2 == -max)
            {
                return -1;
            }
            return 0;
        }
        private void ShowMatrix()
        {
            string temp = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    temp += $"{data[i, j]};";
                }
                temp += "\n";
            }
            MessageBox.Show(temp);
        }
    }
}
