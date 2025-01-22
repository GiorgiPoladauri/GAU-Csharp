using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TraderApp;
using TraderApp.Managers;
using TraderApp.Repositories;

namespace TraderApp
{
    public partial class MainForm : Form
    {
        private List<Item> myItems;
        private int nextItemId;

        public MainForm()
        {
            InitializeComponent();
            myItems = new List<Item>();
            nextItemId = 1;
            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            myItems = ItemManager.LoadItems(); 
            dataGridView1.DataSource = null; 
            dataGridView1.DataSource = myItems; 

            dataGridView2.DataSource = null; 
            dataGridView2.DataSource = myItems; 
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshDataGridView(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            int quantity = (int)numericUpDown1.Value;
            decimal price;

            if (!decimal.TryParse(textBox2.Text, out price))
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter an item name.");
                return;
            }

            int newId = myItems.Count + 1;

            Item newItem = new Item(newId, name, price, quantity, DateTime.Now);

            ItemManager.AddItem(newItem);

            RefreshDataGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int itemId = (int)dataGridView1.Rows[row.Index].Cells["ID"].Value;
                    ItemManager.DeleteItem(itemId);
                }

                RefreshDataGridView();
            }
            else
            {
                MessageBox.Show("Please select an item to delete.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            WelcomeForm welcomeForm = new WelcomeForm();
            welcomeForm.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = comboBox1.SelectedItem.ToString();

            List<Item> sortedItems = new List<Item>();

            switch (selectedOption)
            {
                case "Price Asc":
                    sortedItems = myItems.OrderBy(item => item.Price).ToList();
                    break;
                case "Price Desc":
                    sortedItems = myItems.OrderByDescending(item => item.Price).ToList();
                    break;
                default:
                    sortedItems = myItems;
                    break;
            }

            dataGridView1.DataSource = sortedItems;
            dataGridView2.DataSource = sortedItems;
        }
    }
}
