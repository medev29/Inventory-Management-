using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace inventory_management
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (Form1.LOGINSUCCESS == true)
            {
                masterToolStripMenuItem.Visible = true;
            }
            else
            {
                masterToolStripMenuItem.Visible = false;
            }
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            product pro = new product();
            pro.Enabled = true;
            panel1.Controls.Clear();
            panel1.Controls.Add(pro);
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
           /* stock_keeper stkeeper = new stock_keeper();
            stkeeper.Enabled = true;
            panel1.Controls.Clear();
            panel1.Controls.Add(stkeeper);*/
        }

        private void customerListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            customer_list cus = new customer_list();
            cus.Enabled = true;
            panel1.Controls.Clear();
            panel1.Controls.Add(cus);
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stock_keeper stkeeper = new stock_keeper();
            stkeeper.Enabled = true;
            panel1.Controls.Clear();
            panel1.Controls.Add(stkeeper);
        }

        private void openingStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stock_frm stock = new stock_frm();
            stock.Enabled = true;
            panel1.Controls.Clear();
            panel1.Controls.Add(stock);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void sellerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            sales_form sel = new sales_form();
            sel.Enabled = true;
            panel1.Controls.Clear();
            panel1.Controls.Add(sel);

        }
    }
}
