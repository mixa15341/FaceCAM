using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.Util;
using DirectShowLib;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private VideoCapture capture = null;
        private DsDevice[] webCams = null;
        private int selectedCamID = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            webCams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            for (int i = 0; i < webCams.Length; i++)
            {
                toolStripComboBox1.Items.Add(webCams[i].Name);
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCamID = toolStripComboBox1.SelectedIndex;
        }
        //просмотр
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (webCams.Length == 0)
                {
                    throw new Exception("У тебя нет камеры)))");
                }
                else if (toolStripComboBox1.SelectedItem == null)
                {
                    throw new Exception("А кто за тебя камеру выбирать будет?)");
                }
                else if (capture != null)
                {
                    capture.Start();
                }
                else
                {
                    capture = new VideoCapture(selectedCamID);

                    capture.ImageGrabbed += Capture_ImageGrabbed;

                    capture.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибочка!)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                Mat m = new Mat();

                capture.Retrieve(m);

                pictureBox1.Image = m.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).Bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибочка!)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //пауза
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (capture != null)
                {
                    capture.Pause();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибочка!)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //стоп
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (capture != null)
                {
                    capture.Pause();

                    capture.Dispose();

                    capture = null;

                    pictureBox1.Image.Dispose();

                    pictureBox1.Image = null;

                    selectedCamID = 0;

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

        private void обнаружитьЛицоНаФотоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 fr1 = new Form1();
            fr1.Show();
            Hide();
        }
        //скрин
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                Mat n = new Mat();

                capture.Retrieve(n);

                Screenshot Screenshot = new Screenshot(n.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal));

                Screenshot.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибочка!)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
