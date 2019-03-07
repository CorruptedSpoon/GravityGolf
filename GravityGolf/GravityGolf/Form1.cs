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
        //stores PlanetStructs that will appear in the listBox Planets
        List<PlanetStruct> planets = new List<PlanetStruct>();

        public Form1()
        {
            InitializeComponent();      
        }

        //initialize default values
        private void Form1_Load(object sender, EventArgs e)
        {
            Planets.Items.Add("Planet " + planets.Count);
            planets.Add(new PlanetStruct(0, 0, PlanetType.small));

            textBox5.Text = "level1";

            textBox1.Text = "0";
            textBox2.Text = "0";
        }

        //display correct values when changing selected planet
        private void Planets_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = planets[Planets.SelectedIndex].x + "";
            textBox3.Text = planets[Planets.SelectedIndex].y + "";
            checkedListBox1.SetItemChecked((int)planets[Planets.SelectedIndex].planetType, true);
        }

        //make sure only one box can be checked at a time to determine planet size
        private void checkedListBox1_SelectedIndexChanged(object sender, ItemCheckEventArgs e)
        {
             for (int x = 0; x < checkedListBox1.Items.Count; x++)
                if (x != e.Index) checkedListBox1.SetItemChecked(x, false);
        }

        //saves level using LevelWriter
        private void button1_Click(object sender, EventArgs e)
        {
            int x = 0;
            int.TryParse(textBox4.Text, out x);
            int y = 0;
            int.TryParse(textBox3.Text, out y);

            LevelWriter.WriteLevel(textBox5.Text, x, y, planets);

            MessageBox.Show("level saved");
        }

        //updates planets with new value for planetSize, makes sure unchecking will not result in nothing being checked
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            planets[Planets.SelectedIndex] = new PlanetStruct(planets[Planets.SelectedIndex].x, planets[Planets.SelectedIndex].y, (PlanetType)checkedListBox1.SelectedIndex);
            if (checkedListBox1.CheckedItems.Count == 0)
            {
                checkedListBox1.SetItemChecked(0, true);
            }
        }

        //adds a new planet to planets, updates Planets
        private void button2_Click(object sender, EventArgs e)
        {
            Planets.Items.Add("Planet " + planets.Count);
            planets.Add(new PlanetStruct(0, 0, PlanetType.small));
        }

        //updates planets x value, resets to zero upon typing an invalid character
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int x = 0;
            int.TryParse(textBox4.Text, out x);
            planets[Planets.SelectedIndex] = new PlanetStruct(x, planets[Planets.SelectedIndex].y, planets[Planets.SelectedIndex].planetType);
        }

        //^ same, for y
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int y = 0;
            int.TryParse(textBox3.Text, out y);
            planets[Planets.SelectedIndex] = new PlanetStruct(planets[Planets.SelectedIndex].x, y, planets[Planets.SelectedIndex].planetType);
        }

        //updates x position for ball, resets to zero upon typing an invalid character
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int x = 0;
            int.TryParse(textBox1.Text, out x);
            textBox1.Text = x + "";
        }

        //^ same, for y
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int y = 0;
            int.TryParse(textBox2.Text, out y);
            textBox2.Text = y + "";
        }
    }
}
