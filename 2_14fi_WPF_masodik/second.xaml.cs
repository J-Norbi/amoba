﻿using System;
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
        ServerConnection connection;
        public second(ServerConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            Start();
        }
        void Start()
        {
            jatekos1.Content = Dataa.users[0].username;
            jatekos2.Content = Dataa.users[1].username;
            jatekos1.Background = new SolidColorBrush(Colors.Blue);
            pontok1.Content = $"Pontok: {Dataa.users[0].win}/{Dataa.users[0].lose}/{Dataa.users[0].draw}";
            pontok2.Content = $"Pontok: {Dataa.users[1].win}/{Dataa.users[1].lose}/{Dataa.users[1].draw}";
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
        private void ClickEvent(object s, EventArgs e)
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
                    jatekos1.Background = new SolidColorBrush(Colors.Blue);
                    jatekos2.Background = new SolidColorBrush(Colors.White);
                }
                else
                {
                    (s as Button).Content = "X";
                    jatekos1.Background = new SolidColorBrush(Colors.White);
                    jatekos2.Background = new SolidColorBrush(Colors.Red);
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
            {
                MessageBox.Show("Az első játékos nyert.");
                Dataa.users[0].win++;
                Dataa.users[1].lose++;
            }
            else if (check == -1)
            {
                MessageBox.Show("A második játékos nyert.");
                Dataa.users[1].win++;
                Dataa.users[0].lose++;
            }
            else if (ClickCount == 9)
            {
                MessageBox.Show("Döntetlen");
                Dataa.users[0].draw++;
                Dataa.users[1].draw++;
            }
            else
                return;
            //csak akkor fut le, ha vége a játéknak
            GameOver(check);
        }
        private void GameOver(int num)
        {
            pontok1.Content = $"Pontok: {Dataa.users[0].win}/{Dataa.users[0].lose}/{Dataa.users[0].draw}";
            pontok2.Content = $"Pontok: {Dataa.users[1].win}/{Dataa.users[1].lose}/{Dataa.users[1].draw}";
            jatekos1.Background = new SolidColorBrush(Colors.White);
            jatekos2.Background = new SolidColorBrush(Colors.White);
            if(Dataa.users.Where(user => user.token != null).Count() == 2)
            {
                connection.Save(Dataa.users[0], num);
                connection.Save(Dataa.users[1], -num);
            }
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
        private void ShowMatrix()   // ezzel ellnőriztük, hogy mit ír ki a kattingatásnál
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
