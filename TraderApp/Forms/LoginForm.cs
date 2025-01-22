using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TraderApp.Managers;
using TraderApp.Repositories;

namespace TraderApp
{
    public partial class LoginForm : Form
    {
        private List<User> users;

        public LoginForm()
        {
            InitializeComponent();
            users = new List<User>();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                users = UserManager.LoadUsers();

                if (users == null || users.Count == 0)
                {
                    MessageBox.Show("No users found. Please register first.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Both email and password are required.");
                return;
            }

            bool isAuthenticated = UserManager.AuthenticateUser(email, password);

            if (isAuthenticated)
            {
                MessageBox.Show("Login successful!");
                this.Hide();

                MainForm mainPage = new MainForm();
                mainPage.Show();
            }
            else
            {
                MessageBox.Show("Invalid email or password. Please try again.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            WelcomeForm welcomeForm = new WelcomeForm();
            welcomeForm.Show();
        }
    }
}
