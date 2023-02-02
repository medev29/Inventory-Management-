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
using Microsoft.VisualBasic;

namespace inventory_management
{
    public partial class sales_form : UserControl
    {
        public sales_form()
        {
            InitializeComponent();
        }
        #region header;
        private OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\inventory\stock.mdb;Jet OLEDB:Database Password=karaujiya5");
        private OleDbCommand cmd = new OleDbCommand();
        private OleDbDataAdapter adb = new OleDbDataAdapter();
        private OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder();
        private BindingSource bindingsource = new BindingSource();
        //private OleDbDataReader rdr;
        private DataTable table = new DataTable();
        string title = "IMS";


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
        /*private int CurrentY;
        private int CurrentX;
        private int leftMargin;
        private int rightMargin;
        private int topMargin;
        private int bottmMargin;
        private int InvoiceWidth;
        private int InvoiceHeight;
      
        private Font InvTitleFont = new Font("Arial", 24, FontStyle.Regular);
        private int InvTitleHeight;
        private Font InvSubTitlefont = new Font("Arial", 14, FontStyle.Regular);
        private int InvSubTitleHeight;
        private Font InvoiceFont = new Font("Arial", 12, FontStyle.Regular);
        private int InvoiceFontHeight;
        private SolidBrush BlueBrush = new SolidBrush(Color.Blue);
        private SolidBrush RedBrush = new SolidBrush(Color.Red);
        private SolidBrush BlackBrush = new SolidBrush(Color.Black);*/


        private void sales_form_Load(object sender, EventArgs e)
        {
            p_code_display();
            auto_number();
            unit_display();
            bill_no_display();
            columns_count();
            customer_dispaly();
        }

        private void customer_dispaly()
        {
            DataSet ds = new DataSet();
            OleDbDataAdapter ad = new OleDbDataAdapter("select cust_name from costomer", con);
            ad.Fill(ds, "costomer");
            customer.DataSource = ds.Tables["costomer"];
            customer.DisplayMember = "cust_name";
            customer.ValueMember = "cust_name";
            customer.Enabled = true;
        }

        private void columns_count()
        {
           
        }

        private void bill_no_display()
        {
            try
            {

                double sn;
                cmd = new OleDbCommand("select COUNT(*) from bill_no", con);
                con.Open();
                sn = Convert.ToDouble(cmd.ExecuteScalar()) + 1;
                bill_no.Text = "S00" + sn.ToString();
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

          
            pur_unit.AutoCompleteSource = AutoCompleteSource.CustomSource;
            pur_unit.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            pur_unit.AutoCompleteCustomSource = suggestion;
        }

        private void auto_number()
        {

            try
            {
                /*
                 double sn;
                 cmd = new OleDbCommand("select COUNT(*) from sales_men", con);
                 con.Open();
                 sn = Convert.ToDouble(cmd.ExecuteScalar()) + 1;
                 posting_code.Text = "S00" + sn.ToString();*/
                string auto = autonum.Text;

                cmd = new OleDbCommand("select * from sales_men", con);
                con.Open();
                OleDbDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    auto = rdr[1].ToString();
                    autonum.Text = rdr[1].ToString();
                }


                for (int i = 0; i < auto.Length; i++)
                 {
                     string strmodified = auto.Substring(1, i);
                     textBox3.Text = strmodified.ToString();
                 
                 }

            }
            catch (Exception yy)
            {
                MessageBox.Show(yy.Message);
            }
            finally
            {
                con.Close();

                        
                int sn = Convert.ToInt32(textBox3.Text);
                int newsn = sn + 1;
                posting_code.Text = "S00" + newsn.ToString();

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

        private void checkBox1_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception yy)
            {
                MessageBox.Show(yy.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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

        private void purc_qnty_Leave(object sender, EventArgs e)
        {
            float checkStock = float.Parse(stock_balance.Text);
                     
            float sales = float.Parse(selling_price.Text);
            float qnty = float.Parse(purc_qnty.Text);
            float amount = sales * qnty;
            pur_amount.Text = amount.ToString();
             /*double stock = Convert.ToDouble(textBox3.Text);
            double a_stock = Convert.ToDouble(purc_qnty.Text);
            double t_stock = stock + a_stock;
            textBox5.Text = t_stock.ToString();*/
            total_amt.Text = pur_amount.Text;
            textBox2.Text = pur_amount.Text;

            if (qnty > checkStock)
            {
                DialogResult hello = MessageBox.Show("Sorry, Check Your Stock", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (hello == DialogResult.OK)
                {
                    button1.Enabled = false;
                }
            }
            else
            {
                button1.Enabled = true;
            }
            columns_count();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new OleDbCommand("insert into stock(sn,s_date,p_code,f_name,bill_no,sales,s_unit,selling_price)values('" + posting_code.Text + "','" + maskedTextBox1.Text + "'," + p_code.Text + ",'" + paticular.Text + "','" + bill_no.Text + "'," + purc_qnty.Text + ",'" + pur_unit.Text + "'," + selling_price.Text + ")", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                cmd = new OleDbCommand("insert into creditor_cus(sn,p_date,p_code,bill_no,cr_name,dr)values('" + posting_code.Text + "','" + maskedTextBox1.Text + "'," + p_code.Text + ",'"+bill_no.Text+"','" + customer.Text + "'," + total_amt.Text + ")", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                
                cmd = new OleDbCommand("insert into sales_men(bill_no,sn,p_date,customer,p_code,particular,selling_price,selling_qnty,selling_unit,selling_price_total,discount,vat,total_amount)values('"+bill_no.Text+"','" + posting_code.Text + "','" + maskedTextBox1.Text + "','" + customer.Text + "'," + p_code.Text + ",'" + paticular.Text + "'," + selling_price.Text + "," + purc_qnty.Text + ",'" + pur_unit.Text + "'," + pur_amount.Text + "," + textBox1.Text + "," + t_vat.Text + "," + total_amt.Text + ")", con);
                con.Open();
                int sv = cmd.ExecuteNonQuery();
                if (sv > 0)
                {
                    MessageBox.Show("save saccesfull",title,MessageBoxButtons.OK,MessageBoxIcon.Information);
                }


            }
            catch (Exception yy)
            {
                MessageBox.Show(yy.Message);
            }
            finally
            {
                con.Close();
                billItemDesplay();
                auto_number();
                

              
               // GetData("select particular,selling_price,selling_qnty,selling_unit,selling_price_total,discount,vat,total_amount from sales_men where bill_no='"+posting_code.Text+"'");

                /*GetData("select * from sales_men" );
                dataGridView1.DataSource = bindingsource;
                this.dataGridView1.DataSource = AutoNumberedTable(table);*/
              
               
                
            }

            
        }

        private void billItemDesplay()
        {
            adb = new OleDbDataAdapter("select sn,particular,selling_price,selling_qnty,selling_unit,selling_price_total,discount,vat,total_amount from sales_men where bill_no='" + bill_no.Text + "'", con);
            DataTable dataTable = new DataTable();
            adb.Fill(dataTable);
            this.dataGridView1.DataSource = AutoNumberedTable(dataTable);


            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[1].HeaderText = "S.Code";
            this.dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "Paticular";
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].HeaderText = "Price";
            dataGridView1.Columns[3].Width = 75;
            dataGridView1.Columns[4].HeaderText = "QNTY";
            dataGridView1.Columns[4].Width = 50;
            dataGridView1.Columns[5].HeaderText = "Unit";
            dataGridView1.Columns[5].Width = 30;
            dataGridView1.Columns[6].HeaderText = "Amount";
            dataGridView1.Columns[6].Width = 90;
            dataGridView1.Columns[7].HeaderText = "Discount";
            dataGridView1.Columns[7].Width = 75;
            this.dataGridView1.Columns[7].Visible = false;
            // dataGridView1.Columns[6].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns[8].HeaderText = "Vat";
            dataGridView1.Columns[8].Width = 75;
            this.dataGridView1.Columns[8].Visible = false;
            // dataGridView1.Columns[7].DefaultCellStyle.Format = "N2";
            dataGridView1.Columns[9].HeaderText = "Total";
            dataGridView1.Columns[9].Width = 90;
            this.dataGridView1.Columns[9].Visible = false;
            // dataGridView1.Columns[8].DefaultCellStyle.Format = "N2";

            dataGridView1.ClearSelection();

            this.dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            this.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.GridColor = Color.White;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.EnableHeadersVisualStyles = false;

            double discount = 0;
            double vat = 0;
            double g_total = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                discount += Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                vat += Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
                g_total += Convert.ToDouble(dataGridView1.Rows[i].Cells[8].Value);

            }
            total_dis.Text = discount.ToString();
            Total_vat.Text = vat.ToString();
            Total_amount.Text = g_total.ToString();
        }
        private void GetData(string SelectCommand)
        {
            adb = new OleDbDataAdapter(SelectCommand, con);
            commandBuilder = new OleDbCommandBuilder(adb);
             table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            adb.Fill(table);
            bindingsource.DataSource = table;
          
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
                    paticular.Text = rdr[1].ToString();

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
               
            }
            catch (Exception uu)
            {
                MessageBox.Show(uu.Message);
            }
                                              
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

        private void p_dis_Leave(object sender, EventArgs e)
        {
            float total_pur = float.Parse(pur_amount.Text);
            float dis = float.Parse(p_dis.Text);
            float total_dis = total_pur * (dis / 100);
            textBox1.Text = total_dis.ToString();
            float total_amount = total_pur - total_dis;
            total_amt.Text = total_amount.ToString();
            textBox2.Text = total_amount.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
                      
        }
        private DataTable AutoNumberedTable(DataTable SourceTable)
        {

            DataTable ResultTable = new DataTable();

            DataColumn AutoNumberColumn = new DataColumn();

            AutoNumberColumn.ColumnName = "S.No.";

            AutoNumberColumn.DataType = typeof(int);
            
            AutoNumberColumn.AutoIncrement = true;

            AutoNumberColumn.AutoIncrementSeed = 1;

            AutoNumberColumn.AutoIncrementStep = 1;

            ResultTable.Columns.Add(AutoNumberColumn);

            ResultTable.Merge(SourceTable);

            return ResultTable;

        }

        private void prnDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            /*leftMargin = (int)e.MarginBounds.Left;
            rightMargin = (int)e.MarginBounds.Right;
            topMargin = (int)e.MarginBounds.Top;
            bottmMargin = (int)e.MarginBounds.Bottom;*/
            
            
           // e.Graphics.DrawLine(new Pen(Color.Black, 2), 60, 90, 700, 90);
            //e.Graphics.DrawLine(new Pen(Color.Black, 1), 60, 93, 700, 93);

            string strDisplay = "Dev Interprises";
            System.Drawing.Font fntString = new Font("Times New Roman", 18, FontStyle.Bold);
            e.Graphics.DrawString(strDisplay, fntString, Brushes.Black, 30, 30);

            strDisplay = "Karahiya-3,Bhalwari";
            fntString = new System.Drawing.Font("Times New Roman", 14, FontStyle.Bold);
            e.Graphics.DrawString(strDisplay, fntString, Brushes.Black,200,70);

            //e.Graphics.DrawLine(new Pen(Color.Black, 1), 60, 184, 700, 184);
           // e.Graphics.DrawLine(new Pen(Color.Black, 2), 60, 187, 700, 187);

            e.Graphics.DrawLine(new Pen(Color.Black, 2), 100, 100, 680, 100);
            e.Graphics.DrawLine(new Pen(Color.Black, 2), 100, 104, 680, 104);

            fntString = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
            e.Graphics.DrawString("Bill No:-", fntString, Brushes.Black, 100, 114);
            fntString = new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular);
            e.Graphics.DrawString(bill_no.Text, fntString, Brushes.Black, 150, 114);

            //Font StFont = new Font("Times New Roman", 10, FontStyle.Bold);
            fntString = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
            e.Graphics.DrawString("Date:-", fntString, Brushes.Black, 400, 114);
            e.Graphics.DrawString(maskedTextBox1.Text, fntString, Brushes.Black, 450, 114);

            e.Graphics.DrawString("customer Name:-", fntString, Brushes.Black, 100, 140);
            e.Graphics.DrawString(customer.Text, fntString, Brushes.Black, 260, 140);

            e.Graphics.DrawLine(new Pen(Color.Black, 1), 100, 170, 680, 170);

            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bm, new Rectangle(100, 0, this.dataGridView1.Width,this.dataGridView1.Height));
            e.Graphics.DrawImage(bm, 100, 180);

            e.Graphics.DrawLine(new Pen(Color.Black, 1), 100, 450, 680, 450);

            e.Graphics.DrawString("Discount:-", fntString, Brushes.Black, 500, 460);
            e.Graphics.DrawString(total_dis.Text, fntString, Brushes.Black, 560, 460);

            e.Graphics.DrawLine(new Pen(Color.Black, 1), 500, 480, 680, 480);

            e.Graphics.DrawString("Vat:-", fntString, Brushes.Black, 500, 490);
            e.Graphics.DrawString(Total_vat.Text, fntString, Brushes.Black, 560, 490);

            e.Graphics.DrawLine(new Pen(Color.Black, 1), 500, 510, 680, 510);

            e.Graphics.DrawString("Total:-", fntString, Brushes.Black, 500, 520);
            e.Graphics.DrawString(Total_amount.Text, fntString, Brushes.Black, 560, 520);

            e.Graphics.DrawLine(new Pen(Color.Black, 1), 100, 540, 680, 540);

        }

        private void SetInvoiceData(Graphics g, System.Drawing.Printing.PrintPageEventArgs e)
        {
           /* string fieldValue = "";
            int CurrentRecord = 0;
            int RecordsPerPage = 20;
            decimal Anount = 0;
            bool StopReading = false;
            int XproductID = leftMargin;
            CurrentY = CurrentY + InvoiceHeight;
            g.DrawString("SN", InvoiceFont, BlackBrush, XproductID, CurrentY);

            int xProductName = XproductID + (int)g.MeasureString("SN", InvoiceFont).Width + 4;
            g.DrawString("Particular", InvoiceFont, BlueBrush, xProductName, CurrentY);

            int xUnitPrice = xProductName + (int)g.MeasureString("Particular", InvoiceFont).Width + 72;
            g.DrawString("Price", InvoiceFont, BlueBrush, xUnitPrice, CurrentY);

            int xQuantity = xUnitPrice + (int)g.MeasureString("Price", InvoiceFont).Width + 4;
            g.DrawString("QNTY", InvoiceFont, BlueBrush, xQuantity, CurrentY);

            int xTotal = xQuantity + (int)g.MeasureString("QNTY", InvoiceFont).Width + 4;
            g.DrawString("Total", InvoiceFont, BlueBrush, xTotal, CurrentY);

            CurrentY = CurrentY + InvoiceFontHeight + 8;
            while (CurrentRecord < RecordsPerPage)
            {
                fieldValue = rdr["Particular"].ToString();

                if (fieldValue.Length > 20)
                    fieldValue = fieldValue.Remove(20, fieldValue.Length - 20);
                g.DrawString(fieldValue, InvoiceFont, BlackBrush, xProductName, CurrentY);
                fieldValue = String.Format("{0:0.00}", rdr["Price"]);
                g.DrawString(fieldValue, InvoiceFont, BlackBrush, xUnitPrice, CurrentY);
            }*/


        }

        private void button4_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Show();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
               
                cmd = new OleDbCommand("delete from stock where sn='" + posting_code.Text + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                cmd = new OleDbCommand("delete from creditor_cus where sn='" + posting_code.Text + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                cmd = new OleDbCommand("delete from sales_men where sn='" + posting_code.Text + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception yy)
            {
                MessageBox.Show(yy.Message);
            }
            finally
            {
                
                billItemDesplay();
                button1.Enabled = true;
            }
         


        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                this.dataGridView1.Columns[1].Visible = true;
            }

          
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            billItemDesplay();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
           
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                posting_code.Text = row.Cells[1].Value.ToString();
                paticular.Text = row.Cells[2].Value.ToString();
                selling_price.Text = row.Cells[3].Value.ToString();
                purc_qnty.Text = row.Cells[4].Value.ToString();
                pur_unit.Text = row.Cells[5].Value.ToString();
                pur_amount.Text = row.Cells[6].Value.ToString();
                p_dis.Text = row.Cells[7].Value.ToString();
                t_vat.Text = row.Cells[8].Value.ToString();
                Total_amount.Text = row.Cells[9].Value.ToString();
                button1.Enabled = false;
                button2.Enabled = true;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = prnDocument;
            printDialog.UseEXDialog = true;
            if (DialogResult.OK == printDialog.ShowDialog())
            {
                prnDocument.DocumentName = "INVOICE";
                prnDocument.Print();
            }
        }
        #region sales report;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
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
                    foreach (DataGridViewColumn GridCol in dataGridView2.Columns)
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
                while (iRow <= dataGridView2.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dataGridView2.Rows[iRow];
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
                            e.Graphics.DrawString("Sales Summary", new Font(dataGridView2.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Purchase Summary", new Font(dataGridView2.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate, new Font(dataGridView2.Font, FontStyle.Bold),
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, new Font(dataGridView1.Font,
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top -
                                    e.Graphics.MeasureString("Sales Summary", new Font(new Font(dataGridView2.Font,
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dataGridView2.Columns)
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


        private void button7_Click(object sender, EventArgs e)
        {
                GetData("select * from sales_men" );
                dataGridView2.DataSource = bindingsource;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            printPreviewDialog2.Show();
        }
        #region begin print event Handler
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
                foreach (DataGridViewColumn dgvGridCol in dataGridView2.Columns)
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

    }
}
