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

        }
        private void LoginClick(object s, EventArgs e) {
            Button sender = s as Button;
            string username;
            string password;
            if (sender.Name == "user1Login")
            {
                username = user1Name.Text;
                password = user1Pass.Text;
            }
            else
            {
                username = user2Name.Text;
                password = user2Pass.Text;
            }
            connection.Login(username, password);
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
            
            if(Dataa.users.Count == 2)
            {
                this.Hide();
                gameWindow = new second();
                gameWindow.Show();
                gameWindow.Closing += (ss, ee) =>
                {
                    this.Show();
                };
            }
        }
    }
}
