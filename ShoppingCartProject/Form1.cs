using System;
using System.Windows.Forms;

namespace ShoppingCartProject
{
    public partial class Form1 : Form
    {
        private ShoppingCart cart;

        public Form1()
        {
            InitializeComponent();
            cart = new ShoppingCart();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("ProductName", "Product Name");
            dataGridView1.Columns.Add("Price", "Price");
            dataGridView1.Columns.Add("Quantity", "Quantity");
            dataGridView1.Columns.Add("Total", "Total");
        }

        private void UpdateCartDetails()
        {
            dataGridView1.Rows.Clear();

            foreach (var product in cart.Products)
            {
                dataGridView1.Rows.Add(product.Name, product.Price, product.Quantity, product.GetTotalPrice());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            decimal price;
            int quantity;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a product name.", "Invalid Input");
                return;
            }
            if (!decimal.TryParse(textBox3.Text, out price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid price.", "Invalid Input");
                return;
            }
            if (!int.TryParse(textBox2.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Invalid Input");
                return;
            }

            Product product = new Product(name, price, quantity);
            cart.AddToCart(product);

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();

            UpdateCartDetails();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decimal totalAmount = cart.GetTotalAmount();

            MessageBox.Show($"Total Amount: {totalAmount:C}", "Order Summary");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    int rowIndex = selectedRow.Index;

                    if (rowIndex >= 0 && rowIndex < cart.Products.Count)
                    {
                        cart.Products.RemoveAt(rowIndex);

                        dataGridView1.Rows.Remove(selectedRow);

                        MessageBox.Show("Row deleted successfully!", "Success");
                    }
                    else
                    {
                        MessageBox.Show("Invalid row index. Unable to delete.", "Error");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.", "No Selection");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error");
            }
        }
    }
}
