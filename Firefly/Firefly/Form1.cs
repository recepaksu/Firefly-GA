using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Firefly
{
    public partial class Form1 : Form
    {
        double min = Sphere.min, max = Sphere.max, best;
        Func<double, double, double> calculateFitness;
        bool modified = false;
        Functions a;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            checkBox1.Visible = false;
            button2.Enabled = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {

            button1.Enabled = false;
            button2.Enabled = false;
            richTextBox1.Text = "";
            int population = Convert.ToInt32(textBox1.Text), iter = Convert.ToInt32(textBox2.Text), moveBest = Convert.ToInt32(textBox6.Text);
            double alpha = Convert.ToDouble(textBox3.Text), gamma = Convert.ToDouble(textBox4.Text), beta = Convert.ToDouble(textBox5.Text);
            progressBar1.Maximum = iter;
            bool gammaRandom = checkBox1.Checked;


            a = new Functions(this.calculateFitness, population, alpha, gamma, beta, min, max, moveBest, modified, gammaRandom);
            a.createİnitialPopulation();
            a.findBest();
            chartOlustur(a.bugs);
            best = a.bestBug.fitness;
            for (int i = 0; i < iter; i++)
            {
                a.tryAllWay();
                chartOlustur(a.bugs);
                progressBar1.Value = i + 1;
                if (best > a.bestBug.fitness)
                {
                    string temp = "x  :" + a.bestBug.x.ToString() + "\ny  :" + a.bestBug.y.ToString() + "\nfitness  :" + a.bestBug.fitness.ToString() + "\n\n";
                    temp += richTextBox1.Text;
                    richTextBox1.Text = temp;
                    best = a.bestBug.fitness;
                }
            }
            button1.Enabled = true;
            button2.Enabled = true;
        }

        void chartOlustur(List<Bug> bugs)
        {
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.Minimum = min;
            chart1.ChartAreas[0].AxisX.Maximum = max;
            chart1.ChartAreas[0].AxisY.Minimum = min;
            chart1.ChartAreas[0].AxisY.Maximum = max;

            int sayac = 1;
            foreach (var bug in bugs)
            {
                string name = "bug " + sayac.ToString();
                chart1.Series.Add(name);
                chart1.Series[name].Points.AddXY(bug.x, bug.y);
                chart1.Series[name].ChartType = SeriesChartType.Point;
                chart1.Series[name].BorderWidth = 3;
                sayac++;
            }
            Application.DoEvents();
            //Thread.Sleep(1000);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Visible = false;
            modified = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Visible = true;
            modified = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox4.ReadOnly = true;
            }
            else
            {
                textBox4.ReadOnly = false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int population = Convert.ToInt32(textBox1.Text), iter = Convert.ToInt32(textBox2.Text), moveBest = Convert.ToInt32(textBox6.Text);
            double alpha = Convert.ToDouble(textBox3.Text), gama = Convert.ToDouble(textBox4.Text), beta = Convert.ToDouble(textBox5.Text);

            a.tryAllWay();
            chartOlustur(a.bugs);

            if (best > a.bestBug.fitness)
            {
                string temp = "x  :" + a.bestBug.x.ToString() + "\ny  :" + a.bestBug.y.ToString() + "\nfitness  :" + a.bestBug.fitness.ToString() + "\n\n";
                temp += richTextBox1.Text;
                richTextBox1.Text = temp;
                best = a.bestBug.fitness;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = false;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    min = Sphere.min;
                    max = Sphere.max;
                    this.calculateFitness = Sphere.fitness;
                    break;
                case 1:
                    min = Booth.min;
                    max = Booth.max;
                    this.calculateFitness = Booth.fitness;
                    break;
                case 2:
                    min = Himmelblau.min;
                    max = Himmelblau.max;
                    this.calculateFitness = Himmelblau.fitness;
                    break;
            }
        }
    }
}
