using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Vista
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Bitmap bmp;
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        OpenFileDialog fileDialog = new OpenFileDialog();
        FolderBrowserDialog folderDialog = new FolderBrowserDialog();
        string extend;
        string file;
        bool check1;
        bool check2;
        bool check3;
        bool check4;
        int width, height, xstep, ystep;



        //OpenFileDialog fileDialog = new OpenFileDialog();
        private void import_Click(object sender, EventArgs e)
        {
            //using (OpenFileDialog fileDialog = new OpenFileDialog())
            //{
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //string fileAddress = fileDialog.FileName;

                bmp = new Bitmap(fileDialog.FileName);
                extend = Path.GetExtension(fileDialog.FileName);
                file = Path.GetFileNameWithoutExtension(fileDialog.FileName);
                
                pictureBox1.Image = bmp;
                HeightText.Text = pictureBox1.Image.Height.ToString();
                WidthText.Text = pictureBox1.Image.Width.ToString();


                //using (FileStream fl = new FileStream(fileAddress, FileMode.Open))
                //{
                //    Panel a = (Panel)xml.Deserialize(fl);
                //    rnd(a);
                //}
                //    }
                //}
            }
        }

            private void Export_Click(object sender, EventArgs e)
        {
                //using(SaveFileDialog saveFileDialog = new SaveFileDialog())
                //{
                if (saveFileDialog.ShowDialog() == DialogResult.OK && fileDialog.FileName.Length > 0)
                {
                    pictureBox1.Image.Save(saveFileDialog.FileName);
                }
                //}
            }

        
        public Bitmap blur(Bitmap bmp, int blr)
        {
            Bitmap bm = (Bitmap)bmp.Clone();

            int w = bm.Width;
            int h = bm.Height;

            // горизонталь
            for (int i = 0; i < w - 2 * blr; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    Color c1 = bm.GetPixel(i, j);
                    Color c2 = bm.GetPixel((i + blr), j);
                    Color c3 = bm.GetPixel(i + 2 * blr, j);


                    byte bR = (byte)((c1.R + c2.R + c3.R) / 3);
                    byte bG = (byte)((c1.G + c2.G + c3.G) / 3);
                    byte bB = (byte)((c1.B + c2.B + c3.B) / 3);


                    Color cBlured = Color.FromArgb(bR, bG, bB);

                    bm.SetPixel(i, j, cBlured);

                }
                //i += 2 * blr - 1;
            }

            // вертикаль
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h - 2 * blr; j++)
                {
                    Color c1 = bm.GetPixel(i, j);
                    Color c2 = bm.GetPixel(i, j + blr);
                    Color c3 = bm.GetPixel(i, j + 2 * blr);


                    byte bR = (byte)((c1.R + c2.R + c3.R) / 3);
                    byte bG = (byte)((c1.G + c2.G + c3.G) / 3);
                    byte bB = (byte)((c1.B + c2.B + c3.B) / 3);


                    Color cBlured = Color.FromArgb(bR, bG, bB);

                    bm.SetPixel(i, j, cBlured);
                    //j += 2 * blr - 1;

                }

            }

            return bm;
        }


        public Bitmap brightness(Bitmap bmp, double bright)
        {
            Bitmap res = (Bitmap)bmp.Clone();
            int red, green, blue;
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color pixel = bmp.GetPixel(x, y);
                    red = (int)Math.Round(pixel.R * (bright/100), 0);
                    if (red < 0)
                        red = 0;
                    if (red > 255)
                        red = 255;
                    green = (int)Math.Round(pixel.G * (bright / 100), 0);
                    if (green < 0)
                        green = 0;
                    if (green > 255)
                        green = 255;
                    blue = (int)Math.Round(pixel.B * (bright / 100), 0);
                    if (blue < 0)
                        blue = 0;
                    if (blue > 255)
                        blue = 255;
                    Color next = Color.FromArgb((byte)red, (byte)green, (byte)blue);
                    res.SetPixel(x, y, next);
                }
            }
            return res;
            //bmp = res;
        }

        private void Proverka()
        {
            if (!int.TryParse(x_frm.Text, out width) || x_frm.Text == String.Empty)
            {
                check1 = false;
                x_frm.BackColor = Color.Red;
            }
            else
            {
                check1 = true;
                x_frm.BackColor = Color.White;
            }

            //Проверка b
            if (!int.TryParse(y_frm.Text, out height) || y_frm.Text == String.Empty)
            {
                check2 = false;
                y_frm.BackColor = Color.Red;
            }
            else
            {
                check2 = true;
                y_frm.BackColor = Color.White;
            }

            //Проверка с
            if (!int.TryParse(stepx.Text, out xstep) || stepx.Text == String.Empty)
            {
                check3 = false;
                stepx.BackColor = Color.Red;
            }
            else
            {
                check3 = true;
                stepx.BackColor = Color.White;
            }

            if (!int.TryParse(stepy.Text, out ystep) || stepy.Text == String.Empty)
            {
                check4 = false;
                stepy.BackColor = Color.Red;
            }
            else
            {
                check4 = true;
                stepy.BackColor = Color.White;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Bitmap res = bmp;
            //int red, green, blue;
            //for(int x = 0; x < bmp.Width; x++)
            //{
            //    for(int y = 0; y < bmp.Height; y++)
            //    {
            //        Color pixel = bmp.GetPixel(x, y);
            //        red = (int)Math.Round(pixel.R * 0.9, 0);
            //        if (red < 0)
            //            red = 0;
            //        green = (int)Math.Round(pixel.G * 0.9, 0);
            //        if (green < 0)
            //            green = 0;
            //        blue = (int)Math.Round(pixel.B * 0.9, 0);
            //        if (blue < 0)
            //            blue = 0;
            //        Color next = Color.FromArgb((byte)red, (byte)green, (byte)blue);
            //        res.SetPixel(x, y, next);
            //    }
            //}
            bmp = brightness(bmp, 90);
            pictureBox1.Image = bmp;
            //bmp = res;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //Bitmap res = bmp;
            //int red, green, blue;
            //for (int x = 0; x < bmp.Width; x++)
            //{
            //    for (int y = 0; y < bmp.Height; y++)
            //    {
            //        Color pixel = bmp.GetPixel(x, y);
            //        red = (int)Math.Round(pixel.R * 1.1, 0);
            //        if (red > 255)
            //            red = 255;
            //        green = (int)Math.Round(pixel.G * 1.1, 0);
            //        if (green > 255)
            //            green = 255;
            //        blue = (int)Math.Round(pixel.B * 1.1, 0);
            //        if (blue > 255)
            //            blue = 255;
            //        Color next = Color.FromArgb((byte)red, (byte)green, (byte)blue);
            //        res.SetPixel(x, y, next);
            //    }
            //}
            bmp = brightness(bmp, 110);
            pictureBox1.Image = bmp;
            //bmp = res;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = brightness(bmp, double.Parse(x_frm.Text));
            //pictureBox1.Image = blur(bmp, int.Parse(textBox1.Text));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox1.Image = bmp;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bmp.RotateFlip(RotateFlipType.Rotate90FlipXY);
            pictureBox1.Image = bmp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = blur(bmp, int.Parse(y_frm.Text));
        }

        private void x_frm_TextChanged(object sender, EventArgs e)
        {
            Proverka();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("ВАЖНО!!! Сначала выбирите папку, куда будут сохранятся фото\n" +
                "Импорт - испортируем изображение для обработки\n"+
                "В первое окно ввода необходимо ввести желаемую ширину\n" +
                "Во второе окно вводим желаемую высоту изображения\n" +
                "В третье окно вводим Шаг обработки по оси Х\n" +
                "В четвертое вводим Шаг по оси Y\n" +
                "После запуска кнопка окрасится в красный\n" +
                "ПОЖАЛУЙСТА, дождитесь вывода сообщения о готовности");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (folderDialog.ShowDialog() == DialogResult.OK && folderDialog.SelectedPath.Length > 0)
            {
                textBox3.Text = folderDialog.SelectedPath;
            }
        }


        private Bitmap GetPicture(Bitmap origin, int x, int y, int width, int height)
        {
            Bitmap std = (Bitmap)origin.Clone();
            Bitmap copy = new Bitmap(width, height);
            //if (x + width > origin.Width) x = origin.Width - width;
            x = x + width > origin.Width ? origin.Width - width : x;
            y = y + height > origin.Height ? origin.Height - height : y;
            //using (Graphics g = Graphics.FromImage(copy))
            //{
            //    g.DrawImage(std, new Rectangle(x,y,width,height), new Rectangle(0,0,width,height), GraphicsUnit.Pixel);
            //}
            copy = std.Clone(new Rectangle(x, y, width, height), System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            return copy;
        }

        private void start_Click(object sender, EventArgs e)
        {
            //int width = int.Parse(x_frm.Text);
            //int height = int.Parse(y_frm.Text);
            //int xstep = int.Parse(stepx.Text);
            //int ystep = int.Parse(stepy.Text);
            Proverka();
            if (check1 && check2 && check3 && check4)
            {
                ((Button)sender).BackColor = Color.Red;
                for (int j = 0; j <= bmp.Height - height; j += ystep)
                {
                    for (int i = 0; i <= bmp.Width - width; i += xstep)
                    {
                        Bitmap result = GetPicture(bmp, i, j, width, height);
                        for (double bright = 50; bright <= 200; bright += 50)
                        {
                            result = brightness(result, bright);
                            for (int blr = 0; blr < 2; blr++)
                            {
                                result = (Bitmap)blur(result, blr).Clone();
                                for (int rt = 0; rt < 3; rt++)
                                {
                                    result.Save(textBox3.Text + "\\" + file + "_X" + ((i + width) / 2).ToString()
                                        + "_Y"+((j + height) / 2).ToString() + "_" +
                                        bright.ToString() + "_" + blr.ToString()
                                        + "_" + rt.ToString() + extend);
                                    result.RotateFlip(RotateFlipType.Rotate90FlipNone);

                                }
                                Application.DoEvents();
                            }
                        }
                    }
                }
                ((Button)sender).BackColor = Color.White;
                MessageBox.Show("Готово!");
            }
            
        }
    }
}
