namespace TraderApp
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            LoginForm loginform = new LoginForm();
            loginform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            RegisterForm registerform = new RegisterForm();
            registerform.Show();
        }
    }
}
