using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2
{
    public partial class Form1 : Form
    {
        Graphics g1, g2;
        List<Point> points1 = new List<Point>();
        List<Point> points2 = new List<Point>();
        Pen pen = new Pen(Brushes.Black, 5);
        public Form1()
        {
            InitializeComponent();
            g1 = pictureBox1.CreateGraphics();
            g2 = pictureBox2.CreateGraphics();
        }

        Image image;
        Bitmap bitmap, bitmap2, bitmap4, bitmap8, bitmap16, bitmap32;
        SolidBrush brush;
        int picture1counts = 0, picture2counts = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            g1.Clear(Color.White);
            g2.Clear(Color.White);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (picture1counts == 3) //больше 3 точек = сбрасываем
            {
                points1.Clear();
                g1.Clear(Color.White);
                if (image != null)
                g1.DrawImage(image, 0, 0, pictureBox1.Width, pictureBox1.Height);
                picture1counts = 0;
            }
            picture1counts++;//счетчик точек
            points1.Add(new Point(e.Location.X, e.Location.Y));//Добавляем точку в список точек
            g1.DrawEllipse(pen, e.Location.X, e.Location.Y, 3, 3);//Рисуем точку
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (picture2counts == 3)//больше 3 точек = сбрасываем
            {
                points2.Clear();
                g2.Clear(Color.White);
                if (image != null)
                    g2.DrawImage(image, 0, 0, pictureBox1.Width, pictureBox1.Height);
                picture2counts = 0;
            }
            picture2counts++;//счетчик точек
            points2.Add(new Point(e.Location.X, e.Location.Y));  //Добавляем точку в список точек
            g2.DrawEllipse(pen, e.Location.X, e.Location.Y, 3, 3); //Рисуем точку
        }

        private void Transform_button_Click(object sender, EventArgs e)
        {
            if (picture1counts == 3 && picture2counts == 3)
            {
                g2.Clear(Color.White);
                Point[] p1 = points1.ToArray<Point>();
                Point[] p2 = points2.ToArray<Point>();
                double[,] a = new double[3, 3];
                double[,] b = new double[3, 3];
                double[,] TransformMatrix = new double[3, 3];
                double[,] coordinates = new double[3, 1];
                double[,] neighbour = new double[3, 1];
                double[,] TransCoord = new double[3, 1];
                double[,] NeighbourCords = new double[3, 1];
                double distance;
                int m;
                Bitmap bitmapm, bitmap2m;
                for (int j = 0; j < 3; j++)
                {
                    //Массив старых и новых точек
                    b[0, j] = p1[j].X;
                    b[1, j] = p1[j].Y;
                    b[2, j] = 1;
                    a[0, j] = p2[j].X;
                    a[1, j] = p2[j].Y;
                    a[2, j] = 1;
                }
                TransformMatrix = MultiplyM(a, InvertM(b)); //Матрица преобразования новые точки умножить на обратные старые
                TransformMatrix[2, 0] = 0; TransformMatrix[2, 1] = 0; TransformMatrix[2, 2] = 1;
                coordinates[2, 0] = 1;
                neighbour[2, 0] = 1;
                if ((Math.Abs(TransformMatrix[0, 0]) > 1 && Math.Abs(TransformMatrix[1, 1]) > 1) || (Math.Abs(TransformMatrix[0, 1]) > 1 && Math.Abs(TransformMatrix[1, 0]) > 1))//проверяем по матрице увеличение или уменьшение
                {
                    //Увеличение изображения
                    double Xfloor, Yfloor, Xceiling, Yceiling, X, Y;
                    TransformMatrix = InvertM(TransformMatrix); //Получаем матрицу обратного преобразования
                    for (int i = 0; i < pictureBox2.Width; i++)
                    {
                        coordinates[0, 0] = i;
                        for (int j = 0; j < pictureBox2.Height; j++)
                        {
                            coordinates[1, 0] = j;
                            TransCoord = MultiplyM(TransformMatrix, coordinates); //Получаем соответствующие точки на исходном изображении
                            //Округление вверх и вниз
                            X = TransCoord[0, 0];
                            Y = TransCoord[1, 0]; 
                            Xfloor = Math.Floor(X); 
                            Xceiling = Math.Ceiling(X);
                            Yfloor = Math.Floor(Y);
                            Yceiling = Math.Ceiling(Y);
                            //Билинейная интерполяция
                            if (Xfloor > 0 && Xceiling > 0 && Yfloor > 0 && Yceiling > 0 && Xfloor < pictureBox1.Width && Xceiling < pictureBox1.Width && Yfloor < pictureBox1.Height && Yceiling < pictureBox1.Height)
                            {
                                Color color1 = bitmap.GetPixel((int)Xfloor, (int)Yfloor);
                                Color color2 = bitmap.GetPixel((int)Xceiling, (int)Yfloor);
                                Color color3 = bitmap.GetPixel((int)Xfloor, (int)Yceiling);
                                Color color4 = bitmap.GetPixel((int)Xceiling, (int)Yceiling);
                                double colorR = (color1.R * (Xceiling - X) + color2.R * (X - Xfloor)) * (Yceiling - Y) + (color3.R * (Xceiling - X) + color4.R * (X - Xfloor)) * (Y - Yfloor);
                                double colorG = (color1.G * (Xceiling - X) + color2.G * (X - Xfloor)) * (Yceiling - Y) + (color3.G * (Xceiling - X) + color4.G * (X - Xfloor)) * (Y - Yfloor);
                                double colorB = (color1.B * (Xceiling - X) + color2.B * (X - Xfloor)) * (Yceiling - Y) + (color3.B * (Xceiling - X) + color4.B * (X - Xfloor)) * (Y - Yfloor);
                                Color color = Color.FromArgb((int)colorR, (int)colorG, (int)colorB);
                                brush = new SolidBrush(color);
                                g2.FillRectangle(brush, i, j, 1, 1);
                            }
                        }
                    }
                } 
                else if ((Math.Abs(TransformMatrix[0, 0]) < 1 && Math.Abs(TransformMatrix[1, 1]) < 1) || (Math.Abs(TransformMatrix[0, 1]) < 1 && Math.Abs(TransformMatrix[1, 0]) < 1))//Проверяем по матрице преобразования
                {
                    //Уменьшение
                    TransformMatrix = InvertM(TransformMatrix);
                    for (int i = 0; i < pictureBox2.Width; i++)
                    {
                        coordinates[0, 0] = i;
                        if (i == 0) { neighbour[0, 0] = i + 1; }//Берем соседские пиксели
                        else { neighbour[0, 0] = i - 1; }
                        for (int j = 0; j < pictureBox2.Height; j++)
                        {
                            coordinates[1, 0] = j;
                            if (j == 0) { neighbour[1, 0] = j + 1; }
                            else { neighbour[1, 0] = j - 1; }
                            TransCoord = MultiplyM(TransformMatrix, coordinates);
                            NeighbourCords = MultiplyM(TransformMatrix, neighbour);
                            distance = (Math.Abs(TransCoord[0, 0] - NeighbourCords[0, 0]) + Math.Abs(TransCoord[1, 0] - NeighbourCords[1, 0])) / 2; //Берем разницу как среднее между разницой по x и y
                            if (distance < 2) { bitmapm = bitmap; bitmap2m = bitmap2; m = 1; }//Выбираем уровни детализации
                            else if(distance < 4) { bitmapm = bitmap2; bitmap2m = bitmap4; m = 2; } 
                            else if(distance < 8) { bitmapm = bitmap4; bitmap2m = bitmap8;  m = 4; }
                            else { bitmapm = bitmap8; bitmap2m = bitmap16; m = 8; }
                            if (TransCoord[0, 0] > 0 && TransCoord[1, 0] > 0 && TransCoord[0, 0] < pictureBox1.Width && TransCoord[1, 0] < pictureBox1.Height)
                            {
                                Color colorm = bitmapm.GetPixel(((int)TransCoord[0, 0] / m), (int)(TransCoord[1, 0] / m));
                                Color color2m = bitmap2m.GetPixel(((int)TransCoord[0, 0] / (2 * m)), (int)(TransCoord[1, 0] / (2 * m)));
                                //Линейная интерполяция
                                double colorR = (colorm.R * (2 * m - distance) + color2m.R * (distance - m)) / m;
                                double colorG = (colorm.G * (2 * m - distance) + color2m.G * (distance - m)) / m; 
                                double colorB = (colorm.B * (2 * m - distance) + color2m.B * (distance - m)) / m;
                                if (colorR < 0) { colorR = 0; } if (colorR > 255) { colorR = 255; }
                                if (colorG < 0) { colorG = 0; } if (colorG > 255) { colorG = 255; }
                                if (colorB < 0) { colorB = 0; } if (colorB > 255) { colorB = 255; }
                                Color color = Color.FromArgb((int)colorR, (int)colorG, (int)colorB);
                                brush = new SolidBrush(color);
                                g2.FillRectangle(brush, i, j, 1, 1);
                            }
                        }
                    }
                }
                else //Простейший алгоритм 
                {
                    TransformMatrix = InvertM(TransformMatrix);
                    for (int i = 0; i < pictureBox2.Width; i++)
                    {
                        coordinates[0, 0] = i;
                        for (int j = 0; j < pictureBox2.Height; j++)
                        {
                            coordinates[1, 0] = j;
                            TransCoord = MultiplyM(TransformMatrix, coordinates);
                            if (TransCoord[0, 0] > 0 && TransCoord[1, 0] > 0 && TransCoord[0, 0] < pictureBox1.Width && TransCoord[1, 0] < pictureBox1.Height)
                            {
                                brush = new SolidBrush(bitmap.GetPixel((int)Math.Round(TransCoord[0, 0]), (int)Math.Round(TransCoord[1, 0])));
                                g2.FillRectangle(brush, i, j, 1, 1);
                            }
                        }
                    }
                } 
            }
        }

        private void Download_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображение|*.bmp;*.png;*.jpg";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            image = Image.FromFile(openFileDialog.FileName);
            g1.Clear(Color.White);
            g1.DrawImage(image, 0, 0, pictureBox1.Width, pictureBox1.Height);
            bitmap = new Bitmap(image, pictureBox1.Width + 1, pictureBox1.Height + 1);
            bitmap2 = new Bitmap(image, (pictureBox1.Width + 1) / 2, (pictureBox1.Height + 1) / 2);
            bitmap4 = new Bitmap(image, (pictureBox1.Width + 1) / 4, (pictureBox1.Height + 1) / 4);
            bitmap8 = new Bitmap(image, (pictureBox1.Width + 1) / 8, (pictureBox1.Height + 1) / 8);
            bitmap16 = new Bitmap(image, (pictureBox1.Width + 1) / 16, (pictureBox1.Height + 1) / 16);
            bitmap32 = new Bitmap(image, (pictureBox1.Width + 1) / 32, (pictureBox1.Height + 1) / 32);
        }

        public static double[,] MultiplyM(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) { throw new Exception("Невозможно выполнить умножение"); }

            int ma = a.GetLength(0);
            int mb = b.GetLength(0);
            int nb = b.GetLength(1);

            double[,] r = new double[ma, nb];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < nb; j++)
                {
                    for (int k = 0; k < mb; k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }

        public static double[,] InvertM(double[,] matrix)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            double[,] res = new double[m, n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    res[i, j] = matrix[i, j];
                }
            }

            int[] row = new int[n];
            int[] col = new int[n];

            double[] temp = new double[n];
            int hold;
            int I_pivot;
            int J_pivot;
            double pivot;
            double abs_pivot;

            // установиим row и column как вектор изменений.
            for (int k = 0; k < n; k++)
            {
                row[k] = k;
                col[k] = k;
            }

            // начало главного цикла
            for (int k = 0; k < n; k++)
            {
                // найдем наибольший элемент для основы
                pivot = res[row[k], col[k]];
                I_pivot = k;
                J_pivot = k;
                for (int i = k; i < n; i++)
                {
                    for (int j = k; j < n; j++)
                    {
                        abs_pivot = Math.Abs(pivot);
                        if (Math.Abs(res[row[i], col[j]]) > abs_pivot)
                        {
                            I_pivot = i;
                            J_pivot = j;
                            pivot = res[row[i], col[j]];
                        }
                    }
                }

                if (Math.Abs(pivot) < 1.0E-10)
                {
                    throw new Exception("!");
                }

                // Перестановка к-ой строки и к-ого столбца с стобцом и строкой, содержащий основной элемент
                hold = row[k];
                row[k] = row[I_pivot];
                row[I_pivot] = hold;
                hold = col[k];
                col[k] = col[J_pivot];
                col[J_pivot] = hold;

                // k-ую строку с учетом перестановок делим на основной элемент
                res[row[k], col[k]] = 1.0 / pivot;

                for (int j = 0; j < n; j++)
                {
                    if (j != k)
                    {
                        res[row[k], col[j]] = res[row[k], col[j]] * res[row[k], col[k]];
                    }
                }

                // Внутренний цикл
                for (int i = 0; i < n; i++)
                {
                    if (k != i)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (k != j)
                            {
                                res[row[i], col[j]] = res[row[i], col[j]] - res[row[i], col[k]] * res[row[k], col[j]];
                            }
                        }
                        res[row[i], col[k]] = -res[row[i], col[k]] * res[row[k], col[k]];
                    }
                }
            }

            // Переставляем назад rows
            for (int j = 0; j < n; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    temp[col[i]] = res[row[i], j];
                }

                for (int i = 0; i < n; i++)
                {
                    res[i, j] = temp[i];
                }
            }

            // Переставляем назад columns
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    temp[row[j]] = res[i, col[j]];
                }

                for (int j = 0; j < n; j++)
                {
                    res[i, j] = temp[j];
                }
            }

            return res;
        }
    }
}
