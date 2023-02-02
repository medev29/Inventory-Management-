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
    public partial class Form1 : Form
    {
        public static bool LOGINSUCCESS = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string user=comboBox1.Text;
            string pass=textBox1.Text;


            if (user == "admin" && pass == "hello")
            {
                LOGINSUCCESS = true;
                Form2 one = new Form2();
                one.Show();
                //Form3 c_detail = new Form3();
               // c_detail.Show();

                this.Hide();
            }
            else
            {
                LOGINSUCCESS = false;
                MessageBox.Show("sorry");
                Form2 one = new Form2();
                one.Show();
            
            }
        }
    }
}
