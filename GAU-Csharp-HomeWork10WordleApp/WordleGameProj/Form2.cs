using System;
using System.Linq;
using System.Windows.Forms;

namespace WordleGameProj
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();

            using (var context = new WordleGameDatabaseEntities())
            {
                bool emailExists = context.Users.Any(u => u.Email == email);

                if (emailExists)
                {
                    Form4 form4 = new Form4(email);
                    this.Hide();
                    form4.ShowDialog();
                    this.Close();
                }
                else
                {
                    Form3 form3 = new Form3(email);
                    this.Hide();
                    form3.ShowDialog();
                    this.Close();
                }
            }
        }
    }
}
