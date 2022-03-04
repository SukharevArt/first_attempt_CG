using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace first_attempt
{
    public partial class Form1 : Form
    {
        Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All files(*.*) | *.*";
            if (dialog.ShowDialog() == DialogResult.OK) {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }

        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap newImage = InvertFilter.Execute(image);
            pictureBox1.Image=newImage;
            pictureBox1.Refresh();
        }

        private void отенкиСерогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap newImage = GrayFilter.Execute(image);
            pictureBox1.Image = newImage;
            pictureBox1.Refresh();
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap newImage = SepiaFilter.Execute(image);
            pictureBox1.Image = newImage;
            pictureBox1.Refresh();
        }

        private void сдвигВправоНа50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap newImage = ShiftFilter.Execute(image);
            pictureBox1.Image = newImage;
            pictureBox1.Refresh();
        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap newImage = EmbFilter.Execute(image);
            pictureBox1.Image = newImage;
            pictureBox1.Refresh();
        }

        private void размытиеВДвиженииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap newImage = MotionFilter.Execute(image);
            pictureBox1.Image = newImage;
            pictureBox1.Refresh();
        }

        private void серыйМирToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap newImage = GrayWrFilter.Execute(image);
            pictureBox1.Image = newImage;
            pictureBox1.Refresh();
        }

        private void autolevelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap newImage = AutoLevFilter.Execute(image);
            pictureBox1.Image = newImage;
            pictureBox1.Refresh();
        }

        private void медианаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap newImage = SobelFilter.Execute(image);
            pictureBox1.Image = newImage;
            pictureBox1.Refresh();
        }
    }
}
