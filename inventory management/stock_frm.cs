using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data.OleDb;
using Microsoft.VisualBasic;

namespace inventory_management
{
    public partial class stock_frm : UserControl
    {
        public stock_frm()
        {
            InitializeComponent();
        }

        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\inventory\stock.mdb;Jet OLEDB:Database Password=karaujiya5");
        OleDbCommand cmd;
        string title = "IMS";

        private void stock_frm_Load(object sender, EventArgs e)
        {
                     
           
            auto_number();
            p_code_display();
            unit_display();
            manu_type();
            class_display();
            extra_display();
        }

        private void extra_display()
        {
            try
            {
                AutoCompleteStringCollection suggestion = new AutoCompleteStringCollection();
                cmd = new OleDbCommand("select * from extra", con);

                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                ArrayList arlist = new ArrayList();

                while (rdr.Read())
                {
                    arlist.Add(rdr[1].ToString());
                }
                for (int i = 0; i < arlist.Count; i++)
                {
                    suggestion.Add(arlist[i].ToString());
                }

                con.Close();
                e_feature.AutoCompleteSource = AutoCompleteSource.CustomSource;
                e_feature.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                e_feature.AutoCompleteCustomSource = suggestion;
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

        private void class_display()
        {
            try
            {
                AutoCompleteStringCollection suggestion = new AutoCompleteStringCollection();
                cmd = new OleDbCommand("select * from manu_class", con);

                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                ArrayList arlist = new ArrayList();

                while (rdr.Read())
                {
                    arlist.Add(rdr[1].ToString());
                }
                for (int i = 0; i < arlist.Count; i++)
                {
                    suggestion.Add(arlist[i].ToString());
                }

                con.Close();
                p_class.AutoCompleteSource = AutoCompleteSource.CustomSource;
                p_class.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                p_class.AutoCompleteCustomSource = suggestion;
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

        private void manu_type()
        {

            try
            {
                AutoCompleteStringCollection suggestion = new AutoCompleteStringCollection();
                cmd = new OleDbCommand("select * from manu_type", con);

                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                ArrayList arlist = new ArrayList();

                while (rdr.Read())
                {
                    arlist.Add(rdr[1].ToString());
                }
                for (int i = 0; i < arlist.Count; i++)
                {
                    suggestion.Add(arlist[i].ToString());
                }

                con.Close();

                p_type.AutoCompleteSource = AutoCompleteSource.CustomSource;
                p_type.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                p_type.AutoCompleteCustomSource = suggestion;
            }
            catch (Exception oo)
            {
                MessageBox.Show(oo.Message);
            }
          
        }

        private void unit_display()
        {
            try
            {
                AutoCompleteStringCollection suggestion = new AutoCompleteStringCollection();
                cmd = new OleDbCommand("select * from unit", con);

                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                ArrayList arlist = new ArrayList();

                while (rdr.Read())
                {
                    arlist.Add(rdr[1].ToString());
                }
                for (int i = 0; i < arlist.Count; i++)
                {
                    suggestion.Add(arlist[i].ToString());
                }

                con.Close();

                s_unit.AutoCompleteSource = AutoCompleteSource.CustomSource;
                s_unit.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                s_unit.AutoCompleteCustomSource = suggestion;

                /* pur_unit.AutoCompleteSource = AutoCompleteSource.CustomSource;
                 pur_unit.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                 pur_unit.AutoCompleteCustomSource = suggestion;*/
            }
            catch (Exception yy)
            {
                MessageBox.Show(yy.Message);
            }
        }

        private void p_code_display()
        {

            DataSet ds = new DataSet();
            OleDbDataAdapter ad = new OleDbDataAdapter("select code from manu_f", con);
            ad.Fill(ds, "manu_f");
            p_code.DataSource = ds.Tables["manu_f"];
            p_code.DisplayMember = "code";
            p_code.ValueMember = "code";
            p_code.Enabled = true;

        }

        private void auto_number()
        {
            try
            {
               

                double sn;
                cmd = new OleDbCommand("select COUNT (*)  from stock", con);
                con.Open();
                sn = Convert.ToDouble(cmd.ExecuteScalar()) + 1;
                textBox7.Text = "OP00" + sn.ToString();
                

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



        private void s_unit_Click(object sender, EventArgs e)
        {
            s_unit.Clear();
        }

        private void p_code_Leave(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand("Select * from manu_f where code=" + p_code.Text + "", con);
                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    manu_f.Text = rdr[1].ToString();

                }

                
            }
            catch (Exception pp)
            {
                MessageBox.Show(pp.Message);
            }
            finally
            {
                con.Close();
                stock_display();
            }
        }

        private void p_code_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand("Select * from manu_f where code=" + p_code.Text + "", con);
                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    manu_f.Text = rdr[1].ToString();

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

        private void manu_f_Leave(object sender, EventArgs e)
        {
            //stock_display();
            if (textBox1.Text == "" && textBox2.Text=="" && textBox4.Text=="")
            {
                //textBox1.Text = "0";
                //textBox2.Text = "0";
               // textBox4.Text = "0";
                double op_stock = 0;
                textBox1.Text = op_stock.ToString();
                double purchase = 0;
                textBox2.Text = purchase.ToString();
                double sole = 0;
                textBox4.Text = sole.ToString();
                double total_stock = op_stock + purchase - sole;
                textBox3.Text = total_stock.ToString();
            }
            
            else if(textBox1.Text=="" && textBox2.Text=="" )
            {
                double op_stock = 0;
                textBox1.Text = op_stock.ToString();
                double purchase = 0;

                textBox2.Text = purchase.ToString();
                double sold = Convert.ToDouble(textBox4.Text);

                double total_stock = op_stock + purchase - sold;
                textBox3.Text = total_stock.ToString();

            }
            else if (textBox1.Text == "" && textBox4.Text == "")
            {
                double op_stock = 0;
                textBox1.Text = op_stock.ToString();
                double purchase = Convert.ToDouble(textBox2.Text);

                double sold = 0;
                textBox4.Text = sold.ToString();

                double total_stock = op_stock + purchase - sold;
                textBox3.Text = total_stock.ToString();
               

            }
            else if (textBox2.Text == "" && textBox4.Text == "")
            {
                double op_stock = Convert.ToDouble(textBox1.Text);

                double purchase = 0;
                textBox2.Text = purchase.ToString();

                double sold = 0;
                textBox4.Text = sold.ToString();

                double total_stock = op_stock + purchase - sold;
                textBox3.Text = total_stock.ToString();
            }
            else if (textBox4.Text == "")
            {
                double sold = 0;
                textBox4.Text = sold.ToString();


                double purchase = Convert.ToDouble(textBox2.Text);
                double op_stock = Convert.ToDouble(textBox1.Text);
                double total_stock = op_stock + purchase - sold;
                textBox3.Text = total_stock.ToString();
            }
            else if (textBox2.Text == "")
            {
                double purchase = 0;
                textBox2.Text = purchase.ToString();

                double op_stock = Convert.ToDouble(textBox1.Text);
                double sold = Convert.ToDouble(textBox4.Text);
                double total_stock = op_stock + purchase - sold;
                textBox3.Text = total_stock.ToString();
            }
            else if (textBox1.Text == "")
            {
                double op_stock = 0;
                textBox1.Text = op_stock.ToString();

                double purchase = Convert.ToDouble(textBox2.Text);
                double sold = Convert.ToDouble(textBox4.Text);
                double total_stock = op_stock + purchase - sold;
                textBox3.Text = total_stock.ToString();
            }

            else
            {
                double op_stock = Convert.ToDouble(textBox1.Text);
                double purchase = Convert.ToDouble(textBox2.Text);
                double sold = Convert.ToDouble(textBox4.Text);
                double total_stock = op_stock + purchase - sold;
                textBox3.Text = total_stock.ToString();



            }

            groupBox2.Visible = true;                  
             sellingprice();
            p_type.Enabled = true;
            p_class.Enabled = true;
            e_feature.Enabled = true;
            p_stock.Enabled = true;
            s_unit.Enabled = true;
            button1.Enabled = true;
            button17.Enabled = true;
            button3.Enabled = true;
            button2.Enabled = true;

        }

        private void sellingprice()
        {

            try
            {
                cmd = new OleDbCommand("Select * from stock_keeper where p_code=" + p_code.Text + "", con);
                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    selling_price.Text = rdr["selling_price"].ToString();

                }
            }
            catch (Exception y)
            {
                throw y;
            }
            finally
            {
                con.Close();
            }
        }

        private void stock_display()
        {
            try
            {

               /* cmd = new OleDbCommand("select * from stock where p_code=" + p_code.Text + "", con);
                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    textBox3.Text = rdr["total_stock"].ToString();

                }*/

                // bool sataus = false;
                cmd = new OleDbCommand(" select sum (op_stock),sum(purchase),sum(sales) from stock where p_code=" + p_code.Text + "", con);
                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    textBox1.Text = rdr.GetValue(0).ToString();
                    textBox2.Text = rdr.GetValue(1).ToString();
                    textBox4.Text = rdr.GetValue(2).ToString();
                    //sataus = true;

                }

               /* if (textBox1.Text == "" && textBox2.Text == "" && textBox4.Text == "")
                {
                    textBox1.Text = "0";
                    textBox2.Text = "0";
                    textBox4.Text = "0";
                }*/



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

        private void p_stock_Leave(object sender, EventArgs e)
        {

            try
            {
                if (textBox1.Text == "" && textBox2.Text == "" && textBox4.Text == "")
                {
                    textBox1.Text = "0";
                    textBox2.Text = "0";
                    textBox4.Text = "0";
                }
                double total_stock = Convert.ToDouble(textBox3.Text);
                double a_stock = Convert.ToDouble(p_stock.Text);
                double sum = total_stock + a_stock;
                textBox5.Text = sum.ToString();
            }
            catch (Exception u)
            {
                MessageBox.Show(u.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                /*cmd = new OleDbCommand("insert into stock_keeper(p_date,p_code,f_name,f_type,f_class,f_extra,stock,s_unit,selling_price,total_stock) values('" + maskedTextBox1.Text + "'," + p_code.Text + ",'" + manu_f.Text + "','" + p_type.Text + "','" + p_class.Text + "','" + e_feature.Text + "'," + p_stock.Text + ",'" + s_unit.Text + "'," + selling_price.Text + "," + textBox5.Text + ")",con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();*/

                cmd = new OleDbCommand("insert into stock(sn,s_date,p_code,f_name,f_type,f_class,f_extra,op_stock,s_unit,total_stock,selling_price)values('"+textBox7.Text+"','" + maskedTextBox1.Text + "'," + p_code.Text + ",'" + manu_f.Text + "','" + p_type.Text + "','" + p_class.Text + "','" + e_feature.Text + "'," + p_stock.Text + ",'" + s_unit.Text + "'," + textBox5.Text + "," + selling_price.Text + ")", con);
                con.Open();
               int yy= cmd.ExecuteNonQuery();
               if (yy > 0)
               {
                    DialogResult dmessage = MessageBox.Show("Data Saved", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (dmessage == DialogResult.OK)
                    {
                        maskedTextBox1.Text = "";
                        p_code.Text = "";
                        manu_f.Text = "";
                        p_type.Text = "";
                        p_class.Text = "";
                        e_feature.Text = "";
                        p_stock.Text = "0";
                        s_unit.Text = "Unit";
                        textBox5.Text = "0";
                        //selling_price.Text="";
                       
                       

                       // textBox7.Text = Convert.ToString(Convert.ToDouble(textBox7.Text) + 1);

                    }
               }
                
               
            }
            catch (Exception u)
            {
                throw u;
            }
            finally
            {
                con.Close();
                auto_number();
            }
        }

        private void s_unit_Leave(object sender, EventArgs e)
        {

            try
            {
                string stock_unit = s_unit.Text;

                bool status = false;
                cmd = new OleDbCommand("select * from unit", con);
                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if ((stock_unit == rdr[1].ToString()) && (s_unit.Text.Length != 0))
                    {
                        status = true;
                    }
                }

                if (status == true)
                {

                    button1.Enabled = true;
                    button17.Enabled = true;
                    button3.Enabled = true;
                    button2.Enabled = true;
                    // MessageBox.Show("Please ..! Check Unit", title, MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
                else
                {
                    MessageBox.Show("Please ..! Check Unit", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button1.Enabled = false;
                    button17.Enabled = false;
                    button3.Enabled = false;
                    button2.Enabled = false;
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

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                OleDbDataAdapter adb = new OleDbDataAdapter("select * from stock", con);
                adb.Fill(ds, "stock");
                ds.Tables[0].Constraints.Add("pk_code", ds.Tables[0].Columns[0], true);

                string n = (Interaction.InputBox("Enter Posting No: ....", "@Design by ..", "20", 100, 100));
                int cd = 0;
                DataRow drow = ds.Tables[0].Rows.Find(n);
                if (drow != null)
                {
                    cd = ds.Tables[0].Rows.IndexOf(drow);
                    textBox7.Text = drow[0].ToString();
                    maskedTextBox1.Text = drow[1].ToString();
                    p_code.Text = drow[2].ToString();
                    manu_f.Text = drow[3].ToString();
                    p_type.Text = drow[4].ToString();
                    p_class.Text = drow[5].ToString();
                    e_feature.Text = drow[6].ToString();
                    p_stock.Text = drow[7].ToString();
                    s_unit.Text = drow[11].ToString();
                    //textBox3.Text = drow[12].ToString();
                    selling_price.Text = drow[13].ToString();
                    stock_display();

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
            finally
            {

                string op = textBox7.Text;
                if (op[0] == 'O')
                {
                    
                    button2.Enabled = true;

                }
                else
                {
                    MessageBox.Show("It's Not Oping Stock So...!\n Can not edit", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button2.Enabled = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand("update stock set s_date='"+maskedTextBox1+"',p_code="+p_code.Text+",f_name='"+manu_f.Text+"',f_type='"+p_type.Text+"',f_class='"+p_class.Text+"',f_extra='"+e_feature.Text+"',op_stock="+p_stock.Text+",s_unit='"+s_unit.Text+"',total_stock="+textBox5.Text+",selling_price='"+selling_price.Text+"' where sn='"+textBox7.Text+"'",con);
            con.Open();
            int update1 = cmd.ExecuteNonQuery();
            if (update1 > 0)
            {
                MessageBox.Show("update successfull", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("sorry Try Again", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                /*cmd = new OleDbCommand("delete from stock where sn=" + textBox7.Text + "", con);
                con.Open();
                int delete1 = cmd.ExecuteNonQuery();
                if (delete1 > 0)
                {
                    MessageBox.Show("successfull deleted",title,MessageBoxButtons.OK,MessageBoxIcon.Information);

                    double total_stock = Convert.ToDouble(textBox3.Text);
                    double a_stock = Convert.ToDouble(p_stock.Text);
                    double sum = total_stock - a_stock;
                    textBox5.Text = sum.ToString();
                    p_type.Enabled = false;
                    p_class.Enabled = false;
                    e_feature.Enabled = false;
                    p_stock.Enabled = false;
                    s_unit.Enabled = false;


                   
                   
                }
                else
                {
                    MessageBox.Show("try again");
                }*/

               // bool sataus = false;
                cmd = new OleDbCommand(" select sum (op_stock),sum(purchase),sum(sales) from stock where p_code=" + p_code.Text + "", con);
                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    textBox1.Text = rdr.GetValue(0).ToString();
                    textBox2.Text = rdr.GetValue(1).ToString();
                    textBox4.Text = rdr.GetValue(2).ToString();
                    //sataus = true;
                   
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            auto_number();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
