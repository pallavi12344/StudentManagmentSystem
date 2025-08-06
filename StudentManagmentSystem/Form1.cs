using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace StudentManagmentSystem
{
    public partial class Form1 : Form
    {
        string connStr;
        public Form1()
        {
            InitializeComponent(); 
            connStr = ConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null; // Removes initial focus
        }
       
        private void txtBox_Enter(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }
        private void LoadStudents()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("Select * from Students", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvStudents.DataSource = dt;
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con =new SqlConnection(connStr))
            {
                string query = "Insert into students (Name ,age,course)values(@name,@age,@course)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("age", txtAge.Text);
                cmd.Parameters.AddWithValue("@course", txtCourse.Text);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            LoadStudents();
            ClearTextBoxs();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["Id"].Value);

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    string query = "update students set name=@name,age=@age,course=@course where id=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@age", txtAge.Text);
                    cmd.Parameters.AddWithValue("@course", txtCourse.Text);
                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();

                }
                LoadStudents();
                //ClearTextBoxs();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadStudents();
            ClearTextBoxs();
        }

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0)
            {
                DataGridViewRow row = dgvStudents.Rows[e.RowIndex];
                txtName.Text = row.Cells["Names"].Value.ToString();
                txtAge.Text = row.Cells["Age"].Value.ToString();
                txtCourse.Text = row.Cells["Course"].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["Id"].Value);

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    string query = "DELETE FROM Students WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadStudents();
            }
        }
        private void ClearTextBoxs()
        {
            txtName.Clear();
            txtAge.Clear();
            txtCourse.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
