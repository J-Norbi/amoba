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
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        ServerConnection server = new ServerConnection();
        public Registration()
        {
            InitializeComponent();
        }
        void RegClick(object s, EventArgs e)
        {
            if(!(password.Text.Length > 2 && password.Text == password2.Text))
            {
                MessageBox.Show("Nem megfelelőek a jelszavak.", "Error");
                return;
            }
            if(username.Text.Length < 3)
            {
                MessageBox.Show("Nem elég hosszú a felhasználónév", "Error");
                return;
            }
            server.Registration(username.Text, password.Text);
            this.Close();
        }
        void CancelClick(object s, EventArgs e)
        {
            this.Close();
        }
    }
}
