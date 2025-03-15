using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp10
{
    public partial class Form1 : Form
    {
        // Параметры модели
        private double q = 1.6e-19; // Заряд частицы (Кл)
        private double m = 9.11e-31; // Масса частицы (кг)
        private double E = 1000; // Напряженность поля (В/м)
        private double vx0 = 1e6; // Начальная скорость по оси X (м/с)
        private double dt = 1e-10; // Шаг по времени (с)
        private double tMax = 1e-7; // Максимальное время моделирования (с)

        // Переменные для анимации
        private double x, y, t;
        private System.Windows.Forms.Timer timer; // Явное указание типа
        private Bitmap bmp;
        private Graphics g;

        public Form1()
        {
            InitializeComponent();

            // Инициализация таймера
            timer = new System.Windows.Forms.Timer(); // Явное указание типа
            timer.Interval = 10; // Интервал обновления анимации (мс)
            timer.Tick += Timer_Tick_1;

            // Инициализация Bitmap и Graphics
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);

            // Начальные условия
            x = 50;
            y = pictureBox1.Height / 2;
            t = 0;
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            // Очистка PictureBox перед началом анимации
            pictureBox1.Image = null;
            g.Clear(Color.White);

            // Рисование пластин конденсатора
            Pen platePen = new Pen(Color.Black, 3);
            g.DrawLine(platePen, 50, 50, pictureBox1.Width - 50, 50); // Верхняя пластина
            g.DrawLine(platePen, 50, pictureBox1.Height - 50, pictureBox1.Width - 50, pictureBox1.Height - 50); // Нижняя пластина

            // Запуск таймера для анимации
            timer.Start();
        }

        private void Timer_Tick_1(object sender, EventArgs e)
        {
            // Расчет ускорения
            double a = q * E / m;

            // Обновление позиции частицы
            double newX = x + vx0 * t * 1e6; // Масштабирование для визуализации
            double newY = y + 0.5 * a * t * t * 1e6; // Масштабирование для визуализации

            // Ограничение, чтобы частица не выходила за пределы PictureBox
            if (newX >= pictureBox1.Width - 50 || newY >= pictureBox1.Height - 50 || newY <= 50 || t >= tMax)
            {
                timer.Stop();
                return;
            }

            // Рисование траектории
            Pen trajectoryPen = new Pen(Color.Red, 2);
            g.DrawLine(trajectoryPen, (float)x, (float)y, (float)newX, (float)newY);

            // Обновление текущей позиции
            x = newX;
            y = newY;
            t += dt;

            // Отображение Bitmap в PictureBox
            pictureBox1.Image = bmp;
        }
    }
}