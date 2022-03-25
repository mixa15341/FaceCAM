using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.Util;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Screenshot : Form
    {
        private Image<Bgr, byte> image = null;
        private string filename = string.Empty;
        public Screenshot(Image<Bgr, byte> image)
        {

            this.image = image;
            InitializeComponent();
        }

        private void Screenshot_Load(object sender, EventArgs e)
        {
            filename = $"FaceCAM_{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}.jpg";

            pictureBox1.Image = image.Bitmap;

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image.Save(filename, ImageFormat.Jpeg);

                if (File.Exists(filename))
                {
                    Close();
                }
                else
                {
                    throw new Exception("Я не хочу сохранять изображение!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибочка!)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
