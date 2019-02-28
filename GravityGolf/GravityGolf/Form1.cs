using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GravityGolf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();      
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Planets_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, ItemCheckEventArgs e)
        {
             for (int x = 0; x < checkedListBox1.Items.Count; ++x)
                if (x != e.Index) checkedListBox1.SetItemChecked(x, false);
        }

        //save
        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
