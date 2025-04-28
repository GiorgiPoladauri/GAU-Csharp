using System;
using System.Linq;
using System.Windows.Forms;

namespace WordleGameProj
{
    public partial class Form3 : Form
    {
        public Form3(string email)
        {
            InitializeComponent();
            textBox1.Text = email;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password cannot be empty!");
                return;
            }

            else if (!checkBox1.Checked)
            {
                MessageBox.Show("You must agree to the terms before registering!", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var context = new WordleGameDatabaseEntities())
            {
                var newUser = new User
                {
                    Email = email,
                    Password = password,
                    IsFirstCheckboxChecked = true
                };

                context.Users.Add(newUser);

                context.SaveChanges();
            }

            MessageBox.Show("Registration successful!");

            this.Hide();
            Form4 form4 = new Form4(email);
            form4.ShowDialog();
            this.Close();
        }
    }
}
