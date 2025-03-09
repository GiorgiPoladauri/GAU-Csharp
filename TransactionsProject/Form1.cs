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

namespace TransactionsProject
{
    public partial class Form1 : Form
    {
        private string _connectionString = "Server=ASUSROGSTRIX15;Database=TransactionsDB;Trusted_Connection=True;";
        private SqlConnection _con;
        public Form1()
        {
            InitializeComponent();
            _con = new SqlConnection(_connectionString);
            LoadCourses();
        }

        private void LoadCourses()
        {
            try
            {
                string query = "SELECT ID_P, UserName, UserBalance, SpentBalance FROM [TransactionsDB].[dbo].[Users]";
                SqlDataAdapter da = new SqlDataAdapter(query, _con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Users");

                if (ds.Tables["Users"].Rows.Count > 0)
                {
                    dataGridView1.DataSource = ds.Tables["Users"];

                    dataGridView1.Columns["ID_P"].HeaderText = "User ID";
                    dataGridView1.Columns["UserName"].HeaderText = "User Name";
                    dataGridView1.Columns["UserBalance"].HeaderText = "User Balance";
                    dataGridView1.Columns["SpentBalance"].HeaderText = "Spent Balance";
                }
                else
                {
                    MessageBox.Show("No users found in the database.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ID = textBox1.Text;
                if (string.IsNullOrEmpty(ID))
                {
                    MessageBox.Show("Please enter a valid User ID.");
                    return;
                }

                if (!Decimal.TryParse(textBox2.Text, out decimal PayAmount) || PayAmount <= 0)
                {
                    MessageBox.Show("Please enter a valid amount greater than zero.");
                    return;
                }

                _con.Open();

                using (SqlTransaction transaction = _con.BeginTransaction())
                {
                    try
                    {
                        string selectBalanceQuery = "SELECT UserBalance FROM Users WHERE ID_P = @ID";
                        SqlCommand cmdSelect = new SqlCommand(selectBalanceQuery, _con, transaction);
                        cmdSelect.Parameters.AddWithValue("@ID", ID);

                        object balanceObj = cmdSelect.ExecuteScalar();

                        if (balanceObj == null || balanceObj == DBNull.Value)
                        {
                            throw new Exception("Error: User not found.");
                        }

                        decimal UserBalance = Convert.ToDecimal(balanceObj);

                        if (UserBalance < PayAmount)
                        {
                            throw new Exception("Error: You don't have enough money.");
                        }

                        string updateBalanceQuery = "UPDATE Users SET UserBalance = UserBalance - @PayAmount WHERE ID_P = @ID";
                        SqlCommand cmd1 = new SqlCommand(updateBalanceQuery, _con, transaction);
                        cmd1.Parameters.AddWithValue("@PayAmount", PayAmount);
                        cmd1.Parameters.AddWithValue("@ID", ID);

                        int rowsAffected1 = cmd1.ExecuteNonQuery();

                        if (rowsAffected1 == 0)
                        {
                            throw new Exception("Error: Balance update failed.");
                        }

                        string updateSpentQuery = "UPDATE Users SET SpentBalance = ISNULL(SpentBalance, 0) + @PayAmount WHERE ID_P = @ID";
                        SqlCommand cmd2 = new SqlCommand(updateSpentQuery, _con, transaction);
                        cmd2.Parameters.AddWithValue("@PayAmount", PayAmount);
                        cmd2.Parameters.AddWithValue("@ID", ID);

                        int rowsAffected2 = cmd2.ExecuteNonQuery();

                        if (rowsAffected2 == 0)
                        {
                            throw new Exception("Error: Spent balance update failed.");
                        }

                        transaction.Commit();
                        MessageBox.Show("Transaction successful.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error: " + ex.Message);
                        return;
                    }
                }
                LoadCourses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Error: " + ex.Message);
            }
            finally
            {
                _con.Close();
            }
        }
    }
}
