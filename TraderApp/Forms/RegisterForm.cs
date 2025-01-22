using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TraderApp.Managers;
using TraderApp.Repositories;

namespace TraderApp
{
    public partial class RegisterForm : Form
    {
        private List<User> users;

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            try
            {
                users = UserManager.LoadUsers();

                if (users == null) 
                {
                    users = new List<User>(); 
                }
                else
                {
                    MessageBox.Show("Users loaded successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
                users = new List<User>();  
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;
            string confirmPassword = textBox3.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("All fields are required.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            users = UserManager.LoadUsers();

            if (users.Exists(u => u.Email == email))
            {
                MessageBox.Show("This email is already registered.");
                return;
            }

            int newUserId = users.Count > 0 ? users.Max(u => u.ID) + 1 : 1;
            User newUser = new User(newUserId, email, password);

            users.Add(newUser);

            UserManager.SaveUsers(users);

            MessageBox.Show("Registration successful!");

            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            WelcomeForm welcomeForm = new WelcomeForm();
            welcomeForm.Show();
        }
    }
}
