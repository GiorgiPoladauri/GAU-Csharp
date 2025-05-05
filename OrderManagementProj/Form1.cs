using System;
using System.Linq;
using System.Windows.Forms;

namespace OrderManagementProj
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RefreshGrid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                using (var ctx = new AppDbContext())
                {
                    var count = ctx.Customers.Count();
                    MessageBox.Show($"RUN COMPLETE DUDE – found {count} customers.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("EF error: " + ex.Message);
            }
        }

        private void RefreshGrid()
        {
            using (var ctx = new AppDbContext())
            {
                var allCustomers = ctx.Customers.ToList();

                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = allCustomers;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Form2 = new Form2();

            Form2.FormClosed += (s, args) => this.Close();

            this.Hide();

            Form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Please select one customer to delete.");
                return;
            }

            int id;

            try
            {
                id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CustomerId"].Value);
            }
            catch (Exception)
            {
                try
                {
                    id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                }
                catch
                {
                    MessageBox.Show("Could not determine selected customer's ID.");
                    return;
                }
            }

            if (MessageBox.Show("Are you sure you want to delete this customer?",
                                "Confirm Delete",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            using (var ctx = new AppDbContext())
            {
                var cust = ctx.Customers.Find(id);
                if (cust != null)
                {
                    ctx.Customers.Remove(cust);
                    ctx.SaveChanges();
                }
            }

            RefreshGrid();
        }
    }
}