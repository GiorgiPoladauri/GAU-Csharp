using System;
using System.Windows.Forms;
using static MiniOrderManagementSystemProj.EntityClasses;
using System.Xml.Linq;

namespace OrderManagementProj
{
    public partial class Form2 : Form
    {
        private int? _customerId;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(int customerId)
            : this()
        {
            _customerId = customerId;
            LoadCustomer(customerId);
        }

        private void LoadCustomer(int customerId)
        {
            using (var ctx = new AppDbContext())
            {
                var c = ctx.Customers.Find(customerId);
                if (c != null)
                {
                    textBox1.Text = c.FirstName;
                    textBox2.Text = c.LastName;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var ctx = new AppDbContext())
            {
                Customer c;
                if (_customerId.HasValue)
                {
                    c = ctx.Customers.Find(_customerId.Value);
                }
                else
                {
                    c = new Customer();
                    ctx.Customers.Add(c);
                }

                c.FirstName = textBox1.Text.Trim();
                c.LastName = textBox2.Text.Trim();

                ctx.SaveChanges();
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
