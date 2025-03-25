using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daily.Data
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.AcceptButton = Button1;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            UserDefinition user = new UserDefinition();
            user.FirstName = Nametext.Text;
            user.LastName = Surnametext.Text;
            user.Username = Usertext.Text;

            if (string.IsNullOrWhiteSpace(Nametext.Text) ||
        string.IsNullOrWhiteSpace(Surnametext.Text) ||
        string.IsNullOrWhiteSpace(Usertext.Text))
            {
                MessageBox.Show("Please enter all information");
            }
            else
            {
             MessageBox.Show("Login successful");
             this.Hide();
             Form1 form = new Form1(user);
             form.Show();
            }
         
        }
    }
}
