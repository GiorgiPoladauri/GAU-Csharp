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
                        string updateQuery = @"
                            UPDATE Users 
                            SET 
                                UserBalance = ISNULL(UserBalance, 0) - @PayAmount, 
                                SpentBalance = ISNULL(SpentBalance, 0) + @PayAmount 
                            WHERE ID_P = @ID";

                        SqlCommand cmd = new SqlCommand(updateQuery, _con, transaction);
                        cmd.Parameters.AddWithValue("@PayAmount", PayAmount);
                        cmd.Parameters.AddWithValue("@ID", ID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            transaction.Commit();
                            MessageBox.Show("Transaction successful.");
                        }
                        else
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error: User not found or no update performed.");
                            return;
                        }
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
