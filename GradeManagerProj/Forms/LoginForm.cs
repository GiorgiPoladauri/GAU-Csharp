// LoginForm.cs
using System;
using System.Windows.Forms;

namespace GradeManagerProj
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            // მარტივი ავთენტიფიკაცია: Username = "teacher", Password = "1234"
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if (username == "teacher" && password == "1234")
            {
                // სწორი შეყვანა → გადახვევა MainForm-ზე
                this.Hide();
                MainForm main = new MainForm();
                main.Show();
            }
            else
            {
                MessageBox.Show(
                    "არავალიდური მომხმარებელი.\nUsername: teacher\nPassword: 1234",
                    "ავტორიზაცია ვერ მოხერხდა",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                textBox2.Clear();
                textBox1.Focus();
            }
        }
    }
}
