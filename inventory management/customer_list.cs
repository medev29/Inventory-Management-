using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace inventory_management
{
    public partial class customer_list : UserControl
    {
        public customer_list()
        {
            InitializeComponent();
        }
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\inventory\stock.mdb;Jet OLEDB:Database Password=karaujiya5");
        private void textBox8_Click(object sender, EventArgs e)
        {
            textBox8.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                    OleDbCommand cmd = new OleDbCommand("insert into costomer(cust_code,cust_name,address,ph_no,pan_no) values(" + textBox1.Text + ",'" + textBox2.Text + "','"+textBox3.Text+"','"+textBox4.Text+"','"+textBox5.Text+"')", con);
                    con.Open();
                    int n = cmd.ExecuteNonQuery();
                    if (n > 0)
                    {
                        DialogResult dresult = MessageBox.Show("Add successed", "IMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadgrid();
                        if (dresult == DialogResult.OK)
                        {
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                        }
                    }
        }

        private void loadgrid()
        {
            OleDbDataAdapter ad = new OleDbDataAdapter("select*from costomer", con);
            DataSet ds1 = new DataSet();
            ad.Fill(ds1, "costomer");
            dataGridView1.DataSource = ds1;
            dataGridView1.DataMember = "costomer";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[0].HeaderText = "Code";
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderText = "Customer Name";
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[2].HeaderText = "Address";
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[3].HeaderText = "Phone No";
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[4].HeaderText = "Pan No.";
        }
    }
}
