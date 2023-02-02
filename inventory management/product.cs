using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using Microsoft.VisualBasic;
using System.Collections;
namespace inventory_management
{
    public partial class product : UserControl
    {
        public product()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\inventory\stock.mdb;Jet OLEDB:Database Password=karaujiya5");
        int cd = 0;
        string title = "IMS";
        OleDbDataAdapter adp1;
        DataSet datas;

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" && textBox2.Text == "")
                {
                    MessageBox.Show("Please Input Values", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    OleDbCommand cmd = new OleDbCommand("insert into manu_f(code,f_name) values(" + textBox1.Text + ",'" + textBox2.Text + "')", con);
                    con.Open();
                    int n = cmd.ExecuteNonQuery();
                    if (n > 0)
                    {
                        DialogResult dresult = MessageBox.Show("Add successed", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadgrid();
                        if (dresult == DialogResult.OK)
                        {
                            textBox1.Text = "";
                            textBox2.Text = "";
                        }

                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void loadgrid()
        {
            OleDbDataAdapter ad = new OleDbDataAdapter("select*from manu_f", con);
            DataSet ds1 = new DataSet();
            ad.Fill(ds1, "manu_f");
            dataGridView1.DataSource = ds1;
            dataGridView1.DataMember = "manu_f";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[1].Width = 190;
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[1].HeaderText = "Manufacture Name";

        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                OleDbDataAdapter adb = new OleDbDataAdapter("select * from manu_f", con);
                adb.Fill(ds, "manu_f");
                ds.Tables[0].Constraints.Add("pk_code", ds.Tables[0].Columns[0], true);

                long n = Convert.ToInt64(Interaction.InputBox("Enter Code: ....", "@Design by ..", "20", 100, 100));

                DataRow drow = ds.Tables[0].Rows.Find(n);
                if (drow != null)
                {
                    cd = ds.Tables[0].Rows.IndexOf(drow);
                    textBox1.Text = drow[0].ToString();
                    textBox2.Text = drow[1].ToString();


                }
                else
                {
                    MessageBox.Show("Sorry Cann't find data");
                }
            }
            catch (Exception tt)
            {
                MessageBox.Show(tt.Message);
            }


        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                OleDbDataAdapter ad = new OleDbDataAdapter("select*from manu_f", con);
                DataSet ds1 = new DataSet();
                ad.Fill(ds1, "manu_f");
                dataGridView1.DataSource = ds1;
                dataGridView1.DataMember = "manu_f";
                this.dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[1].Width = 190;
                dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[1].HeaderText = "Manufacture Name";


            }
            else
            {
                string name = textBox8.Text;
                string sql = "select * from manu_f where f_name like '" + name + "%'";
                OleDbDataAdapter ad = new OleDbDataAdapter(sql, con);
                DataSet ds = new DataSet();
                ad.Fill(ds, "manu_f");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "manu_f";
                this.dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[1].Width = 190;
                dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[1].HeaderText = "Manufacture Name";

            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" && textBox2.Text == "")
                {
                    MessageBox.Show("Please Check Code & Manufacture Name", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    OleDbCommand cmd = new OleDbCommand("update manu_f set f_name= '" + textBox2.Text + "' where code=" + textBox1.Text + "", con);
                    con.Open();
                    int updat = cmd.ExecuteNonQuery();
                    if (updat > 0)
                    {
                        MessageBox.Show("update successed");
                    }
                    else
                    {
                        MessageBox.Show("Sorry......Try Again");
                    }
                }
            }
            catch (Exception uu)
            {
                MessageBox.Show(uu.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand("delete from manu_f where code=" + textBox1.Text + "", con);
                con.Open();
                int delete = cmd.ExecuteNonQuery();
                if (delete > 0)
                {
                    MessageBox.Show("Delete Successful", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
                else
                {
                    MessageBox.Show("Sorry....! Try Again");
                }
            }
            catch (Exception tt)
            {
                MessageBox.Show(tt.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void textBox8_Click(object sender, EventArgs e)
        {
            textBox8.Clear();
        }

        private void textBox9_Click(object sender, EventArgs e)
        {
            textBox9.Clear();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text == "")
                {
                    MessageBox.Show("Please Input the Value", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    bool status = false;
                    OleDbCommand cm1 = new OleDbCommand("select * from manu_type where type='" + textBox4.Text + "'", con);
                    con.Open();
                    OleDbDataReader rd = cm1.ExecuteReader();
                    while (rd.Read())
                    {
                        status = true;

                    }

                    if (status == true)
                    {
                        MessageBox.Show("Already, Data Has Saved", title, MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }

                    else
                    {

                        OleDbCommand cmd = new OleDbCommand("insert into manu_type  (type) values('" + textBox4.Text + "')", con);


                        int type = cmd.ExecuteNonQuery();
                        if (type > 0)
                        {
                            DialogResult result1 = MessageBox.Show("Add Successed", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadtype();
                            if (result1 == DialogResult.OK)
                            {
                                textBox4.Text = "";

                            }

                        }



                    }


                }
            }
            catch (Exception yy)
            {

                MessageBox.Show(yy.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void loadtype()
        {
            OleDbDataAdapter ad = new OleDbDataAdapter("select*from manu_type", con);
            DataSet ds1 = new DataSet();
            ad.Fill(ds1, "manu_type");
            dataGridView2.DataSource = ds1;
            dataGridView2.DataMember = "manu_type";
            this.dataGridView2.RowHeadersVisible = false;
            dataGridView2.Columns[0].Width = 50;
            dataGridView2.Columns[0].HeaderText = "S.N";
            dataGridView2.Columns[1].Width = 200;
            dataGridView2.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.Columns[1].HeaderText = "Manufacture Type";
            dataGridView2.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                OleDbDataAdapter ad = new OleDbDataAdapter("select*from manu_type", con);
                DataSet ds1 = new DataSet();
                ad.Fill(ds1, "manu_type");
                dataGridView2.DataSource = ds1;
                dataGridView2.DataMember = "manu_type";
                this.dataGridView2.RowHeadersVisible = false;
                dataGridView2.Columns[0].Width = 50;
                dataGridView2.Columns[0].HeaderText = "S.N";
                dataGridView2.Columns[1].Width = 200;
                dataGridView2.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView2.Columns[1].HeaderText = "Manufacture Type";
                dataGridView2.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {

                OleDbDataAdapter ad = new OleDbDataAdapter("select*from manu_type where type like '" + textBox9.Text + "%'", con);
                DataSet ds1 = new DataSet();
                ad.Fill(ds1, "manu_type");
                dataGridView2.DataSource = ds1;
                dataGridView2.DataMember = "manu_type";
                this.dataGridView2.RowHeadersVisible = false;
                dataGridView2.Columns[0].Width = 50;
                dataGridView2.Columns[0].HeaderText = "S.N";

                dataGridView2.Columns[1].Width = 200;
                dataGridView2.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView2.Columns[1].HeaderText = "Manufacture Type";
                dataGridView2.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand("delete * from manu_type where sn=" + textBox10.Text + "", con);
                con.Open();
                int dlt = cmd.ExecuteNonQuery();
                if (dlt > 0)
                {
                    MessageBox.Show("Delete successful", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox4.Text = "";
                    textBox10.Text = "";
                }
            }
            catch (Exception uu)
            {
                MessageBox.Show(uu.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text == "" && textBox10.Text == "")
                {
                    MessageBox.Show("Please Input Values", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    OleDbCommand cmd = new OleDbCommand("update manu_type set type='" + textBox4.Text + "' where sn=" + textBox10.Text + "", con);
                    con.Open();
                    int upt = cmd.ExecuteNonQuery();
                    if (upt > 0)
                    {
                        MessageBox.Show("Update Successed", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox10.Text = "";
                        textBox4.Text = "";
                    }
                }
            }
            catch (Exception pp)
            {
                MessageBox.Show(pp.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            adp1 = new OleDbDataAdapter("select * from manu_type", con);
            datas = new DataSet();
            adp1.Fill(datas, "manu_type");
            datas.Tables[0].Constraints.Add("pk_sn", datas.Tables[0].Columns[0], true);
            long n = Convert.ToInt64(Interaction.InputBox("Enter S.N.: ....", "@Design by ..", "20", 100, 100));
            DataRow drow = datas.Tables[0].Rows.Find(n);
            if (drow != null)
            {
                cd = datas.Tables[0].Rows.IndexOf(drow);
                textBox10.Text = drow[0].ToString();
                textBox4.Text = drow[1].ToString();

            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox3.Text == "")
                {
                    MessageBox.Show("Please Input Value", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    bool status = false;
                    OleDbCommand cm1 = new OleDbCommand("select * from manu_class where class='" + textBox3.Text + "'", con);
                    con.Open();
                    OleDbDataReader rd = cm1.ExecuteReader();
                    while (rd.Read())
                    {
                        status = true;

                    }

                    if (status == true)
                    {
                        MessageBox.Show("Already, Data Has Saved", title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {

                        OleDbCommand cmd = new OleDbCommand("insert into manu_class  (class) values('" + textBox3.Text + "')", con);

                        int type = cmd.ExecuteNonQuery();
                        if (type > 0)
                        {
                            DialogResult result1 = MessageBox.Show("Add Successed", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadclass();
                            if (result1 == DialogResult.OK)
                            {
                                textBox3.Text = "";

                            }
                        }
                    }
                }
            }
            catch (Exception oo)
            {
                MessageBox.Show(oo.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void loadclass()
        {
            OleDbDataAdapter ad = new OleDbDataAdapter("select*from manu_class", con);
            DataSet ds1 = new DataSet();
            ad.Fill(ds1, "manu_class");
            dataGridView3.DataSource = ds1;
            dataGridView3.DataMember = "manu_class";
            this.dataGridView3.RowHeadersVisible = false;
            dataGridView3.Columns[0].Width = 50;
            dataGridView3.Columns[0].HeaderText = "S.N";
            dataGridView3.Columns[1].Width = 200;
            dataGridView3.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView3.Columns[1].HeaderText = "Manufacture class";
            dataGridView3.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void textBox12_Click(object sender, EventArgs e)
        {
            textBox12.Clear();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true)
            {
                OleDbDataAdapter ad = new OleDbDataAdapter("select*from manu_class", con);
                DataSet ds1 = new DataSet();
                ad.Fill(ds1, "manu_class");
                dataGridView3.DataSource = ds1;
                dataGridView3.DataMember = "manu_class";
                this.dataGridView3.RowHeadersVisible = false;
                dataGridView3.Columns[0].Width = 50;
                dataGridView3.Columns[0].HeaderText = "S.N";
                dataGridView3.Columns[1].Width = 200;
                dataGridView3.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView3.Columns[1].HeaderText = "Manufacture class";
                dataGridView3.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {

                OleDbDataAdapter ad = new OleDbDataAdapter("select * from manu_class where class like '" + textBox12.Text + "%'", con);
                DataSet ds1 = new DataSet();
                ad.Fill(ds1, "manu_class");
                dataGridView3.DataSource = ds1;
                dataGridView3.DataMember = "manu_class";
                this.dataGridView3.RowHeadersVisible = false;
                dataGridView3.Columns[0].Width = 50;
                dataGridView3.Columns[0].HeaderText = "S.N";

                dataGridView3.Columns[1].Width = 200;
                dataGridView3.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView3.Columns[1].HeaderText = "Manufacture class";
                dataGridView3.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                adp1 = new OleDbDataAdapter("select * from manu_class", con);
                datas = new DataSet();
                adp1.Fill(datas, "manu_class");
                datas.Tables[0].Constraints.Add("pk_sn", datas.Tables[0].Columns[0], true);
                long n = Convert.ToInt64(Interaction.InputBox("Enter S.N.: ....", "@Design by ..", "20", 100, 100));
                DataRow drow = datas.Tables[0].Rows.Find(n);
                if (drow != null)
                {
                    cd = datas.Tables[0].Rows.IndexOf(drow);
                    textBox11.Text = drow[0].ToString();
                    textBox3.Text = drow[1].ToString();

                }
            }
            catch (Exception mm)
            {
                MessageBox.Show(mm.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox11.Text == "" && textBox3.Text == "")
                {
                    MessageBox.Show("Please Input Values", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    OleDbCommand cmd = new OleDbCommand("update manu_class set class='" + textBox3.Text + "' where sn=" + textBox11.Text + "", con);
                    con.Open();
                    int upt = cmd.ExecuteNonQuery();
                    if (upt > 0)
                    {
                        MessageBox.Show("Update Successed", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox3.Text = "";
                        textBox11.Text = "";
                    }
                }
            }
            catch (Exception kk)
            {
                MessageBox.Show(kk.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand("delete * from manu_class where sn=" + textBox11.Text + "", con);
                con.Open();
                int dlt = cmd.ExecuteNonQuery();
                if (dlt > 0)
                {
                    MessageBox.Show("Delete successful", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox3.Text = "";
                    textBox11.Text = "";
                }
            }
            catch (Exception ll)
            {
                MessageBox.Show(ll.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text == "")
                {
                    MessageBox.Show("Pease Input Value", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    bool status = false;
                    OleDbCommand cm1 = new OleDbCommand("select * from extra where class='" + textBox5.Text + "'", con);
                    con.Open();
                    OleDbDataReader rd = cm1.ExecuteReader();
                    while (rd.Read())
                    {
                        status = true;

                    }

                    if (status == true)
                    {
                        MessageBox.Show("Already, Data Has Saved", title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {

                        OleDbCommand cmd = new OleDbCommand("insert into extra  (class) values('" + textBox5.Text + "')", con);

                        int type = cmd.ExecuteNonQuery();
                        if (type > 0)
                        {
                            DialogResult result1 = MessageBox.Show("Add Successed", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadextra();
                            if (result1 == DialogResult.OK)
                            {
                                textBox5.Text = "";

                            }
                        }
                    }
                }
            }
            catch (Exception oo)
            {
                MessageBox.Show(oo.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void loadextra()
        {
            OleDbDataAdapter ad = new OleDbDataAdapter("select * from extra", con);
            DataSet ds1 = new DataSet();
            ad.Fill(ds1, "extra");
            dataGridView4.DataSource = ds1;
            dataGridView4.DataMember = "extra";
            this.dataGridView4.RowHeadersVisible = false;
            dataGridView4.Columns[0].Width = 50;
            dataGridView4.Columns[0].HeaderText = "S.N";
            dataGridView4.Columns[1].Width = 200;
            dataGridView4.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView4.Columns[1].HeaderText = "Manufacture Extra";
            dataGridView4.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void textBox14_Click(object sender, EventArgs e)
        {
            textBox14.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                adp1 = new OleDbDataAdapter("select * from extra ", con);
                datas = new DataSet();
                adp1.Fill(datas, "extra");
                datas.Tables[0].Constraints.Add("pk_sn", datas.Tables[0].Columns[0], true);
                long n = Convert.ToInt64(Interaction.InputBox("Enter S.N.: ....", "@Design by ..", "20", 100, 100));
                DataRow drow = datas.Tables[0].Rows.Find(n);
                if (drow != null)
                {
                    cd = datas.Tables[0].Rows.IndexOf(drow);
                    textBox13.Text = drow[0].ToString();
                    textBox5.Text = drow[1].ToString();

                }
            }
            catch (Exception jj)
            {
                MessageBox.Show(jj.Message);
            }

        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (radioButton8.Checked == true)
            {
                OleDbDataAdapter ad = new OleDbDataAdapter("select * from extra", con);
                DataSet ds1 = new DataSet();
                ad.Fill(ds1, "extra");
                dataGridView4.DataSource = ds1;
                dataGridView4.DataMember = "extra";
                this.dataGridView4.RowHeadersVisible = false;
                dataGridView4.Columns[0].Width = 50;
                dataGridView4.Columns[0].HeaderText = "S.N";
                dataGridView4.Columns[1].Width = 200;
                dataGridView4.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView4.Columns[1].HeaderText = "Manufacture Extra";
                dataGridView4.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {
                OleDbDataAdapter ad = new OleDbDataAdapter("select * from extra where class like '" + textBox14.Text + "%'", con);
                DataSet ds1 = new DataSet();
                ad.Fill(ds1, "extra");
                dataGridView4.DataSource = ds1;
                dataGridView4.DataMember = "extra";
                this.dataGridView4.RowHeadersVisible = false;
                dataGridView4.Columns[0].Width = 50;
                dataGridView4.Columns[0].HeaderText = "S.N";
                dataGridView4.Columns[1].Width = 200;
                dataGridView4.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView4.Columns[1].HeaderText = "Manufacture Extra";
                dataGridView4.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox13.Text == "" && textBox5.Text == "")
                {
                    MessageBox.Show("Please Input Values", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    OleDbCommand cmd = new OleDbCommand("update extra set class='" + textBox5.Text + "' where sn=" + textBox13.Text + "", con);
                    con.Open();
                    int upt = cmd.ExecuteNonQuery();
                    if (upt > 0)
                    {
                        MessageBox.Show("Update Successed", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox5.Text = "";
                        textBox13.Text = "";
                    }
                }
            }

            catch (Exception pp)
            {
                MessageBox.Show(pp.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand("delete * from extra where sn=" + textBox13.Text + "", con);
                con.Open();
                int dlt = cmd.ExecuteNonQuery();
                if (dlt > 0)
                {
                    MessageBox.Show("Delete successful", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox5.Text = "";
                    textBox13.Text = "";
                }
            }
            catch (Exception kk)
            {
                MessageBox.Show(kk.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox6.Text == "" && textBox7.Text == "")
                {
                    MessageBox.Show("Please Input Values", title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    bool status = false;
                    OleDbCommand cm1 = new OleDbCommand("select * from discount  where code=" + textBox7.Text + "", con);
                    con.Open();
                    OleDbDataReader rd = cm1.ExecuteReader();
                    while (rd.Read())
                    {
                        status = true;

                    }

                    if (status == true)
                    {
                        MessageBox.Show("Already, Data Has Saved", title, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    OleDbCommand cmd = new OleDbCommand("insert into discount (code,discount) values(" + textBox7.Text + "," + textBox6.Text + ")", con);

                    int type = cmd.ExecuteNonQuery();

                    if (type > 0)
                    {
                        DialogResult result1 = MessageBox.Show("Add Successed", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadedic();
                        if (result1 == DialogResult.OK)
                        {
                            textBox6.Text = "";
                            textBox7.Text = "";

                        }
                    }
                }
            }
            catch (Exception ww)
            {
                MessageBox.Show(ww.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void loadedic()
        {
            OleDbDataAdapter ad = new OleDbDataAdapter("select * from discount", con);
            DataSet ds1 = new DataSet();
            ad.Fill(ds1, "discount");
            dataGridView5.DataSource = ds1;
            dataGridView5.DataMember = "discount";
            this.dataGridView5.RowHeadersVisible = false;
            dataGridView5.Columns[0].Width = 50;
            dataGridView5.Columns[0].HeaderText = "S.N";
            dataGridView5.Columns[1].Width = 200;
            dataGridView5.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView5.Columns[1].HeaderText = "Descount Percentage ";
            dataGridView5.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                adp1 = new OleDbDataAdapter("select * from discount", con);
                datas = new DataSet();
                adp1.Fill(datas, "discount");
                datas.Tables[0].Constraints.Add("pk_code", datas.Tables[0].Columns[0], true);
                long n = Convert.ToInt64(Interaction.InputBox("Enter Code: ....", "@Design by ..", "20", 100, 100));
                DataRow drow = datas.Tables[0].Rows.Find(n);
                if (drow != null)
                {
                    cd = datas.Tables[0].Rows.IndexOf(drow);
                    textBox7.Text = drow[0].ToString();
                    textBox6.Text = drow[1].ToString();
                }
            }
            catch (Exception qq)
            {
                MessageBox.Show(qq.Message);
            }


        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox7.Text == "" && textBox6.Text == "")
                {
                    MessageBox.Show("Please Input Values", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    OleDbCommand cmd = new OleDbCommand("update discount set discount=" + textBox6.Text + " where code=" + textBox7.Text + "", con);
                    con.Open();
                    int upt = cmd.ExecuteNonQuery();
                    if (upt > 0)
                    {
                        MessageBox.Show("Update Successed", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox7.Text = "";
                        textBox6.Text = "";
                    }
                }
            }
            catch (Exception aa)
            {
                MessageBox.Show(aa.Message);

            }
            finally
            {
                con.Close();
            }

        }

        private void button26_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton10.Checked == true)
                {
                    OleDbDataAdapter ad = new OleDbDataAdapter("select * from discount", con);
                    DataSet ds1 = new DataSet();
                    ad.Fill(ds1, "discount");
                    dataGridView5.DataSource = ds1;
                    dataGridView5.DataMember = "discount";
                    this.dataGridView5.RowHeadersVisible = false;
                    dataGridView5.Columns[0].Width = 50;
                    dataGridView5.Columns[0].HeaderText = "Code";
                    dataGridView5.Columns[1].Width = 200;
                    dataGridView5.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView5.Columns[1].HeaderText = "Discount Percentage ";
                    dataGridView5.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                else
                {
                    OleDbDataAdapter ad = new OleDbDataAdapter("select * from discount where code=" + textBox15.Text + "", con);
                    DataSet ds1 = new DataSet();
                    ad.Fill(ds1, "discount");
                    dataGridView5.DataSource = ds1;
                    dataGridView5.DataMember = "discount";
                    this.dataGridView5.RowHeadersVisible = false;
                    dataGridView5.Columns[0].Width = 50;
                    dataGridView5.Columns[0].HeaderText = "Code";
                    dataGridView5.Columns[1].Width = 200;
                    dataGridView5.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView5.Columns[1].HeaderText = "Discount Percentage ";
                    dataGridView5.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception jj)
            {
                MessageBox.Show(jj.Message);
            }
        }

        private void textBox15_Click(object sender, EventArgs e)
        {
            textBox15.Clear();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand("delete * from discount where code=" + textBox7.Text + "", con);
                con.Open();
                int dlt = cmd.ExecuteNonQuery();
                if (dlt > 0)
                {
                    MessageBox.Show("Delete successful", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox7.Text = "";
                    textBox6.Text = "";
                }
            }
            catch (Exception jj)
            {
                MessageBox.Show(jj.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox18.Text == "")
                {
                    MessageBox.Show("Pease Input Value", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    bool status = false;
                    OleDbCommand cm1 = new OleDbCommand("select * from unit where unit='" + textBox18.Text + "'", con);
                    con.Open();
                    OleDbDataReader rd = cm1.ExecuteReader();
                    while (rd.Read())
                    {
                        status = true;

                    }
                    if (status == true)
                    {
                        MessageBox.Show("Already, Data Has Saved", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        OleDbCommand cmd = new OleDbCommand("insert into unit  (unit) values('" + textBox18.Text + "')", con);
                        int type = cmd.ExecuteNonQuery();
                        if (type > 0)
                        {
                            DialogResult result1 = MessageBox.Show("Add Successed", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadunit();
                            if (result1 == DialogResult.OK)
                            {
                                textBox18.Text = "";

                            }
                        }
                    }
                }
            }
            catch (Exception oo)
            {
                MessageBox.Show(oo.Message);
            }
            finally
            {
                con.Close();
            }
            
        }

        private void loadunit()
        {
            OleDbDataAdapter ad = new OleDbDataAdapter("select * from unit", con);
            DataSet ds1 = new DataSet();
            ad.Fill(ds1, "unit");
            dataGridView6.DataSource = ds1;
            dataGridView6.DataMember = "unit";
            this.dataGridView6.RowHeadersVisible = false;
            dataGridView6.Columns[0].Width = 50;
            dataGridView6.Columns[0].HeaderText = "S.N";
            dataGridView6.Columns[1].Width = 200;
            dataGridView6.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView6.Columns[1].HeaderText = "Manufacture Unit";
            dataGridView6.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void textBox16_Click(object sender, EventArgs e)
        {
            textBox16.Clear();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (radioButton12.Checked == true)
            {
                OleDbDataAdapter ad = new OleDbDataAdapter("select * from unit", con);
                DataSet ds1 = new DataSet();
                ad.Fill(ds1, "unit");
                dataGridView6.DataSource = ds1;
                dataGridView6.DataMember = "unit";
                this.dataGridView6.RowHeadersVisible = false;
                dataGridView6.Columns[0].Width = 50;
                dataGridView6.Columns[0].HeaderText = "S.N";
                dataGridView6.Columns[1].Width = 200;
                dataGridView6.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView6.Columns[1].HeaderText = "Manufacture Unit";
                dataGridView6.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            else
            {
                OleDbDataAdapter ad = new OleDbDataAdapter("select * from unit where unit like '"+textBox16.Text+"%'", con);
                DataSet ds1 = new DataSet();
                ad.Fill(ds1, "unit");
                dataGridView6.DataSource = ds1;
                dataGridView6.DataMember = "unit";
                this.dataGridView6.RowHeadersVisible = false;
                dataGridView6.Columns[0].Width = 50;
                dataGridView6.Columns[0].HeaderText = "S.N";
                dataGridView6.Columns[1].Width = 200;
                dataGridView6.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView6.Columns[1].HeaderText = "Manufacture Unit";
                dataGridView6.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {

            try
            {
                adp1 = new OleDbDataAdapter("select * from unit", con);
                datas = new DataSet();
                adp1.Fill(datas, "unit");
                datas.Tables[0].Constraints.Add("pk_sn", datas.Tables[0].Columns[0], true);
                long n = Convert.ToInt64(Interaction.InputBox("Enter S.N.: ....", "@Design by ..", "20", 100, 100));
                DataRow drow = datas.Tables[0].Rows.Find(n);
                if (drow != null)
                {
                    cd = datas.Tables[0].Rows.IndexOf(drow);
                    textBox17.Text = drow[0].ToString();
                    textBox18.Text = drow[1].ToString();

                }
            }
            catch (Exception mm)
            {
                MessageBox.Show(mm.Message);
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox17.Text == "" && textBox18.Text == "")
                {
                    MessageBox.Show("Please Input Values", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    OleDbCommand cmd = new OleDbCommand("update unit set unit='" + textBox18.Text + "' where sn=" + textBox17.Text + "", con);
                    con.Open();
                    int upt = cmd.ExecuteNonQuery();
                    if (upt > 0)
                    {
                        MessageBox.Show("Update Successed", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox17.Text = "";
                        textBox18.Text = "";
                    }
                }
            }

            catch (Exception pp)
            {
                MessageBox.Show(pp.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            try
            {

                if (textBox17.Text == "" && textBox18.Text == "")
                {
                    MessageBox.Show("Please Input Values", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    OleDbCommand cmd = new OleDbCommand("delete * from unit where sn=" + textBox17.Text + "", con);
                    con.Open();
                    int dlt = cmd.ExecuteNonQuery();
                    if (dlt > 0)
                    {
                        MessageBox.Show("Delete successful", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox17.Text = "";
                        textBox18.Text = "";
                    }
                }
            }
            catch (Exception kk)
            {
                MessageBox.Show(kk.Message);
            }
            finally
            {
                con.Close();
            }
        }

    }
}