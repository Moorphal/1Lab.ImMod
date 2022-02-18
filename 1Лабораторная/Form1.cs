using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1Лабораторная
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const double g = 9.81;
        const double C = 0.15;
        const double p = 1.29;

        double height, angle, speed, size,  weight, dt;

        double cosA, sinA;

        double x, y, t, B, k, vx, vy;

        double distance, maxheight = 0, speedend;

        int i = 0;
        private void btStart_Click(object sender, EventArgs e)
        {
            t = 0;

            height = (double)edHeight.Value;
            angle = (double)edAngle.Value;
            speed = (double)edSpeed.Value;
            size = (double)edSize.Value;
            weight = (double)edWeight.Value;
            dt = (double)edStep.Value;

            sinA = Math.Sin(angle * Math.PI / 180);
            cosA = Math.Cos(angle * Math.PI / 180);

            x = 0;
            y = height;
            B = 0.5 * C * p * size;
            k = B / weight;
            vx = speed * cosA;
            vy = speed * sinA;

            graph.Series[i].Points.AddXY(x, y);

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double vxOld = vx;
            double vyOld = vy;

            t = t + dt;

            vx = vxOld - k * vxOld * Math.Sqrt(vx * vx + vy * vy) * dt;
            vy = vyOld - (g + k * vyOld * Math.Sqrt(vx * vx + vy * vy)) * dt;

            x = x + vx * dt;
            y = y + vy * dt;

            graph.Series[i].Points.AddXY(x, y);

            if (y >= maxheight) maxheight = y;

            if (y <= 0)
            {
                distance = x; speedend = Math.Sqrt(vx * vx + vy * vy);
                timer1.Stop();
                dataGridView1.Rows.Add(dt, distance, maxheight, speedend);
                i++;
            };

        }
    }
}
