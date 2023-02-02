using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;
using System.IO;
using Microsoft.VisualBasic;

namespace inventory_management
{
    public partial class stock_keeper : UserControl
    {
        #region Member Variables
        private OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\inventory\stock.mdb;Jet OLEDB:Database Password=karaujiya5");
        private OleDbCommand cmd;
        string title = "IMS";
        private OleDbDataAdapter adb = new OleDbDataAdapter();
        private OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder();
        private BindingSource bindingSource2 = new BindingSource();
        
        StringFormat strFormat;
        ArrayList arrColumnLefts = new ArrayList();
        ArrayList arrColumnWidths = new ArrayList();
       
        int iCellHeight = 0; //Used to get/set the datagridview cell height
        int iTotalWidth = 0; //
        int iRow = 0;//Used as counter
        bool bFirstPage = false; //Used to check whether we are printing first page
        bool bNewPage = false;// Used to check whether we are printing a new page
        int iHeaderHeight = 0;

        #endregion
        

        public stock_keeper()
        {
            InitializeComponent();

            //dataGridView1.Dock = DockStyle.Fill;
        }
        private void stock_keeper_Load_1(object sender, EventArgs e)
        {
           
            /*AutoCompleteStringCollection suggestion = new AutoCompleteStringCollection();
            cmd = new OleDbCommand("select * from manu_f",con);

            con.Open();
            OleDbDataReader rdr = cmd.ExecuteReader();
            ArrayList arlist = new ArrayList();
            while (rdr.Read())
            {
                arlist.Add(rdr[0].ToString());
            }
            for (int i = 0; i < arlist.Count; i++)
            {
                suggestion.Add(arlist[i].ToString());
            }

            con.Close();
            p_code.AutoCompleteSource = AutoCompleteSource.CustomSource;
            p_code.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            p_code.AutoCompleteCustomSource = suggestion;

            manu_f.AutoCompleteSource = AutoCompleteSource.CustomSource;
            manu_f.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            manu_f.AutoCompleteCustomSource = suggestion;*/
          
            p_code_display();
            manu_type();
            class_display();
            extra_display();
            unit_display();
            customer_dispaly();
            auto_number();
            //discount_display();
           
                       
        }

        private void auto_number()
        {
            try
            {
                /* double pn = 0;
                 cmd = new OleDbCommand("select sn from stock_keeper",con);
                 con.Open();
                 OleDbDataReader rdr = cmd.ExecuteReader();
                 while (rdr.Read())
                 {
                     pn = Convert.ToDouble(rdr[0].ToString());

                 }
                 rdr.Close();
                 posting_code.Text = (pn + 1).ToString();
                 con.Close();*/

                double sn;
                cmd = new OleDbCommand("select COUNT(*) from stock_keeper", con);
                con.Open();
                sn = Convert.ToDouble(cmd.ExecuteScalar()) + 1;
                posting_code.Text = "P00" + sn.ToString();
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

        private void customer_dispaly()
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter ad = new OleDbDataAdapter("select cust_name from costomer", con);
            ad.Fill(ds, "costomer");
            company_name.DataSource = ds.Tables["costomer"];
            company_name.DisplayMember = "cust_name";
            company_name.ValueMember = "cust_name";
            company_name.Enabled = true;

           /* DataSet ds = new DataSet();
            OleDbDataAdapter ad = new OleDbDataAdapter("select code from costomer", con);
            ad.Fill(ds, "manu_f");
            p_code.DataSource = ds.Tables["manu_f"];
            p_code.DisplayMember = "code";
            p_code.ValueMember = "code";
            p_code.Enabled = true;*/
        }

       

        private void discount_display()
        {
           /* DataSet daset = new DataSet();
            OleDbDataAdapter adbdata = new OleDbDataAdapter("select * from discount",con);
            adbdata.Fill(daset, "discount");
            p_dis.DataSource=daset.Tables["discount"];
            p_dis.DisplayMember = "discount";
            p_dis.ValueMember = "discount";
            p_dis.Enabled = true;*/
        }

        private void sellingprice()
        {
            try
            {
                cmd = new OleDbCommand("Select * from stock where p_code=" + p_code.Text + "", con);
                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    selling_price.Text = rdr[13].ToString();

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

        private void unit_display()
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

            pur_unit.AutoCompleteSource = AutoCompleteSource.CustomSource;
            pur_unit.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            pur_unit.AutoCompleteCustomSource = suggestion;
        }

        private void stock_display()
        {
            try
            {

                cmd = new OleDbCommand(" select sum (op_stock),sum(purchase),sum(sales) from stock where p_code=" + p_code.Text + "", con);
                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    op_stock.Text = rdr.GetValue(0).ToString();
                    pur_stock.Text = rdr.GetValue(1).ToString();
                    sold_stock.Text = rdr.GetValue(2).ToString();
                    //sataus = true;

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
            finally
            {
                con.Close();
            }
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            s_unit.Clear();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                //groupBox2.Enabled = true;
                //groupBox2.Visible = true;
            }
            if (checkBox1.Checked == false)
            {
               // groupBox2.Enabled = false;
               // groupBox2.Visible = false;
            }
        }

        private void textBox12_Click(object sender, EventArgs e)
        {
            purc_qnty.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double ledger_amount = Convert.ToDouble(total_amt.Text);
                double balance = Convert.ToDouble(textBox4.Text);
                double grand_total = ledger_amount + balance;
                textBox6.Text = grand_total.ToString();

                double purchase = Convert.ToDouble(pur_price.Text);
                double purchase_qnty = Convert.ToDouble(purc_qnty.Text);
                double purchase_amount = Convert.ToDouble(pur_amount.Text);
                double disount = Convert.ToDouble(textBox1.Text);
                double total_amount = Convert.ToDouble(total_amt.Text);
                double vat = Convert.ToDouble(t_vat.Text);

                cmd = new OleDbCommand("insert into stock_keeper(sn,p_date,p_code,f_name,f_type,f_class,f_extra,stock,s_unit,selling_price,pur_bill,com_name,pur_price,pur_qnty,pur_unit,pur_price_total,discount,vat,total_amount,total_stock)values('"+posting_code.Text+"','" + maskedTextBox1.Text + "'," + p_code.Text + ",'" + manu_f.Text + "','" + p_type.Text + "','" + p_class.Text + "','" + e_feature.Text + "'," + p_stock.Text + ",'" + s_unit.Text + "'," + selling_price.Text + ",'"+pur_bill.Text+"','"+company_name.Text+"'," + purchase + "," + purchase_qnty + ",'" + pur_unit.Text + "'," + purchase_amount + "," + disount + "," + vat + "," + total_amount + "," + textBox5.Text + ")", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                              
                cmd = new OleDbCommand("insert into creditor(sn,p_date,p_code,bill_no,cr_name,cr,total)values('"+posting_code.Text+"','" + maskedTextBox1.Text + "'," + p_code.Text + ",'" + pur_bill.Text + "','" + company_name.Text + "'," + total_amount + ","+grand_total+")", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                cmd = new OleDbCommand("insert into stock (sn,s_date,p_code,bill_no,purchase,total_stock,selling_price) values('"+posting_code.Text+"','" + maskedTextBox1.Text + "'," + p_code.Text + ",'"+pur_bill.Text+"'," + purchase_qnty + "," + textBox5.Text + ","+selling_price.Text+")", con);
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
                        p_stock.Text = "";
                        s_unit.Text = "";
                        //selling_price.Text="";
                        pur_bill.Text = "0";
                        company_name.Text = "";
                        pur_price.Text = "0";
                        purc_qnty.Text = "0";
                        pur_unit.Text = "Unit";
                        textBox5.Text = "0";
                        textBox1.Text = "0";
                        textBox2.Text = "0";
                        t_vat.Text = "0";
                        total_amt.Text = "0";
                        p_dis.Text = "0";
                        pur_amount.Text = "0";
                        pur_price.Text = "0";
                        textBox4.Text = "0";
                        textBox6.Text = "0";

                       // posting_code.Text = Convert.ToString(Convert.ToDouble(posting_code.Text) + 1);
                    }
               }

               

            }
            catch (Exception u)
            {
                MessageBox.Show(u.Message);
            }
            finally
            {
                con.Close();
                auto_number();
            }
               
         

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            pur_unit.Clear();
        }

        private void purc_qnty_Leave(object sender, EventArgs e)
        {
            float purchase = float.Parse(pur_price.Text);
            float qnty = float.Parse(purc_qnty.Text);
            float amount = purchase * qnty;
            pur_amount.Text = amount.ToString();

            double stock = Convert.ToDouble(textBox3.Text);
            double a_stock = Convert.ToDouble(purc_qnty.Text);
            double t_stock = stock + a_stock;
            textBox5.Text = t_stock.ToString();
            total_amt.Text = pur_amount.Text;
            textBox2.Text = pur_amount.Text;

            
        }

        private void pur_price_Leave_1(object sender, EventArgs e)
        {
            pur_amount.Text = pur_price.Text;
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                groupBox3.Visible = true;
                double v = 0.13;
                double t = Convert.ToDouble(textBox2.Text);

                double vat = t * v;

                t_vat.Text = vat.ToString();
                double grand_total = t + vat;
                total_amt.Text = grand_total.ToString();
              

            }
            
        }

        private void pur_amount_Leave_1(object sender, EventArgs e)
        {
            //total_amt.Text = pur_amount.Text;
        }

        private void p_dis_Leave(object sender, EventArgs e)
        {
            try
            {
                float total_pur = float.Parse(pur_amount.Text);
                float dis = float.Parse(p_dis.Text);
                float total_dis = total_pur * (dis / 100);
                textBox1.Text = total_dis.ToString();
                float total_amount = total_pur - total_dis;
                total_amt.Text = total_amount.ToString();
                textBox2.Text = total_amount.ToString();
            }
            catch (Exception y)
            {
                MessageBox.Show(y.Message);
            }
        }

        private void p_stock_Leave(object sender, EventArgs e)
        {
            try
            {
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

        private void purc_qnty_Click(object sender, EventArgs e)
        {
            purc_qnty.Clear();
        }

        private void pur_price_Click(object sender, EventArgs e)
        {
            pur_price.Clear();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {

            double v = 0.13;
            double t = Convert.ToDouble(textBox2.Text);

            double vat = t * v;

            t_vat.Text = vat.ToString();
            double grand_total = t + vat;
            total_amt.Text = grand_total.ToString();
            if (checkBox1.Checked == false)
            {
                float total_pur = float.Parse(pur_amount.Text);
                float dis = float.Parse(p_dis.Text);
                float total_dis = total_pur * (dis / 100);
                textBox1.Text = total_dis.ToString();
                float total_amount = total_pur - total_dis;
                total_amt.Text = total_amount.ToString();
                textBox2.Text = total_amount.ToString();
                groupBox3.Visible = false;

            }

              
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand("delete from stock_keeper where sn='" + posting_code.Text + "'", con);
                con.Open();
                int delete1 = cmd.ExecuteNonQuery();
                con.Close();

                cmd = new OleDbCommand("delete from stock where sn='" + posting_code.Text + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                cmd = new OleDbCommand("delete from creditor where sn='" + posting_code.Text + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                /*if (delete1 > 0)
                {
                    DialogResult dial = MessageBox.Show("Edit successfull....!\n\nClick Ok for Save ", title, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dial == DialogResult.OK)
                    {
                        
                       
                        dataGridView1.DataSource = bindingSource2;
                        GetData("select sn, p_date, p_code, f_name, pur_bill,com_name,pur_price,pur_qnty,pur_unit from stock_keeper");

                    }
                    // MessageBox.Show("successfull deleted", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }*/
            }
            catch (Exception uu)
            {
                MessageBox.Show(uu.Message);

            }
            finally
            {
                con.Close();
               

                DialogResult dial = MessageBox.Show("Edit successfull....!\n\nClick Ok for Save ", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dial == DialogResult.OK)
                {
                    button1.PerformClick();

                    //dataGridView1.DataSource = bindingSource2;
                   // GetData("select sn, p_date, p_code, f_name, pur_bill,com_name,pur_price,pur_qnty,pur_unit from stock_keeper");

                }
                
            }


        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           
                p_stock.Enabled = true;
                s_unit.Enabled = true;
                groupBox2.Enabled = false;

            
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
          /*  if (checkBox2.Checked == true)
            {
                p_stock.Enabled = true;
                s_unit.Enabled = true;
                amnu_display();
             
               
            }       

            else if (checkBox2.Checked == false)
            {
                p_stock.Enabled = false;
                s_unit.Enabled = false;
                groupBox2.Enabled = true;
                manu_f.Text = "";
               // p_code.Items.Clear();
                //p_code.DataSource = null;
                

            }*/
        }

        private void amnu_display()
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
            catch (Exception kk)
            {
                MessageBox.Show(kk.Message);
            }
            finally
            {
                con.Close();
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

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
           // maskedTextBox1.Text = DateTime.Now.ToString();
        }

        private void maskedTextBox1_Click(object sender, EventArgs e)
        {
           // maskedTextBox1.Text = DateTime.Now.ToString();
        }

        private void pur_unit_Click(object sender, EventArgs e)
        {
            pur_unit.Clear();
        }

        private void p_code_Leave(object sender, EventArgs e)
        {
           /* bool status = false;
            cmd = new OleDbCommand("select * from stock_keeper where p_code ="+p_code.Text+"", con);
            con.Open();
            OleDbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                status = true;
                textBox3.Text = rdr[16].ToString();
                manu_f.Text = rdr[2].ToString();
                p_type.Text = rdr[3].ToString();
                p_class.Text = rdr[4].ToString();
                e_feature.Text = rdr[5].ToString();               
            }
            if (status == false)
            {
                MessageBox.Show("sorry");
            }
           */

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
               // sellingprice();
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

        private void p_code_Leave_1(object sender, EventArgs e)
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

        private void manu_f_TextChanged(object sender, EventArgs e)
        {

        }

        private void manu_f_Leave(object sender, EventArgs e)
        {
            try
            {
                //stock_display();

                if (op_stock.Text == "" && pur_stock.Text == "" && sold_stock.Text == "")
                {
                    //textBox1.Text = "0";
                    //textBox2.Text = "0";
                    // textBox4.Text = "0";
                    double op_stock_c = 0;
                    op_stock.Text = op_stock_c.ToString();
                    double purchase = 0;
                    pur_stock.Text = purchase.ToString();
                    double sole = 0;
                    sold_stock.Text = sole.ToString();
                    double total_stock = op_stock_c + purchase - sole;
                    stock_balance.Text = total_stock.ToString();
                }

                else if (op_stock.Text == "" && pur_stock.Text == "")
                {
                    double op_stock_c = 0;
                    op_stock.Text = op_stock_c.ToString();
                    double purchase = 0;

                    pur_stock.Text = purchase.ToString();
                    double sold = Convert.ToDouble(sold_stock.Text);

                    double total_stock = op_stock_c + purchase - sold;
                    stock_balance.Text = total_stock.ToString();

                }
                else if (op_stock.Text == "" && sold_stock.Text == "")
                {
                    double op_stock_c = 0;
                    op_stock.Text = op_stock_c.ToString();
                    double purchase = Convert.ToDouble(pur_stock.Text);

                    double sold = 0;
                    sold_stock.Text = sold.ToString();

                    double total_stock = op_stock_c + purchase - sold;
                    stock_balance.Text = total_stock.ToString();


                }
                 else if (pur_stock.Text == "" && sold_stock.Text == "")
                 {
                     double op_stock_c = Convert.ToDouble(op_stock.Text);

                     double purchase = 0;
                     pur_stock.Text = purchase.ToString();

                     double sold = 0;
                     sold_stock.Text = sold.ToString();

                     double total_stock = op_stock_c + purchase - sold;
                     stock_balance.Text = total_stock.ToString();
                 }

                else if (sold_stock.Text == "")
                {
                    double sold = 0;
                    sold_stock.Text = sold.ToString();

                    double purchase = Convert.ToDouble(pur_stock.Text);
                    double op_stock_c = Convert.ToDouble(op_stock.Text);
                    double total_stock = op_stock_c + purchase - sold;
                    stock_balance.Text = total_stock.ToString();

                }
                else if (op_stock.Text == "") 
                {
                    double op_stock_c = 0;
                    op_stock.Text = op_stock_c.ToString();

                    double purchase = Convert.ToDouble(pur_stock.Text);
                    double sold = Convert.ToDouble(sold_stock.Text);
                    double total_stock = op_stock_c + purchase - sold;
                    stock_balance.Text = total_stock.ToString();
                }
                else if (pur_stock.Text == "")
                {
                    double purchase = 0;
                    pur_stock.Text = purchase.ToString();

                    double op_stock_c = Convert.ToDouble(op_stock.Text);
                    double sold = Convert.ToDouble(sold_stock.Text);
                    double total_stock = op_stock_c + purchase - sold;
                    stock_balance.Text = total_stock.ToString();
                }

                else
                {
                    double op_stock_c = Convert.ToDouble(op_stock.Text);
                    double purchase = Convert.ToDouble(pur_stock.Text);
                    double sold = Convert.ToDouble(sold_stock.Text);
                    double total_stock = op_stock_c + purchase - sold;
                    stock_balance.Text = total_stock.ToString();


                }
                sellingprice();
                button1.Enabled = true;
                button17.Enabled = true;
                button3.Enabled = true;
                button2.Enabled = true;
            }
            catch (Exception uu)
            {
                MessageBox.Show(uu.Message);
            }
            finally
            {
                textBox3.Text = stock_balance.Text;
            }
            
        }

        private void p_dis_Leave_1(object sender, EventArgs e)
        {
            float total_pur = float.Parse(pur_amount.Text);
            float dis = float.Parse(p_dis.Text);
            float total_dis = total_pur * (dis / 100);
            textBox1.Text = total_dis.ToString();
            float total_amount = total_pur - total_dis;
            total_amt.Text = total_amount.ToString();
            textBox2.Text = total_amount.ToString();
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

        private void pur_unit_Leave(object sender, EventArgs e)
        {
            try
            {
                string unit_purchase = pur_unit.Text;
                
                bool status = false;
                cmd = new OleDbCommand("select * from stock_keeper where p_code=" + p_code.Text + "", con);
                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if ((unit_purchase == rdr[8].ToString()) && (pur_unit.Text.Length != 0))
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
                    DialogResult dial = MessageBox.Show("Please ..! Check Unit \n for new Unit click OK ", title, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dial == DialogResult.OK)
                    {
                        button1.Enabled = true;
                        button17.Enabled = true;
                        button3.Enabled = true;
                        button2.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = false;
                        button17.Enabled = false;
                        button3.Enabled = false;
                        button2.Enabled = false;

                    }

                    //MessageBox.Show("Please ..! Check Unit \n for new Unit click OK ", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
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

        private void pur_bill_Leave(object sender, EventArgs e)
        {
            try
            {
                string bill_name = pur_bill.Text;

                bool status = false;
                cmd = new OleDbCommand("select * from stock_keeper where p_code=" + p_code.Text + "", con);
                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                   if((bill_name==rdr[10].ToString())&&(pur_bill.Text.Length!=0))
                   {
                       status=true;
                   }
                }

                if (status == true)
                {
                    MessageBox.Show("Sorry...! Bill already Used ", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button1.Enabled = false;
                    button17.Enabled = false;
                    button3.Enabled = false;
                    button2.Enabled = false;

                   /*button1.Enabled = true;
                    button17.Enabled = true;
                    button3.Enabled = true;
                    button2.Enabled = true;*/
                    // MessageBox.Show("Please ..! Check Unit", title, MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
                else
                {
                    button1.Enabled = true;
                    button17.Enabled = true;
                    button3.Enabled = true;
                    button2.Enabled = true;

                    /*MessageBox.Show("Please ..! Check Unit", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button1.Enabled = false;
                    button17.Enabled = false;
                    button3.Enabled = false;
                    button2.Enabled = false;*/
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

        private void company_name_Leave(object sender, EventArgs e)
        {
            try
            {
                string customer = company_name.Text;

                bool status = false;
                cmd = new OleDbCommand("select * from creditor where cr_name='" +customer + "'", con);
                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    status = true;
                    textBox4.Text = rdr[7].ToString();
                                   
                }
                if (status == false)
                {
                    textBox4.Text = "0";
                }
                pur_price.Enabled = true;
                purc_qnty.Enabled = true;
                pur_unit.Enabled = true;
               
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
            /*OleDbCommand cmd = new OleDbCommand("select * from stock_keeper where p_date='" + maskedTextBox1.Text + "' and p_code=" + p_code.Text + "", con);
            con.Open();
            OleDbDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read()) 
            {
                manu_f.Text = rdr[2].ToString();
                p_type.Text = rdr[3].ToString();
                p_class.Text = rdr[4].ToString();
                e_feature.Text = rdr[5].ToString();
                p_stock.Text = rdr[6].ToString();
                s_unit.Text = rdr[7].ToString();
                selling_price.Text = rdr[8].ToString();
                pur_bill.Text = rdr[9].ToString();
                company_name.Text = rdr[10].ToString();
                pur_price.Text = rdr[11].ToString();
                purc_qnty.Text = rdr[12].ToString();
                pur_unit.Text = rdr[13].ToString();
                pur_amount.Text = rdr[14].ToString();
                p_dis.Text = rdr[15].ToString();
                t_vat.Text = rdr[16].ToString();
                total_amt.Text = rdr[17].ToString();
            }*/

            try
            {
                DataSet ds = new DataSet();
                OleDbDataAdapter adb = new OleDbDataAdapter("select * from stock_keeper", con);
                adb.Fill(ds, "stock_keeper");
                ds.Tables[0].Constraints.Add("pk_code", ds.Tables[0].Columns[0], true);

               string n =(Interaction.InputBox("Enter  Posting No: ....", "@Design by ..", "20", 100, 100));
                int cd = 0;
                DataRow drow = ds.Tables[0].Rows.Find(n);
                if (drow != null)
                {
                    cd = ds.Tables[0].Rows.IndexOf(drow);
                    posting_code.Text = drow[0].ToString();
                    maskedTextBox1.Text = drow[1].ToString();
                    p_code.Text = drow[2].ToString();
                    /*textBox1.Text = drow[0].ToString();
                    textBox2.Text = drow[1].ToString();*/
                    manu_f.Text = drow[3].ToString();
                    p_type.Text = drow[4].ToString();
                    p_class.Text = drow[5].ToString();
                    e_feature.Text = drow[6].ToString();
                    p_stock.Text = drow[7].ToString();
                    s_unit.Text = drow[8].ToString();
                    selling_price.Text = drow[9].ToString();
                    pur_bill.Text = drow[10].ToString();
                    company_name.Text = drow[11].ToString();
                    pur_price.Text = drow[12].ToString();
                    purc_qnty.Text =  drow[13].ToString();
                    pur_unit.Text =  drow[14].ToString();
                    pur_amount.Text = drow[15].ToString();
                    p_dis.Text = drow[16].ToString();
                    t_vat.Text = drow[17].ToString();
                    total_amt.Text = drow[18].ToString();

                    //textBox3.Text = drow["total_stock"].ToString();
                    stock_display();
                    button2.Enabled = true;
                    button1.Enabled = true;
                    //button3.Enabled = true;




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


        private void button4_Click_1(object sender, EventArgs e)
        {
            if (date_by.Checked == true)
            {
               // dataGridView1.DataSource = "stock_keeper";
                dataGridView1.DataSource = bindingSource2;
                GetData("select sn, p_date, p_code, f_name, pur_bill,com_name,pur_price,pur_qnty,pur_unit from stock_keeper where p_date >='" + from_date.Text + "' and p_date <='" + to_date.Text + "'");
                this.dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[0].HeaderText = "Posting Code";
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[1].HeaderText = "Date";
                dataGridView1.Columns[2].Width = 50;
                dataGridView1.Columns[2].HeaderText = "P.Code";
                dataGridView1.Columns[3].Width = 150;
                dataGridView1.Columns[3].HeaderText = "Product Name";
                dataGridView1.Columns[4].Width = 75;
                dataGridView1.Columns[4].HeaderText = "Pur.Bill";
                dataGridView1.Columns[5].Width = 150;
                dataGridView1.Columns[5].HeaderText = "company Name";
                dataGridView1.Columns[6].Width = 75;
                dataGridView1.Columns[6].HeaderText = "Price";
                dataGridView1.Columns[7].Width = 50;
                dataGridView1.Columns[7].HeaderText = "QNTY";
                dataGridView1.Columns[8].Width = 50;
                dataGridView1.Columns[8].HeaderText = "Unit";
               
                
            }
            else if (stock_by.Checked == true)
            {
                if (stock_morethen.Text.Length > 0 && stock_lessthen.Text.Length > 0)
                {
                    dataGridView1.DataSource = bindingSource2;
                    GetData("select  sn, p_date, p_code, f_name, pur_bill,com_name,pur_price,pur_qnty,pur_unit from stock_keeper where pur_qnty >=" + stock_morethen.Text + " and pur_qnty <=" + stock_lessthen.Text + "");
                    this.dataGridView1.RowHeadersVisible = false;
                    dataGridView1.Columns[0].Width = 50;
                    dataGridView1.Columns[0].HeaderText = "Posting Code";
                    dataGridView1.Columns[1].Width = 100;
                    dataGridView1.Columns[1].HeaderText = "Date";
                    dataGridView1.Columns[2].Width = 50;
                    dataGridView1.Columns[2].HeaderText = "P.Code";
                    dataGridView1.Columns[3].Width = 150;
                    dataGridView1.Columns[3].HeaderText = "Product Name";
                    dataGridView1.Columns[4].Width = 75;
                    dataGridView1.Columns[4].HeaderText = "Pur.Bill";
                    dataGridView1.Columns[5].Width = 150;
                    dataGridView1.Columns[5].HeaderText = "company Name";
                    dataGridView1.Columns[6].Width = 75;
                    dataGridView1.Columns[6].HeaderText = "Price";
                    dataGridView1.Columns[7].Width = 50;
                    dataGridView1.Columns[7].HeaderText = "QNTY";
                    dataGridView1.Columns[8].Width = 50;
                    dataGridView1.Columns[8].HeaderText = "Unit";
                }

                else if (stock_morethen.Text.Length > 0 )
                {
                    
                    //dataGridView1.DataSource = "stock_keeper";
                    dataGridView1.DataSource = bindingSource2;
                    GetData("select sn, p_date, p_code, f_name, pur_bill,com_name,pur_price,pur_qnty,pur_unit from stock_keeper where pur_qnty >= " + stock_morethen.Text + "");
                    this.dataGridView1.RowHeadersVisible = false;
                    dataGridView1.Columns[0].Width = 50;
                    dataGridView1.Columns[0].HeaderText = "Posting Code";
                    dataGridView1.Columns[1].Width = 100;
                    dataGridView1.Columns[1].HeaderText = "Date";
                    dataGridView1.Columns[2].Width = 50;
                    dataGridView1.Columns[2].HeaderText = "P.Code";
                    dataGridView1.Columns[3].Width = 150;
                    dataGridView1.Columns[3].HeaderText = "Product Name";
                    dataGridView1.Columns[4].Width = 75;
                    dataGridView1.Columns[4].HeaderText = "Pur.Bill";
                    dataGridView1.Columns[5].Width = 150;
                    dataGridView1.Columns[5].HeaderText = "company Name";
                    dataGridView1.Columns[6].Width = 75;
                    dataGridView1.Columns[6].HeaderText = "Price";
                    dataGridView1.Columns[7].Width = 50;
                    dataGridView1.Columns[7].HeaderText = "QNTY";
                    dataGridView1.Columns[8].Width = 50;
                    dataGridView1.Columns[8].HeaderText = "Unit";
                    //GetData(adb.SelectCommand.CommandText);
                    //GetData("select * from stock_keeper where pur_qnty =" + stock_morethen + "");
                   
                    // MessageBox.Show("hello");
                
                }
                else if (stock_lessthen.Text.Length > 0)
                {
                    dataGridView1.DataSource = bindingSource2;
                    GetData("select sn, p_date, p_code, f_name, pur_bill,com_name,pur_price,pur_qnty,pur_unit from stock_keeper where pur_qnty <= " + stock_lessthen.Text + "");
                    this.dataGridView1.RowHeadersVisible = false;
                    dataGridView1.Columns[0].Width = 50;
                    dataGridView1.Columns[0].HeaderText = "Posting Code";
                    dataGridView1.Columns[1].Width = 100;
                    dataGridView1.Columns[1].HeaderText = "Date";
                    dataGridView1.Columns[2].Width = 50;
                    dataGridView1.Columns[2].HeaderText = "P.Code";
                    dataGridView1.Columns[3].Width = 150;
                    dataGridView1.Columns[3].HeaderText = "Product Name";
                    dataGridView1.Columns[4].Width = 75;
                    dataGridView1.Columns[4].HeaderText = "Pur.Bill";
                    dataGridView1.Columns[5].Width = 150;
                    dataGridView1.Columns[5].HeaderText = "company Name";
                    dataGridView1.Columns[6].Width = 75;
                    dataGridView1.Columns[6].HeaderText = "Price";
                    dataGridView1.Columns[7].Width = 50;
                    dataGridView1.Columns[7].HeaderText = "QNTY";
                    dataGridView1.Columns[8].Width = 50;
                    dataGridView1.Columns[8].HeaderText = "Unit";
                }
               
                
                                        
               
            }
            else if (code_by.Checked == true)
            {
                dataGridView1.DataSource = bindingSource2;
                GetData("select sn, p_date, f_name, pur_bill,com_name,pur_price,pur_qnty,pur_unit from stock_keeper where p_code=" + textBox14.Text + "");
                this.dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[0].HeaderText = "Posting Code";
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[1].HeaderText = "Date";
                
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[2].HeaderText = "Product Name";
                dataGridView1.Columns[3].Width = 75;
                dataGridView1.Columns[3].HeaderText = "Pur.Bill";
                dataGridView1.Columns[4].Width = 150;
                dataGridView1.Columns[4].HeaderText = "company Name";
                dataGridView1.Columns[5].Width = 75;
                dataGridView1.Columns[5].HeaderText = "Price";
                dataGridView1.Columns[6].Width = 50;
                dataGridView1.Columns[6].HeaderText = "QNTY";
                dataGridView1.Columns[7].Width = 50;
                dataGridView1.Columns[7].HeaderText = "Unit";
            }

            else if (all_data.Checked == true)
            {
                dataGridView1.DataSource = bindingSource2;
                GetData("select sn, p_date, p_code, f_name, pur_bill,com_name,pur_price,pur_qnty,pur_unit from stock_keeper");
                //GetData("select  p_date, p_code from stock_keeper");

                this.dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[0].HeaderText = "Posting Code";
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[1].HeaderText = "Date";
                dataGridView1.Columns[2].Width = 50;
                dataGridView1.Columns[2].HeaderText = "P.Code";
                dataGridView1.Columns[3].Width = 150;
                dataGridView1.Columns[3].HeaderText = "Product Name";
                dataGridView1.Columns[4].Width = 75;
                dataGridView1.Columns[4].HeaderText = "Pur.Bill";
                dataGridView1.Columns[5].Width = 150;
                dataGridView1.Columns[5].HeaderText = "company Name";
                dataGridView1.Columns[6].Width = 75;
                dataGridView1.Columns[6].HeaderText = "Price";
                dataGridView1.Columns[7].Width = 50;
                dataGridView1.Columns[7].HeaderText = "QNTY";
                dataGridView1.Columns[8].Width = 50;
                dataGridView1.Columns[8].HeaderText = "Unit";

            }

            else
            {
                MessageBox.Show("Sorry,  You can't choose", title, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


            }
        private void GetData(string SelectCommand)
        {
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\inventory\stock.mdb;Jet OLEDB:Database Password=karaujiya5");
                 adb = new OleDbDataAdapter(SelectCommand, con);
                 commandBuilder = new OleDbCommandBuilder(adb);
               // DataSet ds = new DataSet();
                 DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                //adb.Fill(ds,"stock_keeper");
                adb.Fill(table);
                bindingSource2.DataSource = table;
               // dataGridView1.DataSource = ds;
                //dataGridView1.DataMember = "stock_keeper";
              
                //bindingSource1.DataSource = ds;
                //dataGridView1.AutoResizeColumn(DataGridViewAutoSizeColumnMode.AllCellsExceptHeader);
            }
            catch (OleDbException tt)
            {
                MessageBox.Show(tt.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //GetData(adb.SelectCommand.CommandText);
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;
            printDialog.UseEXDialog = true;
            if (DialogResult.OK == printDialog.ShowDialog())
            {
                printDocument1.DocumentName = "Purchase Report";
                printDocument1.Print();
            }
        }

        private void dataGridView1_CellDoubleClick(object sendrt,DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex >-1)
            {
               // button17.PerformClick();
                MessageBox.Show("test");


            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                 button17.PerformClick();
                //MessageBox.Show("test");

                
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("test");
        }

        #region Print Page Event
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
          
           /* int iLeftMargin = e.MarginBounds.Left;
            int iTopMargin = e.MarginBounds.Top;
            bool bMorePageToPrint = false;
            int iTmpWidth = 0;
            int iTotalWidth=0;
            int iHeaderHeight = 0;
            int iRow=0;
             int iCellHeight = 0;
             //int iCount = 0;
            //StringFormat strFormat = new StringFormat();

            if (bFirstPage)
            {
                foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
                {
                    iTmpWidth=(int)(Math.Floor((double)((double)GridCol.Width/(double)iTotalWidth*(double)iTotalWidth*((double)e.MarginBounds.Width/(double)iTotalWidth))));

                    iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText, GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                    arrColumnLefts.Add(iLeftMargin);
                    arrColumnWidths.Add(iTmpWidth);
                    iLeftMargin += iTmpWidth;
                }
            }
            while (iRow <= dataGridView1.Rows.Count - 1)
            {
                DataGridViewRow GridRow = dataGridView1.Rows[iRow];
                iCellHeight = GridRow.Height + 5;
                int iCount = 0;

                if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    bNewPage = true;
                    bFirstPage = false;
                    bMorePageToPrint = true;
                    break;
                }
                else
                {
                    if (bNewPage)
                    {
                        e.Graphics.DrawString("Purchase Summary", new Font(dataGridView1.Font, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top - e.Graphics.MeasureString("Customer Summary", new Font(dataGridView1.Font, FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                        string strDate = DateTime.Now.ToLongDateString() + "" + DateTime.Now.ToShortTimeString();

                        e.Graphics.DrawString(strDate, new Font(dataGridView1.Font, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(strDate, new Font(dataGridView1.Font, FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top - e.Graphics.MeasureString("Purchase Summary", new Font(new Font(dataGridView1.Font, FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                        iTopMargin = e.MarginBounds.Top;
                        foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),new Rectangle((int)arrColumnLefts[iCount], iTopMargin,(int)arrColumnWidths[iCount], iHeaderHeight));
                            //e.Graphics.FillRectangle(new SolidBrush(Color.LightCyan),new Rectangle((int)arrColumnLefts[iCount],iTopMargin,(int)arrColumnWidths[iCount],iHeaderHeight));

                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)arrColumnLefts[iCount], iTopMargin,(int)arrColumnWidths[iCount], iHeaderHeight));
                            //e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount], iHeaderHeight));

                            e.Graphics.DrawString(GridCol.HeaderText,GridCol.InheritedStyle.Font,new SolidBrush(GridCol.InheritedStyle.ForeColor),new RectangleF((int)arrColumnLefts[iCount], iTopMargin,(int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                            iCount++;
                            //e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font, new SolidBrush(GridCol.InheritedStyle.ForeColor), new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                            //iCount++;
                                               
                        }
                        bNewPage = false;
                        iTopMargin += iHeaderHeight;
                        
                    }
                    iCount=0;
                    foreach(DataGridViewCell Cel in GridRow.Cells)
                    {
                        if (Cel.Value != null)
                        {
                            e.Graphics.DrawString(Cel.Value.ToString(),Cel.InheritedStyle.Font, new SolidBrush(Cel.InheritedStyle.ForeColor), new RectangleF((int)arrColumnLefts[iCount],(float)iTopMargin,(int)arrColumnWidths[iCount], (float)iCellHeight),
                             strFormat);
                                                        
                        }
                        e.Graphics.DrawRectangle(Pens.Black,new Rectangle((int)arrColumnLefts[iCount], iTopMargin,(int)arrColumnWidths[iCount], iCellHeight));
                        iCount++;

                    }

                   
                }

                iRow++;
                iTopMargin += iCellHeight;
                
            }

            if (bMorePageToPrint)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;*/

           try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                                       (double)iTotalWidth * (double)iTotalWidth *
                                       ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headres
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                //Loop till all the grid rows not get printed
                while (iRow <= dataGridView1.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dataGridView1.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("Purchase Summary", new Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Purchase Summary", new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new Font(dataGridView1.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Purchase Summary", new Font(new Font(dataGridView1.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dataGridView1.Columns)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {
                                e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font,
                                            new SolidBrush(Cel.InheritedStyle.ForeColor),
                                            new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin,
                                            (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)arrColumnLefts[iCount],
                                    iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }

                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           


        }
        #endregion

        #region Begin Print Event Handler
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            /*StringFormat strformat = new StringFormat();
            strformat.Alignment = StringAlignment.Near;
            strformat.LineAlignment = StringAlignment.Center;
            strformat.Trimming = StringTrimming.EllipsisCharacter;

            arrColumnLefts.Clear();
            arrColumnWidths.Clear();
             iCellHeight = 0;
             iRow = 0;

            bFirstPage = true;
            bNewPage = true;
            int iTotalWidth = 0;
            foreach (DataGridViewColumn dgvGridCol in dataGridView1.Columns)
            {
                iTotalWidth += dgvGridCol.Width;
            }*/

            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in dataGridView1.Columns)
                {
                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        #endregion

        private void button6_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Show();
        }

       
              
        
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   