using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepartmensAndUsersProj
{
    public partial class Form1 : Form
    {
        private readonly string _connectionString = "Server=ASUSROGSTRIX15;Database=TransactionsDB;Trusted_Connection=True;";
        private SqlConnection _connection;
        private DataSet _dataSet;
        private SqlDataAdapter _usersAdapter;
        private SqlCommandBuilder _commandBuilder;

        public Form1()
        {
            InitializeComponent();
            _connection = new SqlConnection(_connectionString);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;
            await LoadDepartmentsAsync();
            await LoadUsersAsync();
        }

        private async Task LoadDepartmentsAsync()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    string query = "SELECT DepartmentId, DepartmentName FROM Departments";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    var bindingSource = new BindingSource();
                    bindingSource.DataSource = dt;

                    comboBox1.DataSource = bindingSource;
                    comboBox1.DisplayMember = "DepartmentName";
                    comboBox1.ValueMember = "DepartmentId";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading departments: " + ex.Message);
            }
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                _dataSet = new DataSet();
                await _connection.OpenAsync();

                string query = @"SELECT u.UserId, u.UserName, u.UserBalance, u.SpentBalance, u.DepartmentId, d.DepartmentName 
                         FROM Users u
                         INNER JOIN Departments d ON u.DepartmentId = d.DepartmentId";

                _usersAdapter = new SqlDataAdapter(query, _connection);
                _commandBuilder = new SqlCommandBuilder(_usersAdapter);

                await Task.Run(() => _usersAdapter.Fill(_dataSet, "Users"));

                Invoke(new Action(() => {
                    dataGridView1.DataSource = _dataSet.Tables["Users"];
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Please enter a user name.");
                return;
            }
            if (!decimal.TryParse(textBox3.Text, out decimal userBalance))
            {
                MessageBox.Show("Please enter a valid balance.");
                return;
            }
            int departmentId = (int)comboBox1.SelectedValue;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("AddUser", con, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@UserName", userName);
                                cmd.Parameters.AddWithValue("@UserBalance", userBalance);
                                cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                                await cmd.ExecuteNonQueryAsync();
                            }
                            transaction.Commit();
                            MessageBox.Show("User added successfully.");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error adding user: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error: " + ex.Message);
            }

            await LoadUsersAsync();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int userId))
            {
                MessageBox.Show("Select a valid user from the grid.");
                return;
            }
            string userName = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Please enter a user name.");
                return;
            }
            if (!decimal.TryParse(textBox3.Text, out decimal userBalance))
            {
                MessageBox.Show("Please enter a valid balance.");
                return;
            }
            int departmentId = (int)comboBox1.SelectedValue;

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    using (SqlTransaction transaction = con.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("UpdateUser", con, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@UserId", userId);
                                cmd.Parameters.AddWithValue("@UserName", userName);
                                cmd.Parameters.AddWithValue("@UserBalance", userBalance);
                                cmd.Parameters.AddWithValue("@DepartmentId", departmentId);
                                await cmd.ExecuteNonQueryAsync();
                            }
                            transaction.Commit();
                            MessageBox.Show("User updated successfully.");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error updating user: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error: " + ex.Message);
            }

            await LoadUsersAsync();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int userId))
            {
                MessageBox.Show("Select a valid user from the grid.");
                return;
            }
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("DeleteUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        await cmd.ExecuteNonQueryAsync();
                    }
                    MessageBox.Show("User deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message);
            }

            await LoadUsersAsync();
            textBox1.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_dataSet?.Tables["Users"] == null)
            {
                MessageBox.Show("No data loaded.");
                return;
            }

            decimal threshold = 1000m;
            var query = _dataSet.Tables["Users"].AsEnumerable()
                               .Where(row => row.Field<decimal>("UserBalance") > threshold)
                               .Select(row => new
                               {
                                   UserId = row.Field<int>("UserId"),
                                   UserName = row.Field<string>("UserName"),
                                   Balance = row.Field<decimal>("UserBalance"),
                                   Department = row.Field<string>("DepartmentName")
                               });

            string result = "Users with balance > " + threshold + ":\n" +
                            string.Join("\n", query.Select(q => $"{q.UserId} - {q.UserName} - {q.Balance} ({q.Department})"));
            MessageBox.Show(result);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            textBox1.Text = row.Cells["UserId"].Value.ToString();
            textBox2.Text = row.Cells["UserName"].Value.ToString();
            textBox3.Text = row.Cells["UserBalance"].Value.ToString();

            if (row.Cells["DepartmentId"].Value != DBNull.Value)
                comboBox1.SelectedValue = Convert.ToInt32(row.Cells["DepartmentId"].Value);
        }
    }
}
