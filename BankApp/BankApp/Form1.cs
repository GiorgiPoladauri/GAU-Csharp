using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApp
{
    public partial class Form1 : Form
    {
        private string ConnectionString = "Server=GIGABYTE-ULTRA\\SQLEXPRESS01;Database=BankApp;Trusted_Connection=True;";
        private SqlConnection Con;

        public Form1()
        {
            InitializeComponent();
            Con = new SqlConnection(ConnectionString);
            LoadData();
        }

        private async void LoadData()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("usp_SelectAllUsers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private async Task InsertUser(string UserName, DateTime BirthDate, decimal LoanAmount, decimal LoanInterest)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("usp_InsertUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
                    cmd.Parameters.AddWithValue("@LoanAmount", LoanAmount);
                    cmd.Parameters.AddWithValue("@LoanInterest", LoanInterest);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task UpdateUser(int ID_P, string UserName, DateTime BirthDate, decimal LoanAmount, decimal LoanInterest)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("usp_UpdateUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_P", ID_P);
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
                    cmd.Parameters.AddWithValue("@LoanAmount", LoanAmount);
                    cmd.Parameters.AddWithValue("@LoanInterest", LoanInterest);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task DeleteUser(int ID_P)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("usp_DeleteUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_P", ID_P);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await InsertUser(textBox1.Text, dateTimePicker1.Value, decimal.Parse(textBox2.Text), decimal.Parse(textBox3.Text));
            LoadData();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_P"].Value);
                await DeleteUser(id);
                LoadData();
            }
            else
            {
                MessageBox.Show("Select a row to delete.");
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Select a row to update.");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID_P"].Value);

            string userName = textBox1.Text;

            DateTime birthDate = dateTimePicker1.Value;

            decimal loanAmount;
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                loanAmount = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["LoanAmount"].Value);
            }
            else if (!decimal.TryParse(textBox2.Text, out loanAmount))
            {
                MessageBox.Show("Loan Amount is not a valid number.");
                return;
            }

            decimal loanInterest;
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                loanInterest = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["LoanInterest"].Value);
            }
            else if (!decimal.TryParse(textBox3.Text, out loanInterest))
            {
                MessageBox.Show("Loan Interest is not a valid number.");
                return;
            }

            await UpdateUser(id, userName, birthDate, loanAmount, loanInterest);

            LoadData();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["UserName"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["BirthDate"].Value);
                textBox2.Text = row.Cells["LoanAmount"].Value.ToString();
                textBox3.Text = row.Cells["LoanInterest"].Value.ToString();
            }
        }
    }
}
