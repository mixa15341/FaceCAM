using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        private static CascadeClassifier classifire = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult res = openFileDialog1.ShowDialog();

                if(res == DialogResult.OK)
                {
                    string path = openFileDialog1.FileName;

                    pictureBox1.Image = Image.FromFile(path);

                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                    Bitmap bitmap = new Bitmap(pictureBox1.Image);

                    Image<Bgr, byte> grayImage = new Image<Bgr, byte>(bitmap);

                    Rectangle[] faces = classifire.DetectMultiScale(grayImage, 1.4, 0);

                    foreach (Rectangle face in faces)
                    {
                        using(Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            using (Pen pen = new Pen(Color.Red, 3))
                            {
                                graphics.DrawRectangle(pen, face);

                            }
                        }

                    }

                    pictureBox1.Image = bitmap;
                }
                else
                {
                    MessageBox.Show("Изображение не выбрано!", "Ошибочка!)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибочка!)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void захватВидеоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fr2 = new Form2();
            fr2.Show();
            Hide();
        }
    }
}
