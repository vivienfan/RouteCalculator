using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RouteCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panel1.Invalidate();
            pictureBox1.ImageLocation = "http://fc05.deviantart.net/fs71/f/2010/315/a/2/walking_animation_front_by_spookyspoots-d32o2an.gif";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "" || textBox2.Text == "" || textBox2.Text == "")    // invalid input
            {
                MessageBox.Show("Empty postal code.", "Invalid Inputs",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string[] source = new string[4] {textBox1.Text.Split((char[]) null, StringSplitOptions.RemoveEmptyEntries)[0],
                                              textBox2.Text.Split((char[]) null, StringSplitOptions.RemoveEmptyEntries)[0],
                                              textBox3.Text.Split((char[]) null, StringSplitOptions.RemoveEmptyEntries)[0],
                                              textBox4.Text.Split((char[]) null, StringSplitOptions.RemoveEmptyEntries)[0]};
            Point[] pts = new Point[4];
            char first;
            int second;
            int gap = 50;
            int offset_x = 25;
            int offset_y = 25;

            string textDis = "";

            for (int i = 0; i < 4; i++)
            {
                first = source[i][0];
                second = (int)source[i][1] - 48;
                if (second <= 0 || second > 6)  //invalid input
                {
                    MessageBox.Show("Postal code out of range.", "Invalid Inputs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (first == 'A')
                {
                    pts[i].X = (second - 1) * gap + offset_x;
                    pts[i].Y = 5 * gap + offset_y;
                }
                else if (first == 'B')
                {
                    pts[i].X = (second - 1) * gap + offset_x;
                    pts[i].Y = 4 * gap + offset_y;
                }
                else if (first == 'C')
                {
                    pts[i].X = (second - 1) * gap + offset_x;
                    pts[i].Y = 3 * gap + offset_y;
                }
                else if (first == 'D')
                {
                    pts[i].X = (second - 1) * gap + offset_x;
                    pts[i].Y = 2 * gap + offset_y;
                }
                else if (first == 'E')
                {
                    pts[i].X = (second - 1) * gap + offset_x;
                    pts[i].Y = gap + offset_y;
                }
                else if (first == 'F')
                {
                    pts[i].X = (second - 1) * gap + offset_x;
                    pts[i].Y = offset_y;
                }
                else // invalid input
                {
                    MessageBox.Show("Postal code out of range.", "Invalid Inputs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            int[,] combination = new int[12, 4] { { 1, 2, 3, 4 }, { 1, 2, 4, 3 }, { 1, 3, 2, 4 }, { 1, 3, 4, 2 }, 
                                                    { 1, 4, 2, 3 }, { 1, 4, 3, 2 }, { 2, 1, 3, 4 }, { 2, 1, 4, 3 }, 
                                                    { 2, 3, 1, 4 }, { 2, 4, 1, 3 }, { 3, 1, 2, 4 }, { 3, 2, 1, 4 } };
            double[] min = new double[2] { 0, 1000000000 };   //min[0] = j-th combination
            //min[1] = total distance (initially = infinity)
            double temp_dis = 0;
            int temp_x0 = 150;
            int temp_y0 = 150;
            int temp_x1 = 0;
            int temp_y1 = 0;

            for (int j = 0; j < 12; j++) //iterate through all combinations
            {
                temp_dis = 0;
                temp_x0 = 150;
                temp_y0 = 150;
                for (int k = 0; k < 4; k++)
                {
                    temp_x1 = pts[combination[j, k] - 1].X;
                    temp_y1 = pts[combination[j, k] - 1].Y;
                    temp_dis = Math.Round(Math.Pow((Math.Pow(temp_x0 - temp_x1, 2) + Math.Pow(temp_y0 - temp_y1, 2)), 0.5), 3) + temp_dis;
                    temp_x0 = temp_x1;
                    temp_y0 = temp_y1;
                }
                temp_dis = Math.Round(Math.Pow((Math.Pow(temp_x0 - 150, 2) + Math.Pow(temp_y0 - 150, 2)), 0.5), 3) + temp_dis;
                if (temp_dis < min[1])
                {
                    min[0] = j;
                    min[1] = temp_dis;
                }
            }

            label1.Text = " Home -> " + source[combination[(int)min[0], 0] - 1] + " -> " + 
                            source[combination[(int)min[0], 1] - 1] + " -> " +
                            source[combination[(int)min[0], 2] - 1] + " -> " + 
                            source[combination[(int)min[0], 3] - 1] + " -> Home";
            label1.Refresh();
            label2.Text = Convert.ToString(min[1]) + " m";
            label2.Refresh();

            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.BlueViolet, 2);
            g.DrawLine(p, new Point(150, 150), pts[combination[(int)min[0], 0] - 1]);
            g.DrawLine(p, pts[combination[(int)min[0], 0] - 1], pts[combination[(int)min[0], 1] - 1]);
            g.DrawLine(p, pts[combination[(int)min[0], 1] - 1], pts[combination[(int)min[0], 2] - 1]);
            g.DrawLine(p, pts[combination[(int)min[0], 2] - 1], pts[combination[(int)min[0], 3] - 1]);
            g.DrawLine(p, pts[combination[(int)min[0], 3] - 1], new Point(150,150));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }
    }
}
