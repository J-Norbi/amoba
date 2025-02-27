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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        ServerConnection connection;
        Registration regWindow;
        second gameWindow;
        public LoginWindow()
        {
            InitializeComponent();
            connection = new ServerConnection();
            Dataa.users.CollectionChanged += ObservableEvent;
            this.IsVisibleChanged += WindowVisibility;
        }
        void WindowVisibility(object s, DependencyPropertyChangedEventArgs e)            // amikor nem látszik az ablak, akkor ne fusson le az esemény updatelése
        {
            if(this.Visibility == Visibility.Visible)
            {
                Dataa.users.CollectionChanged += ObservableEvent;
            }
            else
            {
                Dataa.users.CollectionChanged -= ObservableEvent;
            }
        }
        void ObservableEvent(object s, EventArgs e)
        {
            if (Dataa.users[0].username != null)
            {
                player1.Background = new SolidColorBrush(Colors.Green);
                if (Dataa.users[0].token == null)
                {
                    player1.Background = new SolidColorBrush(Colors.LightBlue);
                }
            }
            else if (Dataa.users[0].username == null)
            {
                player1.Background = new SolidColorBrush(Colors.White);
            }
            if (Dataa.users[1].username != null)
            {
                player2.Background = new SolidColorBrush(Colors.Green);
                if (Dataa.users[1].token == null)
                {
                    player2.Background = new SolidColorBrush(Colors.LightBlue);
                }
            }
            else if (Dataa.users[1].username == null)
            {
                player2.Background = new SolidColorBrush(Colors.White);
            }
        }
        private async void LoginClick(object s, EventArgs e) {
            Button sender = s as Button;
            string username;
            string password;
            if (sender.Name == "user1Login")
            {
                username = user1Name.Text;
                password = user1Pass.Text;
                if(password.Length < 1)
                {
                    Dataa.users[0] = new JsonResponse() { username = username, draw = 0, lose = 0, win = 0 };
                    return;
                }
            }
            else
            {
                username = user2Name.Text;
                password = user2Pass.Text;
                if (password.Length < 1)
                {
                    Dataa.users[1] = new JsonResponse() { username = username, draw = 0, lose = 0, win = 0 };
                    return;
                }
            }
            if (Dataa.users.Where(user => user.username == username && user.token != null).Count() == 1)
            {
                MessageBox.Show("Duplikált bejelentkezés");
            }
            else
            {
                JsonResponse onePlayer = await connection.Login(username, password);
                if (sender.Name == "user1Login")
                    Dataa.users[0] = onePlayer;
                else
                    Dataa.users[1] = onePlayer;
            }
        }
        private void RegistrationClick(object s, EventArgs e) 
        {
            regWindow = new Registration();
            regWindow.Top = this.Top;
            regWindow.Left = this.Left;
            regWindow.Closing += (ss, ee) =>
            {
                this.Show();
            };
            
            regWindow.Show();
            this.Hide();
        }
        private void NewGame(object s, EventArgs e)
        {
            
            if(Dataa.users.Where(user => user.username != null).Count() == 2)
            {
                this.Hide();
                gameWindow = new second(connection);
                gameWindow.Show();
                gameWindow.Closing += (ss, ee) =>
                {
                    this.Show();
                };
            }
            else
            {
                MessageBox.Show("Nincs két bejelentkezett felhasználó");
            }
        }
    }
}
