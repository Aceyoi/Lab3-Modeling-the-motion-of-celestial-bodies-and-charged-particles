using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp10
{
    public partial class Form1 : Form
    {
        // ��������� ������
        private double q = 1.6e-19; // ����� ������� (��)
        private double m = 9.11e-31; // ����� ������� (��)
        private double E = 1000; // ������������� ���� (�/�)
        private double vx0 = 1e6; // ��������� �������� �� ��� X (�/�)
        private double dt = 1e-10; // ��� �� ������� (�)
        private double tMax = 1e-7; // ������������ ����� ������������� (�)

        // ���������� ��� ��������
        private double x, y, t;
        private System.Windows.Forms.Timer timer; // ����� �������� ����
        private Bitmap bmp;
        private Graphics g;

        public Form1()
        {
            InitializeComponent();

            // ������������� �������
            timer = new System.Windows.Forms.Timer(); // ����� �������� ����
            timer.Interval = 10; // �������� ���������� �������� (��)
            timer.Tick += Timer_Tick_1;

            // ������������� Bitmap � Graphics
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);

            // ��������� �������
            x = 50;
            y = pictureBox1.Height / 2;
            t = 0;
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            // ������� PictureBox ����� ������� ��������
            pictureBox1.Image = null;
            g.Clear(Color.White);

            // ��������� ������� ������������
            Pen platePen = new Pen(Color.Black, 3);
            g.DrawLine(platePen, 50, 50, pictureBox1.Width - 50, 50); // ������� ��������
            g.DrawLine(platePen, 50, pictureBox1.Height - 50, pictureBox1.Width - 50, pictureBox1.Height - 50); // ������ ��������

            // ������ ������� ��� ��������
            timer.Start();
        }

        private void Timer_Tick_1(object sender, EventArgs e)
        {
            // ������ ���������
            double a = q * E / m;

            // ���������� ������� �������
            double newX = x + vx0 * t * 1e6; // ��������������� ��� ������������
            double newY = y + 0.5 * a * t * t * 1e6; // ��������������� ��� ������������

            // �����������, ����� ������� �� �������� �� ������� PictureBox
            if (newX >= pictureBox1.Width - 50 || newY >= pictureBox1.Height - 50 || newY <= 50 || t >= tMax)
            {
                timer.Stop();
                return;
            }

            // ��������� ����������
            Pen trajectoryPen = new Pen(Color.Red, 2);
            g.DrawLine(trajectoryPen, (float)x, (float)y, (float)newX, (float)newY);

            // ���������� ������� �������
            x = newX;
            y = newY;
            t += dt;

            // ����������� Bitmap � PictureBox
            pictureBox1.Image = bmp;
        }
    }
}