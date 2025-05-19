using System;
using System.Windows.Forms;

namespace WordleGameProj
{
    public partial class Form4 : Form
    {
        public Form4(string email)
        {
            InitializeComponent();
            label2.Text = email;
        }
        private void button1_Click(object sender, EventArgs e, Models.User email)
        {
            this.Hide();
            Form5 form5 = new Form5(email);
            form5.ShowDialog();
            this.Close();
        }
    }
}
