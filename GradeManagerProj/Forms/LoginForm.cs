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
            string username = textBox1.Text.Trim().ToLower();
            string password = textBox2.Text.Trim().ToLower();

            if (username == "teacher" && password == "1234")
            {
                this.Hide();
                using (var main = new MainForm())
                {
                    main.ShowDialog();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Please, Use Teacher Pass, No Need To Cheat My Funny !",
                    "Can't Log In !",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                textBox2.Clear();
                textBox1.Focus();
            }
        }
    }
}
