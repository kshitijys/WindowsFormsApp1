using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace WindowsFormsApp1
{
    public partial class textForm1 : Form
    {

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;

        public textForm1()
        {
            InitializeComponent();
            string str =
               ConfigurationManager.ConnectionStrings["default connection"].ConnectionString;
               
            conn = new SqlConnection(str);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into studd values(@nm,@roln,@cty,@per)";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@nm", textName.Text);
                cmd.Parameters.AddWithValue("@roln", Convert.ToDecimal(textRollno.Text));
                cmd.Parameters.AddWithValue("@cty", textCity.Text);
                cmd.Parameters.AddWithValue("@per", Convert.ToDecimal(textPercentage.Text));
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update studd set name=@nm ,rollno=@roln,city=@cty,percentage=@per  where id=@id";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@nm", textName.Text);
                cmd.Parameters.AddWithValue("@roln", textRollno.Text);
                cmd.Parameters.AddWithValue("@cty", textCity.Text);
                cmd.Parameters.AddWithValue("@per", Convert.ToDecimal(textPercentage.Text));
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textId.Text));
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from studd where id=@id";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textId.Text));
                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record deleted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from studd where id=@id";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textId.Text));
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        textName.Text = reader["name"].ToString();
                        textRollno.Text = reader["rollno"].ToString();
                        textCity.Text = reader["city"].ToString();
                        textPercentage.Text = reader["percentage"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void btnviewwholetable_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from studd";
                cmd = new SqlCommand(qry, conn);
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                    dataGridView1.DataSource = table;
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void textForm1_Load(object sender, EventArgs e)
        {

        }
    }

       
    
}

